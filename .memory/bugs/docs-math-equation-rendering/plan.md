---
title: "Mathematical equations render as raw LaTeX text on documentation site"
component: "docs/eleventy"
domain: "documentation"
status: "completed"
tags: ["bug", "docs", "eleventy", "markdown-it", "katex"]
created: 2026-11-19
updated: 2026-11-19
---

## Symptom and Reproduction

The documentation page `docs/content/explanation/data-pipeline/processing.md` contains block equations (`$$ ... $$`) and an inline equation (`$ ... $`) under the `Comparator set computation` section (including `Pupil Calculation`). When Eleventy builds the static site, the output HTML displays raw dollar-sign delimiters and unformatted LaTeX source text instead of rendered mathematical notation.

## Root Cause Analysis (5 Whys)

1. **Why do mathematical equations display as raw text on the documentation site?**
   The generated HTML page contains plain `<p>` elements with raw `$$ ... $$` and `$ ... $` text, and the browser loads no mathematical typesetting library.
2. **Why does the generated HTML contain corrupted LaTeX strings inside `<p>` elements?**
   The `markdown-it` parser treats `$` and `$$` as ordinary text characters, strips escape backslashes from `\%` and `\\`, and inserts `<br>` tags at line breaks because `breaks: true` is active.
3. **Why does `markdown-it` lack rules for `$` and `$$` delimiters?**
   `@x-govuk/govuk-eleventy-plugin` configures `markdown-it` with GOV.UK typography plugins, subscript, and superscript rules, but it does not include a mathematical expression plugin.
4. **Why does `docs/eleventy.config.js` not extend Markdown or load a mathematical renderer?**
   When the documentation site moved to Eleventy, client-side support was added for Mermaid diagrams (`mermaid.js`), while LaTeX equation support was omitted.
5. **Why was this omission not detected before deployment?**
   The automated test suite in `docs/tests/` verifies Pagefind search indexing and UI behaviour, but it has no test assertions for mathematical equation markup or rendering.

## Horizontal Impact Analysis

A repository-wide scan of `docs/content/**/*.md` identified all occurrences of `$` characters to find affected pages and false-positive risks:

* **Affected equations (`docs/content/explanation/data-pipeline/processing.md`):**
  * Line 321: Single-line block equation (`Pupil Calculation (non-special)`).
  * Lines 325 to 339: Multi-line `\begin{aligned}` block equation (`Special Calculation`).
  * Line 346: Inline equation with inner spaces (`$ ProportionArea * (Year - BuildingAge)  $`).
  * Line 393: Single-line block equation (`Area Calculation`).
  * Lines 405 to 407: Multi-line `\begin{aligned}` block equation with `\sum` and `\substack` (`Trust Calculation`).
  * Lines 411 to 421: Multi-line `\begin{aligned}` block equation (`Unified pupil calc`).
* **Non-math dollar signs across `docs/content/` (must remain unchanged):**
  * Currency text in prose: `docs/content/reference/decisions/0014-dfe-databricks-data-out.md` (`~$50 a month`).
  * Inline code spans: `docs/content/reference/decisions/0022-build-logic-vs-pipeline-orchestration.md` (`` `$(CI)` ``) and `docs/content/reference/platform-and-web/branding.md` (`` `$root` ``).
  * Fenced code blocks in C#, KQL, Terraform, and JavaScript across architecture and decision pages.
* **Alternative delimiters:** No pages use `\( ... \)`, `\[ ... \]`, or ```` ```math ```` blocks.

## Resolution Plan

### Fix Steps

1. **Create regression tests first (`docs/tests/math-rendering.test.js`):**
   * Tag tests with `// regression-test: docs-math-equation-rendering`.
   * Verify that `markdownMath` and the Eleventy build emit `.app-math--block` and `.app-math--inline` elements on `explanation/data-pipeline/processing/index.html` while preserving LaTeX backslashes (`\\`, `\%`) and alignment operators (`&=`).
   * Verify that currency values (`~$50 a month`, `$50 and $100`), inline code spans (`` `$(CI)` ``), and fenced code blocks remain untouched.
   * Verify that production builds rewrite `/assets/js/math.js` with the `/education-benchmarking-and-insights/` path prefix.
   * Verify that the client-side `initMathRendering` module does not load KaTeX assets when a page has no `.app-math` elements, and renders block and inline equations safely (`trust: false`, `throwOnError: false`) when `.app-math` elements exist.
2. **Extend Markdown parsing (`docs/eleventy.config.js`):**
   * Use `eleventyConfig.amendLibrary('md', markdownMath)` to register `math_block` and `math_inline` rules.
   * Emit `<div class="app-math app-math--block" data-math="block">` and `<span class="app-math app-math--inline" data-math="inline">` with HTML-escaped raw LaTeX so `markdown-it` does not strip backslashes, add `<br>` tags, or apply GOV.UK text replacements inside equations.
   * Include `<script src="/assets/js/math.js" type="module"></script>` in `footer.meta.html` next to `mermaid.js` and `pagefind-search.js`.
3. **Add client-side KaTeX renderer (`docs/content/assets/js/math.js`):**
   * Match the existing `mermaid.js` and `pagefind-search.js` architecture without adding npm build dependencies.
   * Dynamically load KaTeX CSS and ESM module from jsDelivr CDN only when `[data-math]` elements exist on the current page.
4. **Add minimal container styling (`docs/content/assets/styles.scss`):**
   * Add `.app-math--block` spacing and `overflow-x: auto` to prevent horizontal viewport overflow on small screens.
5. **Update documentation (`docs/README.md`):**
   * Document mathematical equation support under `Visuals & Diagrams`.

### Potential Impacts

* Pages without equations incur zero network requests for KaTeX stylesheets, scripts, or fonts.
* If JavaScript or the CDN is unavailable, the page displays clean, uncorrupted LaTeX source text as a progressive enhancement fallback.

## Architectural Trade-offs

| Approach | Pros | Cons | Repository Fit |
| :--- | :--- | :--- | :--- |
| **Lazy client-side KaTeX (Implemented)** | • Zero npm dependencies added to `package.json`<br>• Zero network or DOM overhead on 99% of pages without equations<br>• Follows existing repository pattern used by `mermaid.js` and `pagefind-search.js`<br>• Clean fallback to uncorrupted LaTeX text if offline | • Requires client-side JavaScript for equation typesetting<br>• Minor layout shift during initial typesetting on math pages | **Chosen**: Aligns directly with established repository architecture and avoids maintenance overhead of npm dependencies and vendored font assets. |
| **Build-time KaTeX (`katex.renderToString`)** | • Renders static HTML and native MathML during Eleventy build<br>• Works without client-side JavaScript<br>• Zero layout shift on page load | • Requires adding `katex` to `devDependencies`<br>• Requires hosting and bundling KaTeX fonts and CSS assets locally (~300 KB)<br>• Still requires custom Markdown delimiter parsing to avoid conflicts with `breaks: true` and currency | Viable future enhancement if pure zero-JS output becomes a strict accessibility requirement. |
| **Off-the-shelf plugin (`markdown-it-katex`)** | • Minimal initial configuration code | • Unmaintained community libraries (mostly untouched for 5+ years)<br>• Breaks under `@x-govuk/govuk-eleventy-plugin` due to `breaks: true` inserting `<br>` inside equations<br>• High risk of false positives on currency prose (`~$50`, `$50 and $100`) | Rejected due to incompatibility with GOV.UK typography rules and maintenance risk. |
