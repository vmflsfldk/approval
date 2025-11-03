import json
import os
from contextlib import contextmanager
from typing import Generator

import pymysql
from pymysql.cursors import DictCursor


_CONFIG_CACHE = None


def load_config() -> dict:
    global _CONFIG_CACHE
    if _CONFIG_CACHE is None:
        config_path = os.environ.get("DOCVIEWER_CONFIG", os.path.join(os.path.dirname(__file__), "config.json"))
        if not os.path.exists(config_path):
            raise RuntimeError(f"Config file not found: {config_path}. Copy config.example.json to config.json and update it.")
        with open(config_path, "r", encoding="utf-8") as f:
            _CONFIG_CACHE = json.load(f)
    return _CONFIG_CACHE


def get_connection():
    config = load_config()
    db_conf = config["db"]
    return pymysql.connect(
        host=db_conf.get("host", "127.0.0.1"),
        user=db_conf["user"],
        password=db_conf["password"],
        database=db_conf.get("database", "approvaldb"),
        charset="utf8mb4",
        autocommit=False,
        cursorclass=DictCursor,
    )


@contextmanager
def get_db() -> Generator[pymysql.connections.Connection, None, None]:
    conn = get_connection()
    try:
        yield conn
        conn.commit()
    except Exception:
        conn.rollback()
        raise
    finally:
        conn.close()
