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


def test_list_docs_rejects_mismatched_user_for_non_admin(monkeypatch):
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
            current_user={"username": "alice", "role": "user"},
        )
    assert exc.value.status_code == 403


def test_list_docs_allows_admin_requesting_other_user(monkeypatch):
    executions = []

    class ListDocsCursor:
        def __enter__(self):
            return self

        def __exit__(self, exc_type, exc, tb):
            return False

        def execute(self, sql, params=None):
            executions.append((sql, params))
            if sql.startswith("SELECT COUNT"):
                self._row = {"cnt": 0}
            elif sql.startswith("SELECT id, file_name"):
                self._rows = []
            else:
                self._row = None
                self._rows = []

        def fetchone(self):
            return getattr(self, "_row", None)

        def fetchall(self):
            return getattr(self, "_rows", [])

    class ListDocsConnection:
        def cursor(self):
            return ListDocsCursor()

    @contextmanager
    def fake_get_db():
        yield ListDocsConnection()

    monkeypatch.setattr(app_module, "get_db", fake_get_db)

    result = app_module.list_docs(
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

    assert result == {"page": 1, "size": 50, "total": 0, "items": []}
    count_sql, count_params = executions[0]
    data_sql, data_params = executions[1]
    assert "owner_user = %s" in count_sql
    assert "owner_user = %s" in data_sql
    assert count_params == ["bob"]
    assert data_params[0] == "bob"


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
        endpoint(doc_id=1, current_user={"username": "alice", "role": "user"})
    assert exc.value.status_code in {403, 404}


@pytest.mark.parametrize(
    "endpoint_name",
    [
        "get_doc",
        "preview_doc",
        "download_doc",
    ],
)
def test_doc_endpoints_allow_admin_for_other_owners(endpoint_name, monkeypatch, tmp_path):
    executions = []

    class DocCursor:
        def __enter__(self):
            return self

        def __exit__(self, exc_type, exc, tb):
            return False

        def execute(self, sql, params=None):
            executions.append((sql, params))
            if sql.startswith("SELECT * FROM docs"):
                self._row = {"id": 1, "owner_user": "bob", "file_path": "dummy.pdf"}
            else:
                self._row = None

        def fetchone(self):
            return getattr(self, "_row", None)

        def fetchall(self):
            return []

    class DocConnection:
        def cursor(self):
            return DocCursor()

    @contextmanager
    def fake_get_db():
        yield DocConnection()

    monkeypatch.setattr(app_module, "get_db", fake_get_db)

    endpoint = getattr(app_module, endpoint_name)

    if endpoint_name in {"preview_doc", "download_doc"}:
        file_path = tmp_path / "dummy.pdf"
        file_path.write_text("dummy")
        monkeypatch.setattr(app_module, "resolve_file_path", lambda doc: file_path)

    response = endpoint(doc_id=1, current_user={"username": "alice", "role": "admin"})

    if endpoint_name == "get_doc":
        assert response["owner_user"] == "bob"
    else:
        from fastapi.responses import FileResponse

        assert isinstance(response, FileResponse)

    select_queries = [sql for sql, _ in executions if sql.startswith("SELECT")]
    assert any("WHERE id = %s" in sql and "owner_user" not in sql for sql in select_queries)
