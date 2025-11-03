# DocViewer

전자결재 문서를 로컬 PDF 스토리지와 MariaDB에 연동하여 열람/다운로드할 수 있는 단일 PC용 서비스입니다.

## 구성 요소
- **Backend**: FastAPI (Python 3.10+) + PyMySQL
- **Database**: MariaDB (utf8mb4, InnoDB)
- **Frontend**: 정적 HTML/JS, PDF.js 뷰어 포함
- **Tools**: 파일 분류 및 인덱싱 스크립트

## 설치

### 1. MariaDB 준비
```sql
CREATE DATABASE approvaldb DEFAULT CHARACTER SET utf8mb4;
CREATE USER 'docsvc'@'%' IDENTIFIED BY 'yourpass';
GRANT ALL ON approvaldb.* TO 'docsvc'@'%';
```

스키마 적용:
```bash
mysql -h 127.0.0.1 -u docsvc -p approvaldb < backend/schema.sql
```

### 2. 설정 파일
`backend/config.example.json`을 `backend/config.json`으로 복사하고 경로, DB, JWT 비밀키를 환경에 맞게 수정합니다.

### 3. 파이썬 패키지 설치
```bash
pip install -r backend/requirements.txt
```

### 4. 초기 계정 추가 예시
```python
from passlib.context import CryptContext
import pymysql

pwd = CryptContext(schemes=["bcrypt"], deprecated="auto")
password_hash = pwd.hash("adminpass")

conn = pymysql.connect(host="127.0.0.1", user="docsvc", password="yourpass", database="approvaldb", charset="utf8mb4")
with conn:
    with conn.cursor() as cur:
        cur.execute(
            "INSERT INTO users(username, password_hash, role) VALUES (%s, %s, %s)",
            ("admin", password_hash, "admin")
        )
    conn.commit()
```

## 실행
```bash
uvicorn backend.app:app --host 0.0.0.0 --port 8000
```

브라우저에서 [http://localhost:8000](http://localhost:8000) 접속 후 로그인합니다.

## 도구 사용

### 제목 기준 폴더 정리
```bash
python tools/classify_by_title.py "D:\\approval\\approval_doc" --apply -r
```

### 파일 인덱싱(업서트)
```bash
python tools/indexer_mariadb.py --apply
```

## 감사 로그
- 로그인, 검색, 미리보기(View), 다운로드(DOWNLOAD) 동작이 `audit_logs` 테이블에 기록됩니다.

## 참고
- PDF 파일은 `config.json`의 `doc_root` 경로 아래에 위치해야 하며, DB에는 해당 경로에 대한 상대 경로가 저장됩니다.
- `DOCVIEWER_CONFIG` 환경 변수를 지정하면 다른 위치의 설정 파일을 사용할 수 있습니다.
