# PR Title: fix(docs): render mathematical equations with client-side KaTeX

## 🧾 Summary
Fixes mathematical equations rendering as raw unformatted LaTeX text on the documentation site (`docs/content/explanation/data-pipeline/processing.md`). Extends Markdown-It to recognise block and inline equations while protecting LaTeX syntax from typographic mangling, and adds client-side lazy-loaded KaTeX typesetting.

> **Note:** This PR was generated with Antigravity and the `/fix-bug` skill.

### 🛠️ Bug Fix Description
* **Root Cause:** The Eleventy site lacked a math parsing plugin for Markdown-It and loaded no mathematical typesetting library. The default parser stripped escape backslashes (`\\`, `\%`), inserted `<br>` tags due to `breaks: true`, and left raw `$$ ... $$` and `$ ... $` text in `<p>` elements.
* **Changes:**
  * Implemented `docs/markdown-math.js` with `math_block` and `math_inline` rules that emit semantic `.app-math--block` and `.app-math--inline` containers with HTML-escaped raw LaTeX.
  * Registered `markdownMath` via `eleventyConfig.amendLibrary('md', markdownMath)` in `docs/eleventy.config.js`.
  * Added lazy client-side typesetting in `docs/content/assets/js/math.js` fetching KaTeX CSS and ESM module only when `[data-math]` elements exist on the page.
  * Added responsive container styling with `overflow-x: auto` in `docs/content/assets/styles.scss`.
  * Documented equation syntax in `docs/README.md`.
  * Added comprehensive automated regression tests in `docs/tests/math-rendering.test.js`.

### ⚖️ Architectural Trade-offs

| Approach | Pros | Cons | Verdict |
| :--- | :--- | :--- | :--- |
| **Lazy client-side KaTeX (Implemented)** | • Zero npm dependencies added to `package.json`<br>• Zero network or DOM overhead on pages without equations<br>• Follows existing patterns used by `mermaid.js` and `pagefind-search.js`<br>• Clean fallback to uncorrupted LaTeX text if offline | • Requires client-side JavaScript for equation typesetting<br>• Minor layout shift during initial render on math pages | **Chosen:** Aligns directly with established documentation architecture and avoids supply chain and font asset maintenance overhead. |
| **Build-time KaTeX (`katex.renderToString`)** | • Pre-renders static HTML and native MathML during Eleventy build<br>• Works without client-side JavaScript<br>• Zero layout shift on page load | • Requires adding `katex` to `devDependencies`<br>• Requires bundling and hosting KaTeX fonts and CSS assets locally (~300 KB)<br>• Still requires custom Markdown delimiter parsing to avoid conflicts with `breaks: true` and currency | Viable future enhancement if pure zero-JS output becomes a strict requirement. |
| **Off-the-shelf plugin (`markdown-it-katex`)** | • Minimal configuration code | • Unmaintained community libraries (mostly untouched for 5+ years)<br>• Incompatible with `@x-govuk/govuk-eleventy-plugin` (`breaks: true` inserts `<br>` inside equations)<br>• False positives on currency prose (`~$50`, `$50 and $100`) | Rejected due to incompatibility with GOV.UK typography rules and maintenance risk. |

## ✅ Checklist
- [x] Code follows project coding standards (Trunk-based guidelines, small PR size).
- [x] Unit and integration tests added/updated (`docs/tests/math-rendering.test.js`).
- [x] Tested by running locally (`npm test` and `npm run build` pass with 21/21 tests green).
- [x] Relevant documentation updated (`docs/README.md`).

## 🔍 Notes
* Verified that currency values (`~$50 a month`, `$50 and $100`), inline code spans (`$(CI)`), and fenced code blocks are not misidentified as math expressions.
* Verified that production builds rewrite the script URL to `/education-benchmarking-and-insights/assets/js/math.js`.
