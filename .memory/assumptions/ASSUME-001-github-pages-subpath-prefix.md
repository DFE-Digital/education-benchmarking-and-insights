---
id: ASSUME-001
title: "GitHub Pages Subpath Prefix Configuration"
component: "docs/config"
domain: "documentation"
tags: ["eleventy", "github-pages", "path-prefix", "pagefind"]
status: "validated"
created: 2026-11-17
updated: 2026-11-17
resolved: 2026-11-17
---

## Assumption

Production builds of the Eleventy documentation site run with `ELEVENTY_ENV=production` and are served from the `/education-benchmarking-and-insights/` subpath on GitHub Pages, whereas local development builds serve from `/`.

## Context

`docs/path-prefix.js` sets `pathPrefix` to `"/education-benchmarking-and-insights/"` when `process.env.ELEVENTY_ENV === "production"` and `"/"` otherwise. Pagefind indexes static HTML files directly from `docs/_site/`, where file paths are relative to the site root (for example, `/explanation/architecture/`). Consequently, client-side Pagefind UI instances must receive `bundlePath: "${pathPrefix}pagefind/"` and `baseUrl: "${pathPrefix}"` so index chunk requests and result links resolve accurately in both environments.

## Resolution

Validated on 2026-11-17 via automated build and unit tests (`TC-06`) confirming `ELEVENTY_ENV=production` rewrites HTML asset and link URLs under `/education-benchmarking-and-insights/` and `resolveSearchPaths` / `normalizeResultUrl` resolve `bundlePath` (`/education-benchmarking-and-insights/pagefind/`) and `baseUrl` (`/education-benchmarking-and-insights/`) cleanly.
