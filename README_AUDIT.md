
# Audit & Lancement du projet

Ce document sert de base pour analyser et nettoyer le projet (composants, CSS, SEO, accessibilité, performances) et facilite mon audit.

---

## 1) Prérequis

- **SDK .NET** (version du projet) — vérifier dans le `.csproj` la balise `<TargetFramework>` (ex. `net8.0`, `net7.0`, `net6.0`).
- **IDE** : Visual Studio 2022+, Rider, ou VS Code.
- (Optionnel) **Node.js 18+** pour purge/lint CSS.
- (Optionnel) **Git** en CLI.

---

## 2) Cloner & démarrer

### Option A — Visual Studio
1. Cloner le dépôt.
2. Ouvrir la solution dans **Visual Studio 2022+**.
3. Restaurer les packages NuGet (VS le propose automatiquement).
4. Lancer en **Debug** (`F5`).  
   L’URL locale s’affiche dans la console (ex. `https://localhost:7xxx`).

### Option B — CLI (recommandé pour l’audit)
```bash
git clone <URL_DU_DEPOT>
cd <DOSSIER_DU_DEPOT>

# Restauration
dotnet restore

# Run (dev)
dotnet watch run
# ou
dotnet run --configuration Debug
```

---

## 3) Build & Publish

### Build Release
```bash
dotnet build --configuration Release
```

### Publish
```bash
# Remplacer <tfm> par la valeur de <TargetFramework> (ex: net8.0)
dotnet publish -c Release -o ./publish
# Le site publié se trouve dans ./publish
```

> Via Visual Studio : **Build > Publish** et suivre l’assistant (IIS, dossier, etc.).  
> En CLI, préciser `-r <RID>` pour un exécutable self-contained (ex: `-r win-x64`).

---

## 4) Stack technique (déclarée)

- **Blazor Server (Hybride)**
- **Bootstrap** (grille, helpers)
- **Font Awesome** (icônes)

> Si les icônes n’apparaissent pas : vérifier l’inclusion de **FA6**.  
> Utiliser `fa-solid` (FA6). Si le code utilise `fas` (FA5), ajouter le shim/compatibilité FA5 ou migrer les classes.

---

## 5) Données & contenu

- Les **titres**, **méta-descriptions** et certains **textes** doivent être **chargés depuis le JSON** (pas codés en dur).  
- Objectif SEO : titre/méta cohérents par page, pilotés par données.

---

## 6) Scripts (optionnels) pour audit CSS

Ajouter dans `package.json` (à la racine ou sous `wwwroot` selon organisation) :

```json
{
  "scripts": {
    "purge:css": "purgecss --css wwwroot/css/site.css --content **/*.razor **/*.cshtml **/*.html --safelist '/^fa-/' '/^col-/' '/^row$/' '/^btn/' --output wwwroot/css/",
    "lint:css": "npx stylelint \"**/*.css\""
  },
  "devDependencies": {
    "purgecss": "^6.0.0",
    "stylelint": "^16.8.0",
    "stylelint-config-standard": "^36.0.0"
  }
}
```

Exécution :
```bash
npm i           # première installation
npm run purge:css
npm run lint:css
```

- **Safelist** : indispensable pour ne pas supprimer les classes dynamiques (Font Awesome, Bootstrap).
- Toujours **vérifier visuellement** le site après purge.

---

## 7) Pages / Domaines prioritaires

### Accueil — Section “Expertise”
- **Composant dédié** : `ExpertiseCard` (props : `Icon`, `Title`, `Text`, `Href?`, `Target?`).  
- **CSS** : scoper les règles sous `.expertise-section` pour éviter les collisions (ex. `.expertise-section .expertise-item`), supprimer doublons.
- **Icônes** : uniformiser `fa-solid fa-...` (FA6).

### SEO global
- **<title>** et **meta-description** **depuis JSON**.  
- **Hn** : un seul `h1` par page, sous-titres cohérents (`h2/h3`).  
- **Open Graph / Twitter Cards** par défaut.

### Modèles / ViewModels
- Clarifier **Models** (domaine), **DTOs** (transport) et **ViewModels** (UI).  
- Activer **nullability** et corriger les avertissements.

### Accessibilité
- Contrastes suffisants (fond sombre / texte).  
- **Alt** sur images, landmarks ARIA (`<main>`, `<nav>`, `<footer>`).  
- Styles de **focus** visibles.

### Performances
- Images en **WebP/AVIF** si possible, `loading="lazy"`.  
- **Minification** CSS/JS, cache long (hash de fichier).  
- Charger FA et Bootstrap une seule fois via layout, en **CDN** ou assets locaux versionnés.

---

## 8) Composants à factoriser (proposition)

- `Section` (props : `Title`, `Subtitle`, `Icon?`, `Theme="light|dark|brand"`, `Padding?`) — slots pour contenu.
- `Card` (variants : `elevated|flat`, `hover`, `linkable`).  
- `Grid` (API simple pour colonnes responsives).  
- `Icon` (normalise FA6, taille, aria-label).  
- `Heading` (gère niveaux Hn + styles).  
- `Button` (variants : `primary|secondary|ghost`, `size`).  
- `ExpertiseCard` (spécifique à la page d’accueil).

> Supprimer/archiver les composants **non utilisés** pour réduire la surface technique.

---

## 9) Qualité .NET (recommandé)

Dans le `.csproj` principal :

```xml
<PropertyGroup>
  <Nullable>enable</Nullable>
  <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
  <AnalysisLevel>latest</AnalysisLevel>
</PropertyGroup>

<ItemGroup>
  <PackageReference Include="Microsoft.CodeAnalysis.NetAnalyzers" Version="8.0.0" PrivateAssets="all" />
  <PackageReference Include="Meziantou.Analyzer" Version="2.0.146" PrivateAssets="all" />
</ItemGroup>
```

Exécuter régulièrement :
```bash
dotnet format
dotnet build -warnaserror
```

---

## 10) CI minimal (GitHub Actions)

Créer `.github/workflows/ci.yml` :

```yaml
name: CI

on:
  push:
  pull_request:

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-dotnet@v4
        with:
          dotnet-version: "8.0.x"
      - run: dotnet restore
      - run: dotnet build -c Release -warnaserror

  css-lint:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-node@v4
        with:
          node-version: 22
      - run: npm ci || npm i
      - run: npx stylelint "**/*.css" || true
```

---

## 11) Flux de travail d’audit (proposition)

1. Créer la branche : `audit/cleanup-YYYY-MM`.
2. Ouvrir des **issues** thématiques (icônes FA, factorisation `ExpertiseCard`, titres/méta par JSON, purge CSS, accessibilité).
3. Faire des **PRs atomiques** (1 thème = 1 PR) pour faciliter la relecture/rollback.
4. Après validation visuelle, **purge CSS** et suppression composants inutilisés.

---

## 12) Dépannage rapide

- **Icônes invisibles** : vérifier FA6 chargé, classes `fa-solid fa-...` et que les noms d’icônes existent.  
- **Styles “cassés”** : conflits de sélecteurs (ex. `.expertise-item` dupliqué) → scoper par section.  
- **Port indisponible** : changer via `dotnet run --urls "http://localhost:5173;https://localhost:7173"`.  
- **403/404 en prod** (IIS) : vérifier `_Host.cshtml`/`blazor.config` et réécritures d’URL.  
- **CDN bloqué** : preferer assets locaux versionnés.

---

## 13) Ce que j’analyserai en priorité

- Chargement Font Awesome + migration FA6.  
- Factorisation `ExpertiseCard` et `Section`.  
- Titres & Méta **depuis JSON** sur toutes les pages.  
- Purge des classes CSS non utilisées.  
- Hiérarchie Hn / Accessibilité (alt, landmarks, focus).  
- Images (poids, lazy, formats).  
- Analyzers + warnings as errors.


---

> **Note** : si le dépôt est privé, crée une branche publique temporaire (lecture seule) : `audit/cleanup-YYYY-MM`. Donne-moi l’URL + la branche à auditer.
