from pathlib import Path


def test_frontend_contains_session_reset_message():
    html = Path("frontend/index.html").read_text(encoding="utf-8")
    assert "function clearSession" in html
    assert "세션이 만료되었습니다. 다시 로그인하세요." in html

