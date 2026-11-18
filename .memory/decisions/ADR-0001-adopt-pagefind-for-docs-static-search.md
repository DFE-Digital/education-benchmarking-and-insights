---
id: ADR-0001
title: "Adopt Pagefind via Eleventy After-Build Hook for Documentation Search"
component: "docs/search"
domain: "documentation"
tags: ["eleventy", "pagefind", "static-search", "wasm", "docs"]
status: "proposed"
created: 2026-11-17
updated: 2026-11-17
---

## Summary

**Feature:** docs-pagefind-search  
**Date:** 2026-11-17  
**Deciders:** FBIT Engineering Team  
**Status:** Proposed

## Context

The FBIT technical documentation site (`docs/`) uses Eleventy v3 (`@11ty/eleventy`) and `@x-govuk/govuk-eleventy-plugin` v9, deployed as static files to GitHub Pages.

Currently, `docs/eleventy.config.js` enables the built-in `searchIndex` template from `@x-govuk/govuk-eleventy-plugin`. That template generates a single `search-index.json` file (approximately 311 KB today) that every browser client downloads eagerly on initial page load. Client-side filtering runs `new RegExp(searchQuery, 'gi')` across raw token strings. This approach causes three problems:

1. Unnecessary network transfer on every page load, even when the user does not search.
2. Unhandled `SyntaxError` exceptions when a user enters regular expression metacharacters such as `[`, `(`, `*`, or `\`.
3. No word stemming, relevance ranking, content excerpts, sub-heading deep links, or Diátaxis quadrant filtering.

Because GitHub Pages hosts static files only, the replacement search engine must operate without a dedicated backend search server.

## Options Considered

* **Option 1:** Retain `@x-govuk/govuk-eleventy-plugin` built-in `search-index.json` search.
* **Option 2:** Integrate Pagefind via CLI (`npx pagefind --site _site`) in `package.json` npm scripts.
* **Option 3:** Integrate Pagefind via its Node.js API (`pagefind.createIndex`) inside Eleventy's `eleventy.after` event hook.
* **Option 4:** Integrate a hosted third-party search service (for example, Algolia DocSearch).

## Evaluation

### Option 1: Built-in `search-index.json`

* **Pros:**
  * Zero additional dependencies.
* **Cons:**
  * Downloads the full site index on every page view.
  * Lacks stemming, ranking, highlighted excerpts, sub-heading links, and section filters.
  * Throws runtime regular expression errors on special characters.

### Option 2: Pagefind CLI in `package.json` Scripts

* **Pros:**
  * Simple command-line invocation during production `npm run build`.
* **Cons:**
  * Does not run automatically during `npm run start` (`eleventy --serve`) because `eleventy --serve` blocks as a long-running process, leaving local preview without an up-to-date `/pagefind/` index unless developers run a separate build command first.

### Option 3: Pagefind Node.js API in `eleventy.after` Hook

* **Pros:**
  * Generates chunked WebAssembly (WASM) search indexes only loaded on demand when a user interacts with search.
  * Runs automatically after both `npm run build` and `npm run start` (`eleventy --serve`), keeping local development and CI builds consistent.
  * Allows programmatic configuration (`excludeSelectors`, `forceLanguage`) directly in `docs/eleventy.config.js` and clean shutdown via `pagefind.close()`.
  * Uses optional platform packages (`@pagefind/linux-x64`, `@pagefind/darwin-arm64`), remaining compatible with `npm ci --ignore-scripts` in `.github/workflows/docs-ci-cd.yml`.
* **Cons:**
  * Adds `pagefind` as a `devDependency` in `docs/package.json` and adds approximately 1 to 2 seconds to the static build step.

### Option 4: Hosted Third-Party Search (Algolia DocSearch)

* **Pros:**
  * Fast hosted query execution.
* **Cons:**
  * Introduces external third-party network requests, API key management, crawler delays on PR branches, and Content Security Policy complexity.

## Decision

Adopt Option 3: add `pagefind` as a `devDependency` in `docs/package.json` and invoke Pagefind's Node.js API (`createIndex`, `addDirectory`, `writeFiles`, `close`) inside an `eleventy.after` event listener in `docs/eleventy.config.js`, writing static index bundles to `${dir.output}/pagefind`. Remove the legacy `searchIndex: true` virtual template once the Pagefind UI cutover completes.

## Consequences

### Positive

* Zero search index payload is downloaded until a user opens or focuses the search interface.
* Search supports stemming, relevance ranking, highlighted snippets, sub-heading deep links, and Diátaxis quadrant filtering across both local preview and GitHub Pages.
* No external runtime services, credentials, or cookies are introduced.

### Negative

* Build time increases slightly (1 to 2 seconds) while Pagefind indexes the generated `_site` HTML files.
* Pagefind requires explicit `bundlePath` and `baseUrl` configuration to support GitHub Pages subpath hosting (`/education-benchmarking-and-insights/`).
