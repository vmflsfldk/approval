import argparse
import re
from pathlib import Path
from typing import Dict

TITLE_SANITIZE_PATTERN = re.compile(r"^(인쇄\s*옵션\s*-?|인쇄옵션\s*-?)", re.IGNORECASE)


def clean_name(stem: str) -> str:
    cleaned = TITLE_SANITIZE_PATTERN.sub("", stem).strip()
    return re.sub(r"\s+", " ", cleaned)


def ensure_unique(target: Path) -> Path:
    if not target.exists():
        return target
    base = target.stem
    suffix = target.suffix
    counter = 2
    while True:
        candidate = target.with_name(f"{base} ({counter}){suffix}")
        if not candidate.exists():
            return candidate
        counter += 1


def process_file(file_path: Path, root: Path, apply: bool) -> Dict[str, Path]:
    stem = file_path.stem
    cleaned_stem = clean_name(stem)
    parts = cleaned_stem.split("-", 1)
    title = parts[0].strip() if parts else cleaned_stem
    if not title:
        title = "untitled"
    new_name = cleaned_stem + file_path.suffix
    target_dir = root / title
    if apply:
        target_dir.mkdir(parents=True, exist_ok=True)
    target_path = ensure_unique(target_dir / new_name)
    if apply:
        target_path = ensure_unique(target_dir / new_name)
        target_path.parent.mkdir(parents=True, exist_ok=True)
        if file_path.resolve() != target_path.resolve():
            file_path.rename(target_path)
    return {"source": file_path, "target": target_path}


def iterate_files(root: Path, recursive: bool):
    pattern = "**/*.pdf" if recursive else "*.pdf"
    yield from root.glob(pattern)


def main():
    parser = argparse.ArgumentParser(description="Classify PDF files into title-based folders")
    parser.add_argument("root", type=Path, help="Root directory containing PDF files")
    parser.add_argument("--apply", action="store_true", help="Apply changes (default: dry-run)")
    parser.add_argument("-r", "--recursive", action="store_true", help="Search files recursively")
    args = parser.parse_args()

    root: Path = args.root
    if not root.exists():
        raise SystemExit(f"Root not found: {root}")

    for pdf_file in iterate_files(root, args.recursive):
        if not pdf_file.is_file():
            continue
        result = process_file(pdf_file, root, args.apply)
        if args.apply:
            print(f"Moved: {result['source']} -> {result['target']}")
        else:
            print(f"DRY-RUN: {result['source']} -> {result['target']}")


if __name__ == "__main__":
    main()
