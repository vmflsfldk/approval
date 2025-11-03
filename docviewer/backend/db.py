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
        base_dir = os.path.dirname(__file__)
        config_path = os.environ.get("DOCVIEWER_CONFIG", os.path.join(base_dir, "config.json"))

        if os.path.exists(config_path):
            path_to_use = config_path
        else:
            example_path = os.path.join(base_dir, "config.example.json")
            if os.path.exists(example_path):
                path_to_use = example_path
            else:
                raise RuntimeError(
                    "Config file not found. Ensure either config.json or config.example.json exists in the backend directory."
                )

        with open(path_to_use, "r", encoding="utf-8") as f:
            _CONFIG_CACHE = json.load(f)
    return _CONFIG_CACHE


def get_connection():
    config = load_config()
    db_conf = config["db"]
    username = db_conf.get("user") or db_conf.get("root")
    if not username:
        raise KeyError("Database configuration must define 'user'.")

    return pymysql.connect(
        host=db_conf.get("host", "127.0.0.1"),
        user=username,
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
