#!/usr/bin/env python3
"""
Compare les URLs du crawl avec les routes du projet Blazor.
Identifie les URLs de l'ancien site absentes du nouveau.
"""

import json
import re
from urllib.parse import urlparse

CRAWL_FILE = "crawl_results.json"

# Routes gerees par le nouveau site Blazor
BLAZOR_ROUTES = {
    "/",
    "/home",
    "/services",
    "/prestations",
    "/prestation/coordination-ssi",
    "/prestation/creation-dossier-ssi",
    "/prestation/assistance-moe",
    "/prestation/notices-securite-accessibilite",
    "/prestation/audit-diagnostic",
    "/prestation/document-unique-evaluation",
    "/prestation/responsable-unique-securite",
    "/prestation/signaletique",
    "/references",
    "/ils-nous-font-confiance",
    "/contact",
    "/mentions-legales",
    "/confidentialite",
    "/Error",
    "/robots.txt",
    "/sitemap.xml",
}

# Redirections legacy configurees dans Program.cs
LEGACY_REDIRECTS = {
    "/temoignages": "/references",
    "/prestation/5": "/prestation/coordination-ssi",
    "/prestation/6": "/prestation/creation-dossier-ssi",
    "/prestation/7": "/prestation/audit-diagnostic",
    "/prestation/8": "/prestation/assistance-moe",
    "/prestation/9": "/prestation/notices-securite-accessibilite",
    "/prestation/10": "/prestation/document-unique-evaluation",
    "/prestation/11": "/prestation/responsable-unique-securite",
    "/prestation/12": "/prestation/signaletique",
}

# Pattern pour les redirections dynamiques /prestation/{id:int}
PRESTATION_ID_PATTERN = re.compile(r"^/prestation/\d+$")


def is_handled(path: str) -> bool:
    """Verifie si un chemin est gere par le nouveau site."""
    normalized = path.rstrip("/") or "/"

    if normalized in BLAZOR_ROUTES:
        return True

    if normalized in LEGACY_REDIRECTS:
        return True

    # /prestation/{id:int} redirige vers le slug correspondant
    if PRESTATION_ID_PATTERN.match(normalized):
        return True

    return False


def main():
    with open(CRAWL_FILE, "r", encoding="utf-8") as f:
        data = json.load(f)

    internal_pages = data.get("internal_pages", {})
    external_links = data.get("external_links", [])

    # Extraire les chemins des URLs internes
    all_paths = {}
    for url, info in internal_pages.items():
        parsed = urlparse(url)
        path = parsed.path.rstrip("/") or "/"
        all_paths[path] = {
            "url": url,
            "title": info.get("title", ""),
            "status": info.get("status", ""),
        }

    handled = {}
    missing = {}

    for path, info in sorted(all_paths.items()):
        if is_handled(path):
            handled[path] = info
        else:
            missing[path] = info

    # Regrouper les URLs manquantes par categorie
    categories = {}
    for path in sorted(missing.keys()):
        parts = path.strip("/").split("/")
        if len(parts) >= 2 and parts[0] == "page":
            cat = parts[1]
        else:
            cat = "autre"
        categories.setdefault(cat, []).append(path)

    # Rapport
    print("=" * 60)
    print("COMPARAISON ANCIEN SITE vs NOUVEAU PROJET")
    print("=" * 60)

    print(f"\nTotal URLs crawlees    : {len(all_paths)}")
    print(f"URLs gerees (OK)      : {len(handled)}")
    print(f"URLs MANQUANTES       : {len(missing)}")

    print(f"\n--- URLS GEREES ({len(handled)}) ---\n")
    for path, info in sorted(handled.items()):
        redirect = ""
        if path in LEGACY_REDIRECTS:
            redirect = f" -> {LEGACY_REDIRECTS[path]}"
        elif PRESTATION_ID_PATTERN.match(path) and path not in BLAZOR_ROUTES:
            redirect = " -> redirection dynamique"
        status = "OK" if not redirect else f"REDIRECT{redirect}"
        print(f"  [{status}] {path}")
        if info["title"]:
            print(f"           Titre: {info['title']}")

    print(f"\n--- URLS MANQUANTES ({len(missing)}) ---\n")
    for cat, paths in sorted(categories.items()):
        print(f"  [{cat}] ({len(paths)} URLs)")
        for path in paths:
            info = missing[path]
            title = info["title"] if info["title"] else "(sans titre)"
            print(f"    {path}  -  {title}")
        print()

    # Export
    report = {
        "total_crawled": len(all_paths),
        "handled": len(handled),
        "missing_count": len(missing),
        "missing_urls": {
            path: info for path, info in sorted(missing.items())
        },
        "missing_by_category": categories,
        "handled_urls": {
            path: info for path, info in sorted(handled.items())
        },
    }

    with open("missing_urls.json", "w", encoding="utf-8") as f:
        json.dump(report, f, ensure_ascii=False, indent=2)

    print(f"\nRapport exporte dans : missing_urls.json")


if __name__ == "__main__":
    main()
