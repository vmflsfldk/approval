import sys
from contextlib import contextmanager
from pathlib import Path

import pytest
from fastapi import HTTPException

# Ensure the backend package can be imported when running tests from the repo root
sys.path.append(str(Path(__file__).resolve().parents[2]))

import backend.admin as admin_module


class FakeCursor:
    def __init__(self, data):
        self.data = data
        self._result = None

    def __enter__(self):
        return self

    def __exit__(self, exc_type, exc, tb):
        return False

    def execute(self, sql, params=None):
        self.last_sql = sql
        self.last_params = params
        self._result = None

        if sql.startswith("SELECT username FROM users WHERE username="):
            username = params[0]
            if username in self.data["users"]:
                self._result = {"username": username}
            return

        if sql.startswith("SELECT username, name, role FROM users WHERE username="):
            username = params[0]
            user = self.data["users"].get(username)
            if user:
                self._result = {
                    "username": username,
                    "name": user["name"],
                    "role": user["role"],
                }
            return

        if sql.startswith("UPDATE users SET"):
            target_username = params[-1]
            user = self.data["users"].pop(target_username, None)
            if user is None:
                return

            set_clause = sql.split("SET", 1)[1].split("WHERE", 1)[0]
            fields = [field.strip() for field in set_clause.split(",")]

            idx = 0
            new_username = target_username
            for field in fields:
                if field == "username = %s":
                    new_username = params[idx]
                    idx += 1
                elif field == "name = %s":
                    user["name"] = params[idx]
                    idx += 1
                elif field == "password_hash = %s":
                    user["password_hash"] = params[idx]
                    idx += 1
                elif field == "role = %s":
                    user["role"] = params[idx]
                    idx += 1

            self.data["users"][new_username] = user
            return

        if sql.startswith("UPDATE docs SET owner_user"):
            new_owner, old_owner = params
            for doc in self.data["docs"]:
                if doc["owner_user"] == old_owner:
                    doc["owner_user"] = new_owner
            return

        if sql.startswith("UPDATE audit_logs SET `user`"):
            new_user, old_user = params
            for log in self.data["audit_logs"]:
                if log["user"] == old_user:
                    log["user"] = new_user
            return

    def fetchone(self):
        return self._result


class FakeConnection:
    def __init__(self, data):
        self.data = data

    def cursor(self):
        return FakeCursor(self.data)

    def commit(self):
        pass

    def rollback(self):
        pass

    def close(self):
        pass


def patch_get_db(monkeypatch, data):
    @contextmanager
    def fake_get_db():
        yield FakeConnection(data)

    monkeypatch.setattr(admin_module, "get_db", fake_get_db)


def test_update_user_allows_username_change(monkeypatch):
    data = {
        "users": {
            "alice": {"password_hash": "hash", "name": "Alice", "role": "user"},
        },
        "docs": [
            {"owner_user": "alice", "id": 1},
            {"owner_user": "charlie", "id": 2},
        ],
        "audit_logs": [
            {"user": "alice"},
            {"user": "charlie"},
        ],
    }

    patch_get_db(monkeypatch, data)

    result = admin_module.update_user(
        "alice",
        admin_module.UserUpdate(username="alice2", name="Alice Smith"),
        current_user={"username": "admin", "role": "admin"},
    )

    assert result.username == "alice2"
    assert result.name == "Alice Smith"
    assert result.role == "user"
    assert "alice" not in data["users"]
    assert "alice2" in data["users"]
    assert data["users"]["alice2"]["name"] == "Alice Smith"
    assert data["docs"][0]["owner_user"] == "alice2"
    assert data["docs"][1]["owner_user"] == "charlie"
    assert data["audit_logs"][0]["user"] == "alice2"
    assert data["audit_logs"][1]["user"] == "charlie"


def test_update_user_rejects_duplicate_username(monkeypatch):
    data = {
        "users": {
            "alice": {"password_hash": "hash", "name": "Alice", "role": "user"},
            "bob": {"password_hash": "hash2", "name": "Bob", "role": "admin"},
        },
        "docs": [],
        "audit_logs": [],
    }

    patch_get_db(monkeypatch, data)

    with pytest.raises(HTTPException) as exc:
        admin_module.update_user(
            "alice",
            admin_module.UserUpdate(username="bob"),
            current_user={"username": "admin", "role": "admin"},
        )

    assert exc.value.status_code == 409
    assert "alice" in data["users"]
    assert "bob" in data["users"]
