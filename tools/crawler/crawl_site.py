#!/usr/bin/env python3
"""
Crawler pour analyser https://www.coossi-securite-incendie.eu/
et lister tous les liens internes et externes du site.

Usage:
    python3 crawl_site.py
    python3 crawl_site.py --max-pages 200
    python3 crawl_site.py --output rapport.json
"""

import argparse
import json
import sys
import time
from collections import defaultdict
from urllib.parse import urljoin, urlparse

import requests
from bs4 import BeautifulSoup

BASE_URL = "https://www.coossi-securite-incendie.eu/"
DOMAIN = "www.coossi-securite-incendie.eu"
ALT_DOMAINS = [
    "coossi-securite-incendie.eu",
    "www.coossi-securite-incendie.fr",
    "coossi-securite-incendie.fr",
]

HEADERS = {
    "User-Agent": (
        "Mozilla/5.0 (compatible; CoossiCrawler/1.0; "
        "+https://www.coossi-securite-incendie.fr)"
    )
}

IGNORED_EXTENSIONS = {
    ".pdf", ".jpg", ".jpeg", ".png", ".gif", ".svg", ".webp",
    ".mp4", ".mp3", ".zip", ".rar", ".doc", ".docx", ".xls",
    ".xlsx", ".ppt", ".pptx", ".css", ".js", ".ico", ".woff",
    ".woff2", ".ttf", ".eot",
}


def is_internal(url: str) -> bool:
    parsed = urlparse(url)
    host = parsed.hostname or ""
    return host == DOMAIN or host in ALT_DOMAINS


def normalize_url(url: str) -> str:
    parsed = urlparse(url)
    path = parsed.path.rstrip("/") or "/"
    return f"{parsed.scheme}://{parsed.hostname}{path}"


def should_skip(url: str) -> bool:
    parsed = urlparse(url)
    if parsed.scheme not in ("http", "https", ""):
        return True
    path_lower = parsed.path.lower()
    return any(path_lower.endswith(ext) for ext in IGNORED_EXTENSIONS)


def crawl(base_url: str, max_pages: int = 500, delay: float = 0.5):
    visited = set()
    to_visit = {normalize_url(base_url)}
    internal_pages = {}
    external_links = set()
    broken_links = {}
    redirects = {}
    link_sources = defaultdict(set)  # url -> set of pages linking to it

    session = requests.Session()
    session.headers.update(HEADERS)

    print(f"Crawl de {base_url}")
    print(f"Pages max : {max_pages}")
    print("-" * 60)

    while to_visit and len(visited) < max_pages:
        url = to_visit.pop()
        if url in visited:
            continue
        visited.add(url)

        try:
            resp = session.get(url, timeout=15, allow_redirects=True)
            status = resp.status_code

            # Track redirects
            if resp.history:
                original = url
                final = resp.url
                if normalize_url(final) != normalize_url(original):
                    redirects[original] = {
                        "final_url": resp.url,
                        "status_chain": [r.status_code for r in resp.history],
                    }

            content_type = resp.headers.get("content-type", "")
            if "text/html" not in content_type:
                continue

            print(f"  [{status}] {url}")

            if status >= 400:
                broken_links[url] = status
                continue

            soup = BeautifulSoup(resp.text, "html.parser")

            title_tag = soup.find("title")
            title = title_tag.get_text(strip=True) if title_tag else "(sans titre)"

            meta_desc = ""
            meta_tag = soup.find("meta", attrs={"name": "description"})
            if meta_tag:
                meta_desc = meta_tag.get("content", "")

            h1_tags = [h1.get_text(strip=True) for h1 in soup.find_all("h1")]

            internal_pages[url] = {
                "title": title,
                "meta_description": meta_desc,
                "h1": h1_tags,
                "status": status,
            }

            # Extract all links
            for tag in soup.find_all("a", href=True):
                href = tag["href"].strip()
                if not href or href.startswith("#") or href.startswith("javascript:"):
                    continue

                absolute = urljoin(url, href)
                absolute_clean = normalize_url(absolute)

                link_sources[absolute_clean].add(url)

                if is_internal(absolute):
                    if not should_skip(absolute) and absolute_clean not in visited:
                        to_visit.add(absolute_clean)
                else:
                    external_links.add(absolute)

        except requests.RequestException as e:
            print(f"  [ERR] {url} -> {e}")
            broken_links[url] = str(e)

        time.sleep(delay)

    return {
        "internal_pages": internal_pages,
        "external_links": sorted(external_links),
        "broken_links": broken_links,
        "redirects": redirects,
        "link_sources": {k: sorted(v) for k, v in link_sources.items()},
    }


def print_report(data: dict):
    pages = data["internal_pages"]
    external = data["external_links"]
    broken = data["broken_links"]
    redirs = data["redirects"]

    print("\n" + "=" * 60)
    print("RAPPORT DE CRAWL")
    print("=" * 60)

    # -- Pages internes --
    print(f"\n--- PAGES INTERNES ({len(pages)}) ---\n")
    for url, info in sorted(pages.items()):
        print(f"  URL:    {url}")
        print(f"  Titre:  {info['title']}")
        if info["h1"]:
            print(f"  H1:     {', '.join(info['h1'])}")
        if info["meta_description"]:
            desc = info["meta_description"][:100]
            print(f"  Desc:   {desc}...")
        print()

    # -- Liens externes --
    print(f"\n--- LIENS EXTERNES ({len(external)}) ---\n")
    for link in external:
        print(f"  {link}")

    # -- Liens cassés --
    if broken:
        print(f"\n--- LIENS CASSES ({len(broken)}) ---\n")
        for url, status in broken.items():
            print(f"  [{status}] {url}")

    # -- Redirections --
    if redirs:
        print(f"\n--- REDIRECTIONS ({len(redirs)}) ---\n")
        for original, info in redirs.items():
            chain = " -> ".join(str(s) for s in info["status_chain"])
            print(f"  {original}")
            print(f"    -> {info['final_url']}  (chain: {chain})")
            print()

    # -- Resume --
    print("\n--- RESUME ---\n")
    print(f"  Pages internes crawlees : {len(pages)}")
    print(f"  Liens externes trouves : {len(external)}")
    print(f"  Liens casses            : {len(broken)}")
    print(f"  Redirections detectees  : {len(redirs)}")
    print()


def main():
    parser = argparse.ArgumentParser(
        description="Crawler du site coossi-securite-incendie.eu"
    )
    parser.add_argument(
        "--max-pages", type=int, default=500, help="Nombre max de pages a crawler"
    )
    parser.add_argument(
        "--delay", type=float, default=0.5, help="Delai entre requetes (secondes)"
    )
    parser.add_argument(
        "--output", type=str, default="crawl_results.json",
        help="Fichier JSON de sortie"
    )
    args = parser.parse_args()

    data = crawl(BASE_URL, max_pages=args.max_pages, delay=args.delay)

    print_report(data)

    # Export JSON
    output_path = args.output
    with open(output_path, "w", encoding="utf-8") as f:
        json.dump(data, f, ensure_ascii=False, indent=2)

    print(f"Resultats exportes dans : {output_path}")


if __name__ == "__main__":
    main()
