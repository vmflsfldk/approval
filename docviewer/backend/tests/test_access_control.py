import sys
from contextlib import contextmanager
from pathlib import Path
from typing import Any, Dict, Optional

import pytest
from fastapi import HTTPException

# Ensure the backend package can be imported when running tests from the repo root
sys.path.append(str(Path(__file__).resolve().parents[2]))

import backend.app as app_module


class DummyCursor:
    def __init__(self, row: Optional[Dict[str, Any]] = None):
        self._row = row

    def __enter__(self):
        return self

    def __exit__(self, exc_type, exc, tb):
        return False

    def execute(self, sql: str, params: Optional[Any] = None):
        self.last_sql = sql
        self.last_params = params

    def fetchone(self):
        return self._row

    def fetchall(self):
        return []


class DummyConnection:
    def __init__(self, row: Optional[Dict[str, Any]] = None):
        self._row = row

    def cursor(self):
        return DummyCursor(self._row)


def make_forbidden_db(monkeypatch, *, row: Optional[Dict[str, Any]] = None):
    @contextmanager
    def fake_get_db():
        yield DummyConnection(row)

    monkeypatch.setattr(app_module, "get_db", fake_get_db)


def test_list_docs_rejects_mismatched_user(monkeypatch):
    def fail_get_db():
        raise AssertionError("database access should not occur for forbidden requests")

    monkeypatch.setattr(app_module, "get_db", fail_get_db)

    with pytest.raises(HTTPException) as exc:
        app_module.list_docs(
            type="all",
            date=None,
            ym=None,
            user="bob",
            q=None,
            title=None,
            page=1,
            size=50,
            current_user={"username": "alice", "role": "admin"},
        )
    assert exc.value.status_code == 403


@pytest.mark.parametrize(
    "endpoint",
    [
        app_module.get_doc,
        app_module.preview_doc,
        app_module.download_doc,
    ],
)
def test_doc_endpoints_forbidden_for_other_owners(endpoint, monkeypatch):
    make_forbidden_db(monkeypatch, row={"id": 1, "owner_user": "bob", "file_path": "dummy.pdf"})

    with pytest.raises(HTTPException) as exc:
        endpoint(doc_id=1, current_user={"username": "alice", "role": "admin"})
    assert exc.value.status_code in {403, 404}
