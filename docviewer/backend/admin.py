from typing import List, Literal, Optional

from fastapi import APIRouter, Depends, HTTPException, status
from pydantic import BaseModel, constr

from .auth import pwd_context
from .db import get_db
from .dependencies import require_admin

router = APIRouter(prefix="/admin", tags=["admin"])


class UserOut(BaseModel):
    username: str
    role: str


class UserCreate(BaseModel):
    username: constr(min_length=1, max_length=190)
    password: constr(min_length=4, max_length=128)
    role: Literal["admin", "user"]


class UserUpdate(BaseModel):
    username: Optional[constr(min_length=1, max_length=190)] = None
    password: Optional[constr(min_length=4, max_length=128)] = None
    role: Optional[Literal["admin", "user"]] = None


@router.get("/users", response_model=List[UserOut])
def list_users(current_user=Depends(require_admin)):
    with get_db() as conn:
        with conn.cursor() as cur:
            cur.execute("SELECT username, role FROM users ORDER BY username")
            rows = cur.fetchall()
            return [UserOut(**row) for row in rows]


@router.post("/users", status_code=status.HTTP_201_CREATED, response_model=UserOut)
def create_user(payload: UserCreate, current_user=Depends(require_admin)):
    password_hash = pwd_context.hash(payload.password)
    with get_db() as conn:
        with conn.cursor() as cur:
            cur.execute("SELECT username FROM users WHERE username=%s", (payload.username,))
            if cur.fetchone():
                raise HTTPException(status_code=status.HTTP_409_CONFLICT, detail="Username already exists")
            cur.execute(
                "INSERT INTO users(username, password_hash, role) VALUES (%s, %s, %s)",
                (payload.username, password_hash, payload.role),
            )
    return UserOut(username=payload.username, role=payload.role)


@router.patch("/users/{username}", response_model=UserOut)
def update_user(username: str, payload: UserUpdate, current_user=Depends(require_admin)):
    updates = []
    params = []
    new_username = payload.username if payload.username else None
    if new_username and new_username == username:
        new_username = None

    if new_username:
        updates.append("username = %s")
        params.append(new_username)
    if payload.password:
        updates.append("password_hash = %s")
        params.append(pwd_context.hash(payload.password))
    if payload.role:
        updates.append("role = %s")
        params.append(payload.role)
    if not updates:
        raise HTTPException(status_code=status.HTTP_400_BAD_REQUEST, detail="No changes provided")
    params.append(username)
    with get_db() as conn:
        with conn.cursor() as cur:
            cur.execute("SELECT username FROM users WHERE username=%s", (username,))
            if not cur.fetchone():
                raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail="User not found")

            if new_username:
                cur.execute("SELECT username FROM users WHERE username=%s", (new_username,))
                if cur.fetchone():
                    raise HTTPException(status_code=status.HTTP_409_CONFLICT, detail="Username already exists")

            cur.execute(f"UPDATE users SET {', '.join(updates)} WHERE username = %s", params)

            if new_username:
                cur.execute(
                    "UPDATE docs SET owner_user = %s WHERE owner_user = %s",
                    (new_username, username),
                )
                cur.execute(
                    "UPDATE audit_logs SET `user` = %s WHERE `user` = %s",
                    (new_username, username),
                )

            cur.execute(
                "SELECT username, role FROM users WHERE username=%s",
                (new_username or username,),
            )
            updated = cur.fetchone()
    return UserOut(**updated)


@router.delete("/users/{username}", status_code=status.HTTP_204_NO_CONTENT)
def delete_user(username: str, current_user=Depends(require_admin)):
    with get_db() as conn:
        with conn.cursor() as cur:
            cur.execute("DELETE FROM users WHERE username=%s", (username,))
            if cur.rowcount == 0:
                raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail="User not found")
    return None
