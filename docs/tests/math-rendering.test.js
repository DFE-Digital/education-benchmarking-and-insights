// regression-test: docs-math-equation-rendering
import assert from 'node:assert/strict';
import { execFileSync } from 'node:child_process';
import fs from 'node:fs/promises';
import path from 'node:path';
import { after, before, describe, it } from 'node:test';
import { fileURLToPath } from 'node:url';
import MarkdownIt from 'markdown-it';
import markdownItGovuk from 'markdown-it-govuk';
import markdownItSub from 'markdown-it-sub';
import markdownItSup from 'markdown-it-sup';
import * as eleventyModule from '../eleventy.config.js';

const docsDir = path.resolve(path.dirname(fileURLToPath(import.meta.url)), '..');
const generatedSassPath = path.join(docsDir, 'content/assets/_generated-path-prefix.scss');
const eleventyBin = path.join(docsDir, 'node_modules/.bin/eleventy');

function buildDocsSite(outputDir, envOverrides = {}) {
  const env = { ...process.env, ...envOverrides };
  const relativeOutput = path.relative(docsDir, outputDir);
  execFileSync(process.execPath, ['generate-sass-prefix.js'], {
    cwd: docsDir,
    env,
    stdio: 'pipe'
  });
  execFileSync(eleventyBin, [`--output=${relativeOutput}`], {
    cwd: docsDir,
    env,
    stdio: 'pipe'
  });
}

const { markdownMath } = eleventyModule;

function createGovukMarkdown(options = {}) {
  return new MarkdownIt({
    breaks: true,
    html: true,
    typographer: true,
    ...options
  })
    .use(markdownItGovuk)
    .use(markdownItSub)
    .use(markdownItSup);
}

describe('Markdown mathematical equation parsing (regression-test: docs-math-equation-rendering)', () => {
  const mdLib = createGovukMarkdown({});
  if (typeof markdownMath === 'function') {
    markdownMath(mdLib);
  }

  it('preserves backslashes, percent escapes, and alignment rows in block equations', () => {
    const source = [
      '$$ \\sqrt{0.5\\left(\\dfrac{\\Delta Pupils}{range(pupils)}\\right)^2 + 0.4\\left(\\dfrac{\\Delta FSM\\%}{range(FSM\\%)}\\right)^2 } $$',
      '',
      '$$\\begin{aligned}',
      'pupils &= 0.6\\left(\\dfrac{\\Delta Pupils}{range(pupils)}\\right)^2',
      '\\\\',
      'result &= \\sqrt{pupils}',
      '\\end{aligned}$$'
    ].join('\n');

    const html = mdLib.render(source);
    assert.match(html, /class="app-math app-math--block"[^>]*data-math="block"/);
    assert.match(html, /\\Delta FSM\\%/);
    assert.match(html, /\\\\/);
    assert.doesNotMatch(html, /<br>/);
    assert.doesNotMatch(html, /\$\$/);
  });

  it('preserves inline equations without emphasis, superscript, or typographic mangling', () => {
    const source =
      'Formula $ ProportionArea * (Year - BuildingAge)  $ and $a_1^2 + b_2^2 <= 1/2$ in prose.';
    const html = mdLib.render(source);

    assert.match(
      html,
      /<span class="app-math app-math--inline" data-math="inline">ProportionArea \* \(Year - BuildingAge\)<\/span>/
    );
    assert.match(
      html,
      /<span class="app-math app-math--inline" data-math="inline">a_1\^2 \+ b_2\^2 &lt;= 1\/2<\/span>/
    );
    assert.doesNotMatch(html, /<em>/);
    assert.doesNotMatch(html, /<sup>/);
    assert.doesNotMatch(html, /½|≤/);
  });

  it('ignores currency values, escaped dollars, inline code spans, and fenced code blocks', () => {
    const source = [
      'Cost is ~$50 a month, or between $50 and $100 per run, with escaped \\$20 and \\$30.',
      '',
      'Inline code `$(CI)` and `$root` must stay code.',
      '',
      '```csharp',
      'return await GetAsync($"/some/api", cancellationToken);',
      '```'
    ].join('\n');

    const html = mdLib.render(source);
    assert.doesNotMatch(html, /data-math=/);
    assert.match(html, /~\$50 a month, or between \$50 and \$100 per run, with escaped \$20 and \$30\./);
    assert.match(html, /<code[^>]*>\$\(CI\)<\/code>/);
    assert.match(html, /<code[^>]*>\$root<\/code>/);
  });
});

describe('Eleventy build and client-side KaTeX rendering (regression-test: docs-math-equation-rendering)', () => {
  let tmpRoot;
  let devSiteDir;
  let prodSiteDir;
  let originalSassPrefix = null;

  before(async () => {
    try {
      originalSassPrefix = await fs.readFile(generatedSassPath, 'utf8');
    } catch {
      originalSassPrefix = null;
    }

    tmpRoot = await fs.mkdtemp(path.join(docsDir, '_test_math_'));
    devSiteDir = path.join(tmpRoot, 'site-dev');
    prodSiteDir = path.join(tmpRoot, 'site-prod');

    buildDocsSite(devSiteDir, { ELEVENTY_ENV: 'development' });
    buildDocsSite(prodSiteDir, { ELEVENTY_ENV: 'production' });
  });

  after(async () => {
    if (originalSassPrefix !== null) {
      await fs.writeFile(generatedSassPath, originalSassPrefix, 'utf8');
    }
    if (tmpRoot) {
      await fs.rm(tmpRoot, { recursive: true, force: true });
    }
  });

  it('renders all 5 block equations and 1 inline equation on explanation/data-pipeline/processing', async () => {
    const processingHtml = await fs.readFile(
      path.join(devSiteDir, 'explanation/data-pipeline/processing/index.html'),
      'utf8'
    );

    const blockMatches = processingHtml.match(/data-math="block"/g) ?? [];
    const inlineMatches = processingHtml.match(/data-math="inline"/g) ?? [];
    assert.equal(blockMatches.length, 5, 'Expected 5 block equations on processing page');
    assert.equal(inlineMatches.length, 1, 'Expected 1 inline equation on processing page');

    assert.match(processingHtml, /\\Delta FSM\\%/);
    assert.match(processingHtml, /\\sum_\{\\substack\{n=1\}\}\^N/);
    assert.match(processingHtml, /ProportionArea \* \(Year - BuildingAge\)/);
    assert.doesNotMatch(processingHtml, /\$\$/);

    const databricksDecisionHtml = await fs.readFile(
      path.join(devSiteDir, 'reference/decisions/0014-dfe-databricks-data-out/index.html'),
      'utf8'
    );
    assert.doesNotMatch(databricksDecisionHtml, /data-math=/);
    assert.match(databricksDecisionHtml, /~\$50 a month/);
  });

  it('copies math.js asset and prefixes script URL in production builds', async () => {
    const mathAssetStat = await fs.stat(path.join(prodSiteDir, 'assets/js/math.js'));
    assert.equal(mathAssetStat.isFile(), true);

    const prodProcessingHtml = await fs.readFile(
      path.join(prodSiteDir, 'explanation/data-pipeline/processing/index.html'),
      'utf8'
    );
    assert.match(
      prodProcessingHtml,
      /src="\/education-benchmarking-and-insights\/assets\/js\/math\.js"/
    );

    const compiledCss = await fs.readFile(path.join(devSiteDir, 'assets/styles.css'), 'utf8');
    assert.match(compiledCss, /\.app-math--block\{/);
  });

  it('lazily loads KaTeX only on pages with math elements and handles errors safely', async () => {
    const { KATEX_CSS_URL, initMathRendering } = await import('../content/assets/js/math.js');

    function createFakeMathDom(elements = []) {
      const headLinks = [];
      const doc = {
        head: {
          querySelectorAll() {
            return headLinks;
          },
          appendChild(node) {
            headLinks.push(node);
            return node;
          }
        },
        createElement(tag) {
          return { tagName: tag.toUpperCase(), rel: '', href: '' };
        },
        querySelectorAll(selector) {
          if (selector === '[data-math]') {
            return elements;
          }
          return [];
        }
      };
      return { doc, headLinks };
    }

    // No math elements on page -> zero network loads
    let loadCount = 0;
    const emptyDom = createFakeMathDom([]);
    await initMathRendering({
      doc: emptyDom.doc,
      loadKatex: async () => {
        loadCount += 1;
        return { render() {} };
      }
    });
    assert.equal(loadCount, 0);
    assert.equal(emptyDom.headLinks.length, 0);

    // Block and inline math elements -> loads once, renders with safe options, idempotent on re-run
    const renderedCalls = [];
    const makeMathEl = (mode, text) => {
      const attrs = new Map([['data-math', mode]]);
      return {
        textContent: text,
        getAttribute: (name) => attrs.get(name) ?? null,
        setAttribute: (name, value) => attrs.set(name, String(value))
      };
    };

    const blockEl = makeMathEl('block', '\\sqrt{x}');
    const inlineEl = makeMathEl('inline', 'a + b');
    const mathDom = createFakeMathDom([blockEl, inlineEl]);

    const fakeLoader = async () => {
      loadCount += 1;
      return {
        default: {
          render(expression, element, options) {
            renderedCalls.push({ expression, element, options });
          }
        }
      };
    };

    await initMathRendering({ doc: mathDom.doc, loadKatex: fakeLoader });
    await initMathRendering({ doc: mathDom.doc, loadKatex: fakeLoader });

    assert.equal(loadCount, 1);
    assert.equal(mathDom.headLinks.length, 1);
    assert.equal(mathDom.headLinks[0].href, KATEX_CSS_URL);
    assert.equal(renderedCalls.length, 2);
    assert.equal(renderedCalls[0].expression, '\\sqrt{x}');
    assert.equal(renderedCalls[0].options.displayMode, true);
    assert.equal(renderedCalls[0].options.throwOnError, false);
    assert.equal(renderedCalls[0].options.trust, false);
    assert.equal(renderedCalls[1].expression, 'a + b');
    assert.equal(renderedCalls[1].options.displayMode, false);

    // Graceful failure if CDN cannot be reached
    const failEl = makeMathEl('block', 'x^2');
    const failDom = createFakeMathDom([failEl]);
    await assert.doesNotReject(
      initMathRendering({
        doc: failDom.doc,
        loadKatex: async () => {
          throw new Error('CDN offline');
        }
      })
    );
    assert.equal(failEl.textContent, 'x^2');
  });
});
