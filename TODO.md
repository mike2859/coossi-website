# Backlog COOSSI

## Prestations – pages détaillées
- [ ] 1 JSON par prestation (Coordination SSI, Notices, Plans, Audit, Dossiers…)
- [ ] 1 composant Razor par page (lecture via `IContentStore`)
- [ ] Breadcrumb + SEO sur chaque page

## Breadcrumb global
- [ ] Composant `<Breadcrumb>` réutilisable
- [ ] Intégration sur toutes les pages (Accueil, Services, Prestations*, Références, Confiance, Contact, Mentions, Confidentialité)

## Composants Avis
- [ ] `TestimonialCard` + `TestimonialsGrid`
- [ ] Emplacements: Accueil, Références, Prestations (bas de page)

## SEO
- [ ] Composant `<Seo>` (PageTitle + meta description + OG/Twitter + canonical)
- [ ] Balises complétées sur toutes les pages
- [ ] Images OG (WebP ou PNG) par page
- [ ] Minifier css
- [ ] Ooutil Crawler en local

## Bonus utilitaires (tech/SEO)
- [ ] `sitemap.xml` (statiques principales)
- [ ] `robots.txt` (inclut lien vers sitemap)
- [ ] **Structured data**: `LocalBusiness`, `BreadcrumbList`, `WebSite` (SearchAction si besoin)
- [ ] Pages **404** et **500** dédiées
- [ ] Logos/illustrations en **WebP/SVG**, favicons en PNG multiples tailles

## Mettre sur Git
- [ ] Init repo + `.gitignore` (.vs, bin/, obj/, wwwroot/lib…)
- [ ] Branch `main` + `dev`
- [ ] Commits init + README (build, run, structure Content/)

## Crawler local
- [ ] Crawl interne (liens cassés / 404 / redirections)
- [ ] Export rapport (URLs, titres, H1, canonicals, status)

## Vérif globale
- [ ] Passer Lighthouse (Perf/SEO/A11y/Best Practices)
- [ ] Vérifier erreurs console et 404 en navigation
- [ ] Audit accessibilité rapide (contrastes, alt, tab order)

## CSS – optimisation
- [ ] Minifier & purger (supprimer classes non utilisées)
- [ ] Variables et tokens (spacing, radius, couleurs) centralisés
- [ ] Regrouper styles de pages dynamiques (Services, Réal., Confiance, Contact)

## Factoriser composants
- [ ] `PageHeader`, `Section`, `IconBadge`, `InfoBox`, `AlertInfo`
- [ ] `List/FeatureList`, `CardsGrid`, `CTA`
- [ ] `PageShell` (header + breadcrumb + container)

## Contenus 100% JSON
- [ ] Vérifier que TOUT le texte des pages vient d’un `*.json`
- [ ] Tests de désérialisation (unit) pour éviter les régressions
- [ ] Schéma minimal (types/kind) documenté

## Idées/Tasks supplémentaires utiles
- [ ] **Form Contact**: brancher envoi (API/mail), anti-spam (honeypot ou hCaptcha), messages de succès/erreur
- [ ] **CI** (GitHub Actions/GitLab): build + test + lint + export sitemap
- [ ] **Cache/Headers**: `Cache-Control` pour assets, `ETag`, compression Gzip/Brotli
- [ ] **Security headers**: CSP, HSTS, X-Content-Type-Options, Referrer-Policy
- [ ] **Redirects**: anciennes URLs → nouvelles (éviter 404 SEO)
- [ ] **Images**: lazy-load, dimensions explicites, `width/height`, `loading="lazy"`
- [ ] **Perf budget**: objectif <100 KB CSS, <200 KB JS init
- [ ] **Manifest** PWA minimal + `apple-touch-icon`
- [ ] **Print CSS** pour Mentions/Confidentialité
- [ ] **Analytics** (Matomo/GA4) + consent UX (sans cookies marketing si tu préfères)