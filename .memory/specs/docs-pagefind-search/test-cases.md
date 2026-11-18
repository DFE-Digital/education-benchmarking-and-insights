# Test Cases: Add Pagefind Search to Eleventy Documentation Site

## 🟢 Happy Path Scenarios

### TC-01: Build Generates Pagefind Static Index and Exits Cleanly

- **Pre-conditions**: Dependencies in `docs/` are installed.
- **Steps**:
  1. Execute `npm run build` in `docs/`.
  2. Inspect the exit code and the generated `docs/_site/pagefind/` directory.
- **Expected Result**: The build exits with code `0`, `docs/_site/pagefind/pagefind-entry.json`, `pagefind.js`, `pagefind-ui.js`, `pagefind-ui.css`, and `wasm.en.pagefind` are present, and `pagefind-entry.json` reports an English index with `page_count > 0`.

### TC-02: Full-Text Stemmed Search Returns Ranked Results and Sub-Heading Deep Links

- **Pre-conditions**: The documentation site is built with Pagefind indexing enabled.
- **Steps**:
  1. Query the generated Pagefind index (or enter `"architectures"` in the search input on `/search/`).
  2. Inspect the returned results and `sub_results`.
- **Expected Result**: Stemmed matches for `"Architecture"` pages are returned with highlighted `<mark>` excerpts and deep-link `sub_results` pointing to specific section heading anchors (for example, `#documentation-structure` or `#architecture`).

### TC-03: Diátaxis Quadrant Facet Filtering Narrows Search Results

- **Pre-conditions**: User opens `/search/` on a built documentation site.
- **Steps**:
  1. Enter a broad domain query such as `"data"` in the search input.
  2. Select the `"Explanation"` filter under the `section` facet.
- **Expected Result**: Available `section` filters include `"Tutorials"`, `"How-to guides"`, `"Reference"`, and `"Explanation"`, and selecting `"Explanation"` restricts displayed results strictly to pages under `/explanation/`.

### TC-04: Header Search Button and Keyboard Shortcuts Open Accessible Modal

- **Pre-conditions**: User views any documentation page (for example, `/`) with JavaScript enabled.
- **Steps**:
  1. Press `/` (while focus is not inside an editable input) or click `#header-search-button`.
  2. Enter `"docker"` into the modal search input.
  3. Press `Escape` to close the modal.
- **Expected Result**: `<dialog id="pagefind-search-dialog">` opens as a modal, focus moves automatically into the modal search input, matching how-to guide results appear, and pressing `Escape` closes the modal and restores focus to `#header-search-button`.

### TC-05: URL Query Parameter (`?q=`) Hydrates and Synchronises Dedicated Search Page

- **Pre-conditions**: Documentation site is served locally or on GitHub Pages.
- **Steps**:
  1. Navigate directly to `/search/?q=databricks`.
  2. Update the search input value to `"mermaid"`.
- **Expected Result**: On initial load, the search input populates with `"databricks"` and displays matching results immediately; updating the input updates `window.location.search` to `?q=mermaid` via `history.replaceState` without full page reload.

### TC-06: Production GitHub Pages Subpath Prefix (`ELEVENTY_ENV=production`) Resolves Assets and Result Links

- **Pre-conditions**: Clean build directory in `docs/`.
- **Steps**:
  1. Run `ELEVENTY_ENV=production npm run build` in `docs/`.
  2. Inspect asset URLs in `_site/index.html` and `_site/search/index.html`, and resolve `bundlePath` and `baseUrl` in the search controller for script URL `/education-benchmarking-and-insights/assets/js/pagefind-search.js`.
- **Expected Result**: Stylesheet and script tags include the `/education-benchmarking-and-insights/` prefix, `bundlePath` resolves to `/education-benchmarking-and-insights/pagefind/`, `baseUrl` resolves to `/education-benchmarking-and-insights/`, and result links navigate under `/education-benchmarking-and-insights/` without double-prefixing.

## 🔴 Negative Scenarios

### TC-07: Non-Content System Pages Are Excluded from Search Results

- **Pre-conditions**: Documentation site is built with Pagefind indexing enabled.
- **Steps**:
  1. Search the generated Pagefind index for `"Page not found"`, `"Sitemap"`, and `"Search technical documentation"`.
  2. Inspect the URLs of all indexed pages.
- **Expected Result**: `/404.html`, `/sitemap/`, and `/search/` never appear in search results.

### TC-08: Global Chrome and Sub-Navigation Sidebars Do Not Pollute Search Results

- **Pre-conditions**: Documentation site is built with Pagefind indexing enabled.
- **Steps**:
  1. Search the Pagefind index for footer-only licence text `"Open Government Licence v3.0"`.
  2. Search for a sibling page title inside a quadrant and inspect excerpts of unrelated pages in the same quadrant.
- **Expected Result**: Searching for footer-only text returns `0` results, and sibling navigation links from `.x-govuk-sub-navigation` are excluded from page body indexing.

### TC-09: Unmatched Query Displays Clear Zero-Results Status

- **Pre-conditions**: User opens the search modal or `/search/`.
- **Steps**:
  1. Enter `"zephyrion quasarflux nebulon"` into the search input.
- **Expected Result**: Zero result items are rendered and a visible, screen-reader-accessible `"No results"` status message is displayed without console errors.

### TC-10: Missing or Unreachable Pagefind Bundle Degrades Gracefully

- **Pre-conditions**: `pagefind-ui.js` network request fails (simulated offline or `404` response).
- **Steps**:
  1. Open `/search/` or click `#header-search-button`.
- **Expected Result**: No unhandled promise rejection occurs; the container displays a clear fallback message with a working link to `/sitemap/`.

## 🟡 Edge Cases

### TC-11: Regular Expression Metacharacters and HTML Payloads in Query String

- **Pre-conditions**: User opens `/search/?q=%5B(%2B*%3F%5C%3Cscript%3Ealert(1)%3C%2Fscript%3E`.
- **Steps**:
  1. Load `/search/` with the encoded metacharacter and `<script>` query parameter.
  2. Type `[(+*\` directly into the modal search input.
- **Expected Result**: No `SyntaxError` (previously triggered by `new RegExp`) and no DOM XSS execution occur; the literal string is treated strictly as plain search text.

### TC-12: Slash (`/`) Shortcut Ignored When Typing Inside Form Controls

- **Pre-conditions**: User is on `/search/` (or any page with a focused `<input>`, `<textarea>`, `<select>`, or `contenteditable` element).
- **Steps**:
  1. Focus an `<input>` element and press the `/` key.
- **Expected Result**: The `/` character is typed normally into the focused input and does NOT intercept keystrokes or trigger modal re-initialisation.

### TC-13: Overlong URL Query Parameter (`?q=` > 256 Characters) Truncated Safely

- **Pre-conditions**: User navigates to `/search/?q=` followed by a 2,000-character string.
- **Steps**:
  1. Load `/search/` with the 2,000-character `q` parameter.
- **Expected Result**: The client controller caps the hydrated search query at 256 characters before invoking `triggerSearch`, keeping UI rendering responsive.

### TC-14: Legacy Monolithic `search-index.json` Removed

- **Pre-conditions**: Clean build (`npm run build`) completes.
- **Steps**:
  1. Check `docs/_site/search-index.json` and inspect initial network requests on `/`.
- **Expected Result**: `docs/_site/search-index.json` is not generated and initial load of `/` makes zero requests for `search-index.json` or `pagefind-ui.js`.

## 🔵 Non-Functional & Accessibility Scenarios

### TC-15: Progressive Enhancement When JavaScript Is Disabled

- **Pre-conditions**: Browser has JavaScript disabled (`body` lacks `.js-enabled`).
- **Steps**:
  1. Load `/` and inspect the header search area.
  2. Activate the visible `"Search"` link to navigate to `/search/`.
- **Expected Result**: `#header-search-button` remains hidden, `<a class="app-search__link" href="/search/">Search</a>` is visible and keyboard-focusable, and `/search/` displays a `<noscript>` message linking to `/sitemap/`.

### TC-16: WCAG 2.2 Level AA Keyboard Navigation and Focus Visibility

- **Pre-conditions**: User navigates solely via keyboard (`Tab`, `Shift+Tab`, `Enter`, `Escape`).
- **Steps**:
  1. Tab through the header to `#header-search-button` and press `Enter`.
  2. Tab through the modal search input, clear button, result links, and `"Close"` button.
  3. Verify focus outlines and ARIA attributes (`aria-haspopup="dialog"`, `aria-labelledby="pagefind-dialog-title"`).
- **Expected Result**: Every interactive element has a visible GOV.UK focus indicator (`#ffdd00` background / 3px focus ring), tab focus remains within the open modal `<dialog>`, and screen readers announce the dialog title and result count updates.
