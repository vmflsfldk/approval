from datetime import datetime, timedelta
from typing import Optional

import jwt
from fastapi import APIRouter, Form, HTTPException, status
from passlib.context import CryptContext

from .db import get_db, load_config

router = APIRouter(prefix="/auth", tags=["auth"])

pwd_context = CryptContext(schemes=["bcrypt"], deprecated="auto")


def verify_password(plain_password: str, password_hash: str) -> bool:
    return pwd_context.verify(plain_password, password_hash)


def create_access_token(data: dict, expires_delta: Optional[timedelta] = None) -> str:
    config = load_config()
    to_encode = data.copy()
    expire = datetime.utcnow() + (expires_delta or timedelta(hours=8))
    to_encode.update({"exp": expire})
    encoded_jwt = jwt.encode(to_encode, config["jwt_secret"], algorithm="HS256")
    return encoded_jwt


@router.post("/login")
def login(username: str = Form(...), password: str = Form(...)):
    with get_db() as conn:
        with conn.cursor() as cur:
            cur.execute("SELECT username, password_hash, role FROM users WHERE username=%s", (username,))
            user = cur.fetchone()
            if not user or not verify_password(password, user["password_hash"]):
                raise HTTPException(status_code=status.HTTP_401_UNAUTHORIZED, detail="Invalid credentials")
            token = create_access_token({"sub": user["username"], "role": user["role"]})
            cur.execute(
                "INSERT INTO audit_logs(`user`, action, doc_id, q) VALUES (%s, 'LOGIN', NULL, NULL)",
                (user["username"],),
            )
    return {"access_token": token, "token_type": "bearer"}
