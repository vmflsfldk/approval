from pathlib import Path

from fastapi import FastAPI, HTTPException
from fastapi.responses import HTMLResponse
from fastapi.staticfiles import StaticFiles

from .admin import router as admin_router
from .auth import router as auth_router

app = FastAPI()

frontend_dir = Path(__file__).resolve().parent.parent / "frontend_admin"
app.mount("/static", StaticFiles(directory=str(frontend_dir), html=True), name="admin_static")


@app.get("/", response_class=HTMLResponse)
def index():
    index_file = frontend_dir / "index.html"
    if not index_file.exists():
        raise HTTPException(status_code=500, detail="Admin frontend not found")
    return HTMLResponse(index_file.read_text(encoding="utf-8"))


app.include_router(auth_router)
app.include_router(admin_router)
