import argparse
import os
import re
import sys
from contextlib import nullcontext
from datetime import datetime, date
from pathlib import Path
from typing import Optional, Tuple

BASE_DIR = Path(__file__).resolve().parents[1]
BACKEND_DIR = BASE_DIR / "backend"
if str(BACKEND_DIR) not in sys.path:
    sys.path.insert(0, str(BACKEND_DIR))

from db import get_db, load_config  # type: ignore

PDF_PATTERNS = [
    re.compile(r"^(?P<owner>[^-]+)-(?P<date>\d{4}-\d{2}-\d{2})(?:-(?P<no>[^.]+))?", re.IGNORECASE),
    re.compile(r"^(?P<owner>[^_]+)_(?P<date>\d{4}-\d{2}-\d{2})_(?P<no>[^.]+)", re.IGNORECASE),
    re.compile(r"^(?P<owner>[A-Za-z0-9]+)[-_](?P<year>\d{4})(?P<month>\d{2})(?P<day>\d{2})(?:[-_](?P<no>[^.]+))?", re.IGNORECASE),
]


def parse_metadata(file_path: Path) -> Tuple[str, Optional[str], date]:
    stem = file_path.stem
    for pattern in PDF_PATTERNS:
        match = pattern.match(stem)
        if match:
            groups = match.groupdict()
            owner = groups.get("owner", "").strip()
            if pattern is PDF_PATTERNS[2]:
                date_str = f"{groups['year']}-{groups['month']}-{groups['day']}"
            else:
                date_str = groups.get("date")
            doc_no = groups.get("no")
            if doc_no:
                doc_no = doc_no.strip()
            if date_str:
                try:
                    created_date = datetime.strptime(date_str, "%Y-%m-%d").date()
                except ValueError:
                    created_date = datetime.fromtimestamp(file_path.stat().st_mtime).date()
            else:
                created_date = datetime.fromtimestamp(file_path.stat().st_mtime).date()
            return owner, doc_no if doc_no else None, created_date
    created_date = datetime.fromtimestamp(file_path.stat().st_mtime).date()
    return "", None, created_date


def index_file(path: Path, root: Path, apply: bool, conn=None):
    relative_path = path.relative_to(root)
    owner, doc_no, created_date = parse_metadata(path)
    created_date_str = created_date.isoformat()

    sql = (
        "INSERT INTO docs(file_path, file_name, owner_user, doc_no, created_date) "
        "VALUES (%s, %s, %s, %s, %s) "
        "ON DUPLICATE KEY UPDATE "
        "file_name=VALUES(file_name), "
        "owner_user=IF(VALUES(owner_user) <> '', VALUES(owner_user), owner_user), "
        "doc_no=IFNULL(VALUES(doc_no), doc_no), "
        "created_date=VALUES(created_date), "
        "created_ts=CURRENT_TIMESTAMP"
    )
    params = (
        str(relative_path).replace(os.sep, "/"),
        path.name,
        owner,
        doc_no,
        created_date_str,
    )

    if apply and conn is not None:
        with conn.cursor() as cur:
            cur.execute(sql, params)
        conn.commit()
        print(f"Indexed: {relative_path}")
    else:
        print(f"DRY-RUN: would index {relative_path} -> owner={owner}, doc_no={doc_no}, created_date={created_date_str}")


def main():
    parser = argparse.ArgumentParser(description="Index PDF files into MariaDB")
    parser.add_argument("--root", type=Path, help="Override document root directory")
    parser.add_argument("--apply", action="store_true", help="Apply changes (default: dry-run)")
    args = parser.parse_args()

    config = load_config()
    root = Path(args.root) if args.root else Path(config["doc_root"])

    if not root.exists():
        raise SystemExit(f"Root directory not found: {root}")

    ctx = get_db() if args.apply else nullcontext(None)
    with ctx as conn:
        for file_path in sorted(root.rglob("*.pdf")):
            if file_path.is_file():
                index_file(file_path, root, args.apply, conn)


if __name__ == "__main__":
    main()
