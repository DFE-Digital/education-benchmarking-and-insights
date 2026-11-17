---
title: "Intent: Add Pagefind Search to Eleventy Documentation Site"
feature: "docs-pagefind-search"
created: 2026-11-17
updated: 2026-11-17
---

## Objective

Deliver fast, accessible, zero-backend static full-text search across the FBIT Eleventy documentation site (`docs/`) using Pagefind, supporting Diátaxis quadrant filtering, sub-heading deep links, keyboard-accessible quick-search modal navigation, progressive enhancement, and GitHub Pages subpath deployment.

## Boundaries & Scope Constraints

* **Untouchable:**
  * Application runtime modules outside `docs/` (`web/`, `platform/`, `front-end-components/`, `data-pipeline/`, `core-infrastructure/`, `support-analytics/`, `pipelines/`).
  * `@x-govuk/govuk-eleventy-plugin` core layout templates (`base.njk`, `page.njk`, `sub-navigation.njk`, `collection.njk`) inside `node_modules/`; do not copy full plugin layouts into `docs/content/_includes/` when `mainAttributes` and `excludeSelectors` achieve the required markup cleanly.
  * Supply-chain protection flag `--ignore-scripts` in `.github/workflows/docs-ci-cd.yml`.
* **Scope Limits:**
  * Only modify files under `docs/`, `.github/workflows/docs-ci-cd.yml`, and `.memory/`.
  * Do not create any UI screens or modals beyond the header search trigger, the `<dialog id="pagefind-search-dialog">` quick-search modal, and the dedicated `/search/` page defined in `proposal.md`.

## Technical Constraints

* **Dependencies:**
  * Add `pagefind` (`^1.6.0`) to `devDependencies` in `docs/package.json`.
  * Use Node.js built-in test runner (`node:test` and `node:assert/strict`) for automated verification in `docs/` so test execution requires minimal or zero heavy external test runners.
* **Performance:**
  * Initial documentation page loads (excluding `/search/`) MUST NOT download `search-index.json`, `pagefind.js`, `pagefind-ui.js`, or `.pf_index` / `.pf_fragment` chunks until the user activates the header search trigger or presses the search shortcut.
  * Static build indexing step in `eleventy.after` MUST complete in under 10 seconds on CI (`ubuntu-latest`, Node 22).
* **Compatibility & Accessibility:**
  * Must support Node.js 22+ in CI and modern evergreen browsers supporting ES modules, `<dialog>`, and WebAssembly.
  * Must satisfy WCAG 2.2 Level AA requirements (keyboard operability, visible GOV.UK focus indicators, accessible names/roles, focus restoration on modal close, no keyboard traps, and progressive enhancement fallback when JavaScript is disabled).
  * Must work identically when served at `/` (`npm run start`) and `/education-benchmarking-and-insights/` (`ELEVENTY_ENV=production npm run build`).

## Data Definitions & Schemas

### Database Schema Changes

* **New Tables / Collections:** None (static documentation site with build-time file-based WebAssembly search index).
* **Modified Tables / Collections:** None.
* **Data Constraints:** None.

### Code Data Structures

* **Eleventy Page & Computed Data (`docs/content/content.11tydata.js`):**

  ```ts
  interface DocsPageData {
    title?: string;
    description?: string;
    layout?: string;
    sectionKey?: 'Tutorials' | 'How-to guides' | 'Reference' | 'Explanation';
    eleventyExcludeFromSearch?: boolean;
    mainAttributes?: Record<
      string,
      string | { value: string | boolean; optional?: boolean }
    >;
  }
  ```

* **Pagefind Build Configuration (`docs/eleventy.config.js`):**

  ```ts
  interface DocsPagefindBuildConfig {
    forceLanguage: 'en';
    excludeSelectors: [
      '.x-govuk-sub-navigation',
      '.govuk-pagination',
      '.x-govuk-related-navigation',
      '[data-pagefind-ignore]'
    ];
  }
  ```

* **Client Search Controller Configuration & State (`docs/content/assets/js/pagefind-search.js`):**

  ```ts
  interface PagefindClientConfig {
    bundlePath: string; // "/" -> "/pagefind/", prod -> "/education-benchmarking-and-insights/pagefind/"
    baseUrl: string;    // "/" -> "/", prod -> "/education-benchmarking-and-insights/"
    showSubResults: true;
    showImages: false;
    resetStyles: false;
  }

  interface SearchControllerState {
    modalInitialised: boolean;
    pageInitialised: boolean;
    uiScriptLoaded: Promise<void> | null;
    lastTriggerElement: HTMLElement | null;
  }
  ```

* **DOM Attribute Contracts:**
  * Indexable content `<main>` element: `id="main-content" data-pagefind-body` plus optional `data-pagefind-filter="section:{sectionKey}"`.
  * Non-indexed page `<main>` element (`/404.html`, `/sitemap/`, `/search/`): `id="main-content" data-pagefind-ignore="all"` (no `data-pagefind-body`).
  * Header trigger button: `id="header-search-button" type="button" aria-haspopup="dialog" aria-controls="pagefind-search-dialog"`.
  * Modal container: `<dialog id="pagefind-search-dialog" class="app-pagefind-dialog" aria-labelledby="pagefind-dialog-title">`.
  * Dedicated search mount: `<div id="pagefind-page-search">`.
* **Type/Schema Verification:** Must align with `docs/node_modules/pagefind/types/index.d.ts` and `docs/node_modules/govuk-frontend/dist/govuk/macros/attributes.njk`.

## API & Integration Contracts

### Endpoints (Static HTTP Routes & Assets)

| Method | Path / Operation | Description | Auth Required |
| :--- | :--- | :--- | :--- |
| `GET` | `{pathPrefix}search/` | Dedicated HTML search page supporting optional `?q=` query string | No |
| `GET` | `{pathPrefix}pagefind/pagefind-entry.json` | Pagefind index entry manifest listing language hashes and page count | No |
| `GET` | `{pathPrefix}pagefind/pagefind.js` | Pagefind core ES module loader | No |
| `GET` | `{pathPrefix}pagefind/pagefind-ui.js` | Pagefind UI widget script loaded lazily by `pagefind-search.js` | No |
| `GET` | `{pathPrefix}pagefind/pagefind-ui.css` | Base Pagefind UI stylesheet | No |
| `GET` | `{pathPrefix}pagefind/wasm.en.pagefind` | Compressed WebAssembly search runtime | No |
| `GET` | `{pathPrefix}pagefind/index/*.pf_index` | Chunked word index files fetched on demand per query prefix | No |
| `GET` | `{pathPrefix}pagefind/fragment/*.pf_fragment` | Compressed page metadata and excerpt fragments | No |

* **Headers & Query Parameters:**
  * `q` (optional URL query parameter on `{pathPrefix}search/?q={query}`, UTF-8 string, max processed length capped at 256 characters client-side before passing to `triggerSearch` to prevent pathological URL inputs).

### Payload Contracts

* **Request Payloads:** None (GET requests for static assets only).
* **Response Payloads (`{pathPrefix}pagefind/pagefind-entry.json`):**

  ```json
  {
    "version": "1.6.0",
    "languages": {
      "en": {
        "hash": "en_examplehash",
        "wasm": "en",
        "page_count": 95
      }
    }
  }
  ```

* **Error Responses:**
  * Static `404 Not Found` (`{pathPrefix}404.html`) if an invalid route or missing asset is requested; client controller catches asset load errors and renders a user-readable fallback message with links to `{pathPrefix}sitemap/`.

### Webhooks & Messaging

* **Publish/Subscribe Event Schemas:** None.
* **Third-Party Integrations:** None.

## Validation Rules

### Test Sets (Input/Output Rulesets)

* **TS-01 (Build Output):** Given a clean build (`npm run build`), `_site/pagefind/pagefind-entry.json`, `_site/pagefind/pagefind.js`, `_site/pagefind/pagefind-ui.js`, and `_site/pagefind/pagefind-ui.css` MUST exist on disk and `languages.en.page_count` MUST be greater than 0.
* **TS-02 (Legacy Index Removal):** Given a clean build (`npm run build`), `_site/search-index.json` MUST NOT exist in `_site/`.
* **TS-03 (Diátaxis Filters):** Given the generated Pagefind index, querying available filters via Pagefind's JS API (`pagefind.filters()`) MUST return `section` keys containing `'Tutorials'`, `'How-to guides'`, `'Reference'`, and `'Explanation'`, each with a count >= 1.
* **TS-04 (Subpath Prefix Resolution):** Given `ELEVENTY_ENV=production npm run build`, `_site/index.html` and `_site/search/index.html` MUST reference `/education-benchmarking-and-insights/pagefind/pagefind-ui.css` and `/education-benchmarking-and-insights/assets/js/pagefind-search.js`, and computing `bundlePath` / `baseUrl` from that script URL MUST yield `/education-benchmarking-and-insights/pagefind/` and `/education-benchmarking-and-insights/`.
* **TS-05 (Query Parameter Hydration):** Given navigation to `/search/?q=databricks`, the search controller MUST initialise `PagefindUI` and invoke `triggerSearch('databricks')` using plain text (never injecting raw query strings via `innerHTML`).

### Properties (Universally Quantifiable Predicates)

* **PROP-01 (Content-Only Indexing):** For every HTML file in `_site/` corresponding to `/404.html`, `/sitemap/index.html`, and `/search/index.html`, the `<main>` element MUST NOT carry `data-pagefind-body` and MUST carry `data-pagefind-ignore="all"`.
* **PROP-02 (Sidebar Noise Exclusion):** Searching Pagefind for text that appears exclusively in global footer licence text (`Open Government Licence v3.0`) MUST return 0 results, and sidebar navigation links MUST NOT inflate page body excerpts.
* **PROP-03 (Safe Query Handling):** For all arbitrary URL query strings `?q=...` (including `<script>alert(1)</script>` and regex metacharacters `[(+*?\`), no script execution or unhandled exception occurs.

### Contracts (Preconditions, Invariants, Postconditions)

* **CON-01 (Build Lifecycle Contract):**
  * Precondition: Eleventy completes HTML template rendering into `dir.output`.
  * Invariant: `eleventy.after` indexes `dir.output` without mutating source Markdown files in `docs/content/`.
  * Postcondition: `pagefind.close()` terminates the background Pagefind binary process so `npm run build` exits with code `0`.
* **CON-02 (Modal Focus Contract):**
  * Precondition: User activates `#header-search-button` or keyboard shortcut (`/` or `Ctrl+K` / `Cmd+K`).
  * Invariant: Modal `<dialog>` traps tab navigation inside the modal while open and closes on `Escape`.
  * Postcondition: Closing the modal restores `document.activeElement` to `#header-search-button` (or the previously focused trigger element).

## Measurable Success Criteria

* `ELEVENTY_ENV=production npm run build` and `npm test` in `docs/` exit with code `0` with zero errors.
* Zero bytes of search index data (`search-index.json` or `.pf_index`) are fetched during initial load of non-search documentation pages.
* 100% of Diátaxis quadrants (`Tutorials`, `How-to guides`, `Reference`, `Explanation`) appear as selectable `section` filters with accurate document counts.
* Zero `markdownlint-cli2` violations across all new or modified `.md` files.

## Implementation Directives

* Run `npm run build` and `npm test` inside `docs/` after completing each task group.
* Run `ELEVENTY_ENV=production npm run build` inside `docs/` to verify production subpath output before concluding the PR.
* Validate all modified Markdown files (`docs/content/search.md`, `docs/README.md`, and `.memory/**/*.md`) against `.markdownlint-cli2.jsonc` rules and UK English spelling standards (no em dashes, no horizontal rules).
