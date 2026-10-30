#!/usr/bin/env bash
# Publie la version poussée sur GitHub (branche main) vers le VPS.
# Usage, depuis le PC : ./deploy/publier.sh
set -euo pipefail

HOTE_SSH="${HOTE_SSH:-vps-coossi}"
URL_SITE="https://www.coossi-securite-incendie.eu/"

cd "$(git rev-parse --show-toplevel)"

# 1. Le serveur récupère GitHub : tout doit être commité et poussé
if [ -n "$(git status --porcelain --untracked-files=no)" ]; then
    echo "✗ Modifications non commitées :" >&2
    git status --short --untracked-files=no >&2
    exit 1
fi
if [ "$(git branch --show-current)" != "main" ]; then
    echo "✗ Il faut être sur la branche main." >&2
    exit 1
fi
git fetch -q origin main
if [ "$(git rev-parse HEAD)" != "$(git rev-parse origin/main)" ]; then
    echo "✗ main n'est pas à jour sur GitHub : fais d'abord « git push » (ou « git pull »)." >&2
    exit 1
fi

VERSION="$(git log -1 --format='%h %s')"
echo "→ Publication de : $VERSION"

# 2. Sur le serveur : récupération, reconstruction, redémarrage
ssh "$HOTE_SSH" 'set -e
    cd /srv/site
    git pull --ff-only -q
    cd deploy
    docker compose up -d --build
    docker image prune -f >/dev/null'

# 3. Vérification
sleep 5
CODE="$(curl -s -o /dev/null -w '%{http_code}' --max-time 20 "$URL_SITE" || true)"
if [ "$CODE" = "200" ]; then
    echo "✓ Site en ligne ($URL_SITE) — version $VERSION"
else
    echo "✗ Le site répond $CODE : voir « ssh $HOTE_SSH 'cd /srv/site/deploy && docker compose logs --tail 50 app' »" >&2
    exit 1
fi
