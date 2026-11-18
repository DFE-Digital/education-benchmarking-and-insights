# Security Assessment: docs-pagefind-search

**Target Plan:** `.memory/specs/docs-pagefind-search/proposal.md` | **Context:** *Static client-side WebAssembly full-text search (Pagefind) for public Eleventy technical documentation hosted on GitHub Pages.*

## 1. Context Validation

| Attribute | Declared | Assessment | Match? |
|---|---|---|---|
| Sensitivity | *Public* | *Public (open-source engineering documentation; zero PII, secrets, or establishment-restricted data)* | *✅* |
| Authentication | *None* | *None (public GitHub Pages static site)* | *✅* |
| External Runtime Services | *None* | *None (all Pagefind JS, CSS, WASM, and index chunks are built locally/CI and served from same origin)* | *✅* |

## 2. CIA Triad

| Pillar | Status | Notes | Controls |
|---|---|---|---|
| Confidentiality | *Pass* | *Only rendered HTML inside `docs/_site/` carrying `data-pagefind-body` is indexed; zero user queries leave the browser.* | *`data-pagefind-body` allowlist scoping; zero external search API calls.* |
| Integrity | *Pass* | *Index bundles are generated deterministically during GitHub Actions build with `--ignore-scripts` preserved.* | *Pinned lockfile (`docs/package-lock.json`), `npm ci --ignore-scripts`, same-origin asset loading.* |
| Availability | *Pass* | *Eliminates 311 KB upfront JSON download and removes `new RegExp(searchQuery)` client error/ReDoS vector.* | *Chunked WASM index, 256-char `?q=` cap, graceful fallback link to `/sitemap/`.* |

## 3. Data Flow

| Flow | Source | Dest | Type | Trust Boundary? | Protocol | Controls |
|---|---|---|---|---|---|---|
| Build-time HTML Indexing | `docs/_site/**/*.html` | `docs/_site/pagefind/*` | Static HTML -> WASM/PF fragments | No (CI runner internal) | Local filesystem | `data-pagefind-body` scoping, `excludeSelectors` |
| Static Bundle Delivery | GitHub Pages CDN | User Browser | Static JS, CSS, WASM, PF chunks | Yes (Public Internet) | HTTPS (TLS 1.2/1.3) | Same-origin ES module / `bundlePath` resolution |
| URL Query Hydration | Browser URL (`?q=`) | `pagefind-search.js` -> `PagefindUI` | Untrusted URL query string | Yes (DOM input) | Client memory | Plain-text `triggerSearch()`, 256-char length cap, `encodeURIComponent`, zero `innerHTML` |

## 4. Threat Assessment

- **DOM-Based Cross-Site Scripting (XSS) via `?q=` Query Parameter or Modal Link:** If `?q=` from `window.location.search` or modal input text is interpolated into DOM HTML (`innerHTML`) when showing status text or constructing the `"Open full search page"` URL, an attacker could craft a reflected DOM XSS link (`/search/?q=<img src=x onerror=...>`).
- **Supply-Chain Script Execution during Dependency Installation:** Adding `pagefind` to `docs/package.json` could introduce post-install script risks if `--ignore-scripts` were removed from `.github/workflows/docs-ci-cd.yml`. Pagefind v1.6 distributes platform binaries via `optionalDependencies`, so `--ignore-scripts` MUST remain enforced.
- **Client-Side Denial of Service (ReDoS / Oversized Query):** The existing `@x-govuk/govuk-eleventy-plugin` search passes raw user input to `new RegExp(searchQuery, 'gi')`, enabling catastrophic backtracking or `SyntaxError` exceptions. Replacing `search-index.json` with Pagefind WASM token matching and capping `?q=` hydration at 256 characters mitigates this risk.
- **Inline Script Content Security Policy (CSP) Weakening:** Injecting `window.__PATH_PREFIX__` via an inline `<script>` tag requires `'unsafe-inline'` under a strict CSP. Resolving `baseUrl` and `bundlePath` from `import.meta.url` inside `/assets/js/pagefind-search.js` avoids new inline scripts.
- **Accidental Indexing of Non-Documentation Files:** Indexing outside `docs/_site/` or without `data-pagefind-body` could expose utility/error pages. Scoping `addDirectory` strictly to `dir.output` with `data-pagefind-body` prevents unintended indexing.

## 5. Compliance

- **NCSC Zero Trust:** Pass. Treat URL query parameters (`?q=`) and asset network responses as untrusted inputs; validate length, encode URL components with `encodeURIComponent`, and handle failed asset loads gracefully.
- **NCSC Secure by Design (Secure Defaults & Data Minimisation):** Pass. Search runs entirely in-browser against public static chunks with zero server-side query logging, zero cookies, and zero third-party network calls.
- **NCSC Anti-Patterns:** Pass. No Browse-Up, Management Bypass, or Implicit Trust patterns are present; `--ignore-scripts` remains active in CI.

## 6. Required Remediation

### A. Architecture

- [x] **Avoid Inline Path-Prefix Scripts:** Resolve `baseUrl` and `bundlePath` via `new URL('../../', import.meta.url).pathname` in `docs/content/assets/js/pagefind-search.js` rather than adding inline `<script>` blocks (*Rationale:* NCSC Secure by Design, preserve strict CSP compatibility).
- [x] **Preserve CI Script Hardening:** Retain `npm ci --ignore-scripts` in `.github/workflows/docs-ci-cd.yml` when installing `pagefind` (*Rationale:* NCSC Supply Chain Security).

### B. Requirements

- [x] **Sanitise and Bound `?q=` Input:** Truncate URL `?q=` values to 256 characters, pass queries exclusively via `triggerSearch(query)` or `textContent`, and build `/search/?q=...` links using `URLSearchParams` / `encodeURIComponent` (*Rationale:* Prevent DOM XSS and client UI hangs).
- [x] **Remove Legacy Regex Search:** Disable `templates.searchIndex` in `docs/eleventy.config.js` and replace `<app-search>` in `components/search/template.njk` so `new RegExp(searchQuery, 'gi')` is no longer reachable (*Rationale:* Eliminate client ReDoS and syntax exceptions).

### C. Anti-Patterns

- [x] **Prevent Unscoped Directory Indexing:** Restrict `index.addDirectory` strictly to Eleventy's `dir.output` (`_site`) and enforce `data-pagefind-body` on content pages only (*Rationale:* Principle of Least Privilege / Data Minimisation).

## 7. Remediation Tasks

*Use exact format:* `- [ ] [Security] [Context] Description` *(Context: `[Global]` or `[US#]`)*

- [x] [Security] [Global] Preserve `npm ci --ignore-scripts` in `.github/workflows/docs-ci-cd.yml` and verify `pagefind` builds cleanly without lifecycle scripts
- [x] [Security] [Global] Scope Pagefind `addDirectory` strictly to `dir.output` with `data-pagefind-body` allowlisting and `excludeSelectors`
- [x] [Security] [Global] Resolve `baseUrl` and `bundlePath` via `import.meta.url` in `pagefind-search.js` with zero inline `<script>` tags
- [x] [Security] [Global] Cap URL `?q=` query hydration at 256 characters, never write user query strings via `innerHTML`, and construct search links using `URLSearchParams`
- [x] [Security] [Global] Disable legacy `templates.searchIndex` in `docs/eleventy.config.js` to eliminate `new RegExp(searchQuery, 'gi')` evaluation
