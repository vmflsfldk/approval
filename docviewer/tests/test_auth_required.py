import pytest

try:
    from fastapi.testclient import TestClient
    from backend.app import app
except ModuleNotFoundError:  # pragma: no cover - optional dependency for tests
    pytest.skip("fastapi is not available in the test environment", allow_module_level=True)


client = TestClient(app)


def test_titles_requires_authentication():
    response = client.get("/api/titles")
    assert response.status_code == 401
    assert response.json()["detail"] == "Not authenticated"


def test_docs_requires_authentication():
    response = client.get("/api/docs")
    assert response.status_code == 401
    assert response.json()["detail"] == "Not authenticated"
