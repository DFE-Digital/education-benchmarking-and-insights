---
title: "Add Pagefind Search to Eleventy Documentation Site"
component: "docs/search"
domain: "documentation"
tags: ["eleventy", "pagefind", "govuk", "search", "accessibility", "diataxis"]
created: 2026-11-17
updated: 2026-11-17
---

## Intent

Replace the monolithic client-side `search-index.json` search in the FBIT Eleventy documentation site (`docs/`) with Pagefind static search.

Today, `@x-govuk/govuk-eleventy-plugin` generates a single `search-index.json` file (approximately 311 KB) that every visitor downloads eagerly on every page view and filters via unescaped `new RegExp(searchQuery, 'gi')`. Adopting Pagefind eliminates upfront index downloads, prevents regular expression syntax errors on special characters, and adds stemmed full-text search, relevance ranking, highlighted content excerpts, sub-heading deep links, keyboard-first quick search, and Diátaxis quadrant filtering while keeping the site 100% static on GitHub Pages.

## Security Context

| Attribute | Value |
| :--- | :--- |
| Data Sensitivity | Public (open-source technical documentation; zero PII, credentials, or school-level restricted data) |
| Authentication | None (public static GitHub Pages site) |
| Trust Boundary | Static build pipeline (`docs/` -> GitHub Pages CDN) and client browser DOM (URL query `?q=` and Pagefind WASM/JSON fragments) |
| External Runtime Services | None (self-hosted static `/pagefind/` bundle generated at build time) |

## Scope

### In-Scope

* Add `pagefind` (`^1.6.0`) as a `devDependency` in `docs/package.json`.
* Configure an `eleventy.after` hook in `docs/eleventy.config.js` using Pagefind's Node.js API (`createIndex`, `addDirectory`, `writeFiles`, `close`) so both `npm run build` and `npm run start` generate `${dir.output}/pagefind/`.
* Add semantic Pagefind indexing attributes (`data-pagefind-body` and `data-pagefind-filter="section:..."`) via Eleventy computed `mainAttributes` so only genuine documentation prose is indexed, excluding global chrome, sidebars, pagination, related links, `/404.html`, `/sitemap/`, and `/search/`.
* Create a dedicated `/search/` page (`docs/content/search.md`) supporting `?q=` URL query parameter synchronisation, Diátaxis quadrant facet filters (`Tutorials`, `How-to guides`, `Reference`, `Explanation`), highlighted excerpts, sub-heading deep links, and `<noscript>` fallback navigation.
* Override `components/search/template.njk` in `docs/content/_includes/components/search/template.njk` and add `docs/content/assets/js/pagefind-search.js` to progressively enhance the header search link into an accessible native `<dialog>` modal with `/` and `Ctrl+K` / `Cmd+K` shortcuts.
* Style Pagefind UI variables, inputs, buttons, excerpts, badges, and focus states in `docs/content/assets/styles.scss` to conform to GOV.UK Frontend / DfE branding and WCAG 2.2 Level AA contrast and focus standards.
* Support both local root hosting (`/`) and GitHub Pages subpath hosting (`/education-benchmarking-and-insights/`) via relative module URL resolution (`bundlePath` and `baseUrl`).
* Remove legacy `templates.searchIndex` generation as part of the Pagefind cutover.
* Add automated build, index, URL prefix, and UI behavioural tests (`npm test` in `docs/`) and execute them in `.github/workflows/docs-ci-cd.yml`.
* Update `docs/README.md` to document the Pagefind search architecture and npm test command.

### Out-of-Scope

* Modifying the main FBIT ASP.NET Core `web/` portal, `platform/` Azure Functions APIs, `data-pipeline/`, or `front-end-components/`.
* Introducing hosted third-party search services (such as Algolia DocSearch or Azure AI Search).
* Indexing external repositories, PDF attachments in `pipelines/documentation/`, or non-HTML assets.
* Adding search analytics or telemetry cookies.

## Knowledge Context

### Assumptions

* [ASSUME-001: GitHub Pages Subpath Prefix Configuration](../../assumptions/ASSUME-001-github-pages-subpath-prefix.md)
* [ASSUME-002: Pagefind Optional Dependencies Compatibility with npm ci --ignore-scripts](../../assumptions/ASSUME-002-pagefind-optional-dependencies-ci-compatibility.md)
* [ASSUME-003: Diataxis Quadrant Section Metadata Availability](../../assumptions/ASSUME-003-diataxis-quadrant-section-metadata.md)

### Architectural Decisions (ADRs)

* [ADR-0001: Adopt Pagefind via Eleventy After-Build Hook for Documentation Search](../../decisions/ADR-0001-adopt-pagefind-for-docs-static-search.md)
* [ADR-0002: Progressive Dual-Surface Pagefind UI Pattern (Header Modal and Dedicated Search Page)](../../decisions/ADR-0002-progressive-pagefind-search-ui-pattern.md)

## UI Implementation Plan

All user-facing views, states, and interactive components are defined below so no state is left implicit:

1. **Global Header Search Trigger (`docs/content/_includes/components/search/template.njk`)**
   * **No-JS / Pre-Enhancement State:** Displays `<a class="app-search__link" href="/search/">Search</a>` inside `.app-search.app-pagefind-search` in the DfE header. The `<button id="header-search-button">` remains hidden via CSS until `body.js-enabled` is active.
   * **JS-Enabled Ready State (`body.js-enabled`):** Hides `.app-search__link` and displays `#header-search-button` (`type="button"`, `aria-haspopup="dialog"`, `aria-controls="pagefind-search-dialog"`) containing a magnifying glass SVG icon, visible label `"Search documentation"`, and keyboard hint `<kbd aria-hidden="true">/</kbd>`.
   * **Focus State:** Applies standard GOV.UK 3px yellow focus outline (`#ffdd00`) with black text and focus border.
   * **Print State:** Hidden on print media via `govuk-!-display-none-print`.

2. **Quick-Search Modal Dialog (`<dialog id="pagefind-search-dialog">`)**
   * **Closed State:** Native `<dialog>` is closed and removed from the accessibility tree and tab order.
   * **Open / Initialising State:** Triggered by clicking `#header-search-button` or pressing `/` (outside editable fields) or `Ctrl+K` / `Cmd+K`. Opens via `dialog.showModal()`, lazy-loads `${bundlePath}pagefind-ui.js` on first open, mounts `PagefindUI` into `#pagefind-modal-mount`, and moves keyboard focus into the Pagefind search `<input>`. Includes a header bar with `<h2 id="pagefind-dialog-title">Search documentation</h2>` and a GOV.UK-styled `"Close"` button (`aria-label="Close search dialog"`).
   * **Populated Results State:** Displays ranked page matches with page title links, highlighted `<mark>` excerpts, nested sub-heading deep links (`sub_results`), and a footer link `"Open full search page with filters"` pointing to `${baseUrl}search/?q={encodedQuery}`.
   * **Zero Results State:** Renders Pagefind's accessible status message indicating no matching pages were found for the entered term.
   * **Load Error State:** If `pagefind-ui.js` fails to load, displays a GOV.UK error/fallback paragraph inside `#pagefind-modal-mount` with direct links to `/search/` and `/sitemap/`.
   * **Dismissal Behaviour:** Pressing `Escape`, clicking the `"Close"` button, or clicking the `<dialog>` backdrop closes the modal and restores focus to `#header-search-button`.

3. **Dedicated Search Page (`docs/content/search.md` -> `/search/index.html`)**
   * **No-JS State (`<noscript>`):** Displays a GOV.UK inset notice explaining that interactive search requires JavaScript and providing a clickable link to `/sitemap/`.
   * **Initial Idle State (No `?q=` Parameter):** Renders page heading `"Search technical documentation"`, mounts `PagefindUI` inside `#pagefind-page-search` with `showSubResults: true` and Diátaxis `section` filter panel (`Tutorials`, `How-to guides`, `Reference`, `Explanation`), and focuses the search input.
   * **Pre-Populated Query State (`?q={term}`):** Reads `URLSearchParams(window.location.search).get('q')`, triggers `pagefindUI.triggerSearch(query)`, and updates the browser URL query string via `history.replaceState` as the user refines or clears the input.
   * **Filtered Results State:** Selecting one or more Diátaxis `section` checkboxes narrows results to matching quadrants and updates result counts.
   * **Zero Results & Error States:** Mirrors modal empty-result messaging and degrades gracefully with a link to `/sitemap/` if index assets cannot be fetched.

4. **Non-Indexed System Pages (`/404.html`, `/sitemap/index.html`, `/search/index.html`)**
   * Retain the global header search trigger so users on a 404 or sitemap page can search immediately, while omitting `data-pagefind-body` (and setting `data-pagefind-ignore="all"`) on `<main>` so these utility pages never pollute search results.

## Approach

### 1. Build-Time Indexing (`docs/eleventy.config.js`)

Register an `async` listener on `eleventy.after` in `docs/eleventy.config.js`:

* Import `* as pagefind from 'pagefind'`.
* Call `await pagefind.createIndex({ forceLanguage: 'en', excludeSelectors: ['.x-govuk-sub-navigation', '.govuk-pagination', '.x-govuk-related-navigation', '[data-pagefind-ignore]'] })`.
* Index `dir.output` (`_site`) via `index.addDirectory({ path: dir.output })`, check for non-empty `errors`, write bundles to `${dir.output}/pagefind` via `index.writeFiles({ outputPath: path.join(dir.output, 'pagefind') })`, and call `await pagefind.close()` so single-run CLI builds exit cleanly.

### 2. Semantic Layout Metadata (`docs/content/content.11tydata.js`)

Leverage `govuk-frontend`'s built-in `govukAttributes(mainAttributes)` support on `<main id="main-content">` without forking `@x-govuk/govuk-eleventy-plugin` layout files:

* Create `docs/content/content.11tydata.js` exporting `eleventyComputed.mainAttributes`.
* When `data.eleventyExcludeFromSearch` is truthy or `data.layout` is `'error'` or `'sitemap'`, return `{ 'data-pagefind-ignore': 'all' }`.
* Otherwise, return `{ 'data-pagefind-body': { value: true, optional: true } }` merged with `{ 'data-pagefind-filter': 'section:' + data.sectionKey }` when `data.sectionKey` is present.

### 3. Client Controller & Subpath Resolution (`docs/content/assets/js/pagefind-search.js`)

* Compute `baseUrl = new URL('../../', import.meta.url).pathname` and `bundlePath = new URL('../../pagefind/', import.meta.url).pathname`. Because Eleventy's `EleventyHtmlBasePlugin` rewrites the `<script src="/assets/js/pagefind-search.js" type="module">` URL in production builds, `baseUrl` resolves automatically to `"/"` locally and `"/education-benchmarking-and-insights/"` on GitHub Pages with zero inline scripts.
* Load `${bundlePath}pagefind-ui.js` dynamically on demand (immediately on `/search/`, or on first modal open on all other pages) so regular page loads incur zero search JS/WASM overhead beyond the small controller module.
* Pass `{ bundlePath, baseUrl, showSubResults: true, showImages: false, resetStyles: false }` to `new window.PagefindUI(...)`.

### 4. Styling (`docs/content/assets/styles.scss` & `docs/eleventy.config.js`)

* Load `stylesheets: ['/pagefind/pagefind-ui.css', '/assets/styles.css']` in `docs/eleventy.config.js` so GOV.UK overrides in `styles.scss` follow `pagefind-ui.css` in cascade order without `!important` declarations.
* Map `--pagefind-ui-*` custom properties to GOV.UK design tokens (`Inter` font stack, `govuk-colour("black")`, `govuk-colour("blue")`, `govuk-functional-colour(border)`), style `<mark>` highlights with accessible contrast, and apply GOV.UK focus rings to inputs, buttons, filter checkboxes, result links, and the modal close control.

### 5. PR & Deployment Strategy

Deliver the feature as a **single atomic Pull Request** (`docs(search): add pagefind search to eleventy docs`), keeping the implementation concise so it stays within the repository's 400-line pull request guideline (`.github/workflows/pr-compliance-checks.yml`):

* **Single Atomic PR: Pagefind Build Indexing, Dual-Surface GOV.UK Search UI, Legacy Cutover, and Automated Verification**
  * **Build & Semantic Indexing:** Adds `pagefind` devDependency, configures `eleventy.after` indexing in `docs/eleventy.config.js`, disables legacy `templates.searchIndex`, and adds `docs/content/content.11tydata.js` for `data-pagefind-body` and Diátaxis `section` filters.
  * **UI, Progressive Enhancement & Styling:** Adds `docs/content/search.md`, overrides `docs/content/_includes/components/search/template.njk`, adds `docs/content/assets/js/pagefind-search.js` (`<dialog>` modal, `/` and `Ctrl+K` / `Cmd+K` shortcuts, `?q=` hydration, `import.meta.url` subpath resolution), and styles Pagefind UI to GOV.UK WCAG 2.2 Level AA standards in `docs/content/assets/styles.scss`.
  * **Verification & Documentation:** Adds `npm test` (`node --test`) covering build output, index filtering, subpath resolution, modal/query behaviour, and legacy `search-index.json` removal; wires `npm test` into `.github/workflows/docs-ci-cd.yml`; and updates `docs/README.md`.
