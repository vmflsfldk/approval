from pathlib import Path
from typing import Any, Dict, List, Optional

from fastapi import Depends, FastAPI, HTTPException, Query
from fastapi.responses import FileResponse, HTMLResponse
from fastapi.staticfiles import StaticFiles
from pymysql import OperationalError

from .auth import router as auth_router
from .db import get_db, load_config
from .dependencies import get_current_user

app = FastAPI()

frontend_dir = Path(__file__).resolve().parent.parent / "frontend"
app.mount("/frontend", StaticFiles(directory=str(frontend_dir), html=True), name="frontend")


@app.get("/", response_class=HTMLResponse)
def index() -> HTMLResponse:
    index_file = frontend_dir / "index.html"
    if not index_file.exists():
        raise HTTPException(status_code=500, detail="Frontend index not found")
    return HTMLResponse(index_file.read_text(encoding="utf-8"))


app.include_router(auth_router)


def apply_access_filters(where: List[str], params: List[Any], user: Dict[str, Any], target_user: Optional[str]):
    if user["role"] == "user":
        where.append("owner_user = %s")
        params.append(user["username"])
    elif user["role"] == "admin" and target_user:
        where.append("owner_user = %s")
        params.append(target_user)


@app.get("/api/titles")
def get_titles(current_user: Dict[str, Any] = Depends(get_current_user)):
    with get_db() as conn:
        with conn.cursor() as cur:
            where: List[str] = []
            params: List[Any] = []
            apply_access_filters(where, params, current_user, None)
            where_sql = f"WHERE {' AND '.join(where)}" if where else ""
            query = (
                "SELECT DISTINCT CASE WHEN LOCATE('-', file_name) > 0 "
                "THEN SUBSTRING(file_name, 1, LOCATE('-', file_name) - 1) ELSE file_name END AS title "
                "FROM docs "
                f"{where_sql} "
                "ORDER BY title"
            )
            cur.execute(query, params)
            rows = cur.fetchall()
            titles = [row["title"] for row in rows if row["title"]]
    return {"titles": titles}


@app.get("/api/docs")
def list_docs(
    type: str = Query("all", regex="^(all|day|month)$"),
    date: Optional[str] = Query(None),
    ym: Optional[str] = Query(None),
    user: Optional[str] = Query(None),
    q: Optional[str] = Query(None),
    title: Optional[str] = Query(None),
    page: int = Query(1, ge=1),
    size: int = Query(50, ge=1, le=200),
    current_user: Dict[str, Any] = Depends(get_current_user),
):
    offset = (page - 1) * size
    where: List[str] = []
    params: List[Any] = []
    title_expr = "CASE WHEN LOCATE('-', file_name) > 0 THEN SUBSTRING(file_name, 1, LOCATE('-', file_name) - 1) ELSE file_name END"
    apply_access_filters(where, params, current_user, user)

    if title:
        where.append(f"{title_expr} = %s")
        params.append(title)

    if type == "day":
        if not date:
            raise HTTPException(status_code=400, detail="date is required for type=day")
        where.append("created_date = %s")
        params.append(date)
    elif type == "month":
        if not ym:
            raise HTTPException(status_code=400, detail="ym is required for type=month")
        where.append("created_ym = %s")
        params.append(ym)

    search_clause = ""
    search_params: List[Any] = []
    fulltext = False
    if q:
        search_clause = "MATCH(file_name, doc_no) AGAINST(%s IN BOOLEAN MODE)"
        search_params = [q]
        fulltext = True

    order_clause = " ORDER BY created_date DESC, id DESC"

    def run_query(use_fulltext: bool):
        extra_where = where.copy()
        extra_params = params.copy()
        if q:
            if use_fulltext:
                extra_where.append(search_clause)
                extra_params.extend(search_params)
            else:
                extra_where.append("(file_name LIKE %s OR doc_no LIKE %s)")
                pattern = f"%{q}%"
                extra_params.extend([pattern, pattern])
        final_where = " WHERE " + " AND ".join(extra_where) if extra_where else ""
        count_sql = "SELECT COUNT(*) AS cnt " + "FROM docs" + final_where
        data_sql = (
            "SELECT id, file_name, owner_user, doc_no, created_date "
            + "FROM docs"
            + final_where
            + order_clause
            + " LIMIT %s OFFSET %s"
        )
        data_params = extra_params + [size, offset]
        with get_db() as conn:
            with conn.cursor() as cur:
                cur.execute(count_sql, extra_params)
                total = cur.fetchone()["cnt"]
                cur.execute(data_sql, data_params)
                items = cur.fetchall()
                if q:
                    cur.execute(
                        "INSERT INTO audit_logs(`user`, action, doc_id, q) VALUES (%s, 'SEARCH', NULL, %s)",
                        (current_user["username"], q),
                    )
        return total, items

    try:
        total, items = run_query(fulltext)
    except OperationalError:
        if not q:
            raise
        total, items = run_query(False)

    return {"page": page, "size": size, "total": total, "items": items}


def fetch_doc(doc_id: int, current_user: Dict[str, Any]) -> Dict[str, Any]:
    where = ["id = %s"]
    params: List[Any] = [doc_id]
    apply_access_filters(where, params, current_user, None)
    final_where = " WHERE " + " AND ".join(where)
    query = "SELECT * FROM docs" + final_where + " LIMIT 1"
    with get_db() as conn:
        with conn.cursor() as cur:
            cur.execute(query, params)
            doc = cur.fetchone()
    if not doc:
        raise HTTPException(status_code=404, detail="Document not found")
    return doc


@app.get("/api/docs/{doc_id}")
def get_doc(doc_id: int, current_user: Dict[str, Any] = Depends(get_current_user)):
    doc = fetch_doc(doc_id, current_user)
    return doc


def resolve_file_path(doc: Dict[str, Any]) -> Path:
    config = load_config()
    doc_root = Path(config["doc_root"]) if config.get("doc_root") else Path(".")
    file_path = Path(doc["file_path"])
    if not file_path.is_absolute():
        file_path = doc_root / file_path
    return file_path


@app.get("/api/docs/{doc_id}/preview")
def preview_doc(doc_id: int, current_user: Dict[str, Any] = Depends(get_current_user)):
    doc = fetch_doc(doc_id, current_user)
    file_path = resolve_file_path(doc)
    if not file_path.exists():
        raise HTTPException(status_code=404, detail="File not found")
    with get_db() as conn:
        with conn.cursor() as cur:
            cur.execute(
                "INSERT INTO audit_logs(`user`, action, doc_id, q) VALUES (%s, 'VIEW', %s, NULL)",
                (current_user["username"], doc_id),
            )
    return FileResponse(
        str(file_path),
        media_type="application/pdf",
        filename=file_path.name,
        headers={"Content-Disposition": f'inline; filename="{file_path.name}"'}
    )


@app.get("/api/docs/{doc_id}/download")
def download_doc(doc_id: int, current_user: Dict[str, Any] = Depends(get_current_user)):
    doc = fetch_doc(doc_id, current_user)
    file_path = resolve_file_path(doc)
    if not file_path.exists():
        raise HTTPException(status_code=404, detail="File not found")
    with get_db() as conn:
        with conn.cursor() as cur:
            cur.execute(
                "INSERT INTO audit_logs(`user`, action, doc_id, q) VALUES (%s, 'DOWNLOAD', %s, NULL)",
                (current_user["username"], doc_id),
            )
    return FileResponse(
        str(file_path),
        media_type="application/pdf",
        filename=file_path.name,
        headers={"Content-Disposition": f'attachment; filename="{file_path.name}"'},
    )
