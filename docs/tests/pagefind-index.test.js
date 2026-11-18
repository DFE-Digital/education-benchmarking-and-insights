import assert from 'node:assert/strict';
import { execFileSync } from 'node:child_process';
import fs from 'node:fs/promises';
import path from 'node:path';
import { after, before, describe, it } from 'node:test';
import { fileURLToPath, pathToFileURL } from 'node:url';

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

async function loadPagefindRuntime(siteDir) {
  globalThis.location = { href: 'http://localhost:8080/', origin: 'http://localhost:8080' };
  globalThis.window = { location: globalThis.location };
  globalThis.document = {
    currentScript: null,
    querySelector: () => ({ getAttribute: () => 'en' })
  };
  globalThis.fetch = async (resourceUrl) => {
    const relativePath = new URL(resourceUrl, 'http://localhost:8080').pathname.replace(/^\//, '');
    const fileBuffer = await fs.readFile(path.join(siteDir, relativePath));
    return new Response(fileBuffer, { status: 200 });
  };

  const moduleUrl = pathToFileURL(path.join(siteDir, 'pagefind/pagefind.js')).href;
  const pagefindRuntime = await import(moduleUrl);
  await pagefindRuntime.options({ basePath: '/pagefind/' });
  await pagefindRuntime.init();
  return pagefindRuntime;
}

describe('Pagefind build indexing and rendered HTML integration', () => {
  let tmpRoot;
  let devSiteDir;
  let prodSiteDir;
  let originalSassPrefix = null;
  let pagefindRuntime;

  before(async () => {
    try {
      originalSassPrefix = await fs.readFile(generatedSassPath, 'utf8');
    } catch {
      originalSassPrefix = null;
    }

    tmpRoot = await fs.mkdtemp(path.join(docsDir, '_test_build_'));
    devSiteDir = path.join(tmpRoot, 'site-dev');
    prodSiteDir = path.join(tmpRoot, 'site-prod');

    buildDocsSite(devSiteDir, { ELEVENTY_ENV: 'development' });
    buildDocsSite(prodSiteDir, { ELEVENTY_ENV: 'production' });
    pagefindRuntime = await loadPagefindRuntime(devSiteDir);
  });

  after(async () => {
    if (originalSassPrefix !== null) {
      await fs.writeFile(generatedSassPath, originalSassPrefix, 'utf8');
    }
    if (tmpRoot) {
      await fs.rm(tmpRoot, { recursive: true, force: true });
    }
  });

  it('TC-01 & TC-14: generates Pagefind WASM bundle and omits legacy search-index.json', async () => {
    const requiredFiles = [
      'assets/application.js',
      'pagefind/pagefind-entry.json',
      'pagefind/pagefind.js',
      'pagefind/pagefind-ui.js',
      'pagefind/pagefind-ui.css',
      'pagefind/wasm.en.pagefind'
    ];

    for (const relativeFile of requiredFiles) {
      const stat = await fs.stat(path.join(devSiteDir, relativeFile));
      assert.equal(stat.isFile(), true, `Expected ${relativeFile} to exist`);
    }

    const entryRaw = await fs.readFile(path.join(devSiteDir, 'pagefind/pagefind-entry.json'), 'utf8');
    const entry = JSON.parse(entryRaw);
    assert.equal(typeof entry.languages?.en?.hash, 'string');
    assert.equal(entry.languages.en.page_count > 0, true);

    await assert.rejects(
      fs.access(path.join(devSiteDir, 'search-index.json')),
      /ENOENT/
    );
  });

  it('TC-02: stemmed full-text search returns ranked matches with highlighted excerpts and sub_results', async () => {
    const response = await pagefindRuntime.search('architectures');
    assert.equal(response.results.length > 0, true);

    const topMatch = await response.results[0].data();
    assert.equal(typeof topMatch.meta?.title, 'string');
    assert.match(topMatch.excerpt, /<mark>/i);
    assert.equal(Array.isArray(topMatch.sub_results), true);
    assert.equal(topMatch.sub_results.length > 0, true);
  });

  it('TC-03: exposes all four Diataxis section filters and scopes filtered queries to that quadrant', async () => {
    const availableFilters = await pagefindRuntime.filters();
    const expectedQuadrants = ['Tutorials', 'How-to guides', 'Reference', 'Explanation'];

    for (const quadrant of expectedQuadrants) {
      assert.equal(
        availableFilters.section?.[quadrant] > 0,
        true,
        `Expected section filter "${quadrant}" to have indexed pages`
      );
    }

    const filteredResponse = await pagefindRuntime.search('data', {
      filters: { section: 'Explanation' }
    });
    assert.equal(filteredResponse.results.length > 0, true);

    const resolvedItems = await Promise.all(
      filteredResponse.results.slice(0, 10).map((item) => item.data())
    );
    for (const item of resolvedItems) {
      assert.equal(
        item.url.startsWith('/explanation/'),
        true,
        `Expected ${item.url} to belong to /explanation/`
      );
    }
  });

  it('TC-07: excludes /404.html, /sitemap/, and /search/ utility pages from search results', async () => {
    const excludedRoutes = ['404.html', 'sitemap/index.html', 'search/index.html'];

    for (const routeFile of excludedRoutes) {
      const html = await fs.readFile(path.join(devSiteDir, routeFile), 'utf8');
      assert.match(html, /<main[^>]*data-pagefind-ignore="all"/);
      assert.doesNotMatch(html, /data-pagefind-body/);
    }

    const allPages = await pagefindRuntime.search(null);
    const indexedUrls = await Promise.all(allPages.results.map(async (r) => (await r.data()).url));
    assert.equal(indexedUrls.includes('/404.html'), false);
    assert.equal(indexedUrls.includes('/sitemap/'), false);
    assert.equal(indexedUrls.includes('/search/'), false);
  });

  it('TC-08: excludes global footer licence chrome and sidebar navigation links from indexed body', async () => {
    const footerQuery = await pagefindRuntime.search('"Open Government Licence v3.0"');
    assert.equal(footerQuery.results.length, 0);

    const gettingStartedQuery = await pagefindRuntime.search('"Pipeline Trigger Message Schemas"');
    const urls = await Promise.all(gettingStartedQuery.results.map(async (r) => (await r.data()).url));
    assert.equal(urls.includes('/reference/decisions/0001-data-storage/'), false);
  });

  it('TC-09 & TC-11: handles unmatched queries and regex/HTML metacharacters without errors', async () => {
    const emptyResponse = await pagefindRuntime.search('zephyrion quasarflux nebulon');
    assert.equal(emptyResponse.results.length, 0);

    const specialResponse = await pagefindRuntime.search('[(+*?\\<script>alert(1)</script>');
    assert.equal(Array.isArray(specialResponse.results), true);
  });

  it('TC-06: prefixes assets and header fallback link under /education-benchmarking-and-insights/ in production builds', async () => {
    const prodHomeHtml = await fs.readFile(path.join(prodSiteDir, 'index.html'), 'utf8');
    const prodSearchHtml = await fs.readFile(path.join(prodSiteDir, 'search/index.html'), 'utf8');

    assert.match(
      prodHomeHtml,
      /href="\/education-benchmarking-and-insights\/pagefind\/pagefind-ui\.css"/
    );
    assert.match(
      prodHomeHtml,
      /src="\/education-benchmarking-and-insights\/assets\/js\/pagefind-search\.js"/
    );
    assert.match(
      prodHomeHtml,
      /class="app-search__link" href="\/education-benchmarking-and-insights\/search\/"/
    );
    assert.match(
      prodSearchHtml,
      /href="\/education-benchmarking-and-insights\/sitemap\/"/
    );
    assert.doesNotMatch(prodHomeHtml, /window\.__PATH_PREFIX__/);
  });

  it('TC-15: renders progressive-enhancement search link, noscript sitemap fallback, and CSS button gating', async () => {
    const homeHtml = await fs.readFile(path.join(devSiteDir, 'index.html'), 'utf8');
    const searchHtml = await fs.readFile(path.join(devSiteDir, 'search/index.html'), 'utf8');
    const compiledCss = await fs.readFile(path.join(devSiteDir, 'assets/styles.css'), 'utf8');

    assert.match(homeHtml, /<a class="app-search__link" href="\/search\/">Search<\/a>/);
    assert.match(homeHtml, /id="header-search-button"[^>]*aria-haspopup="dialog"/);
    assert.match(searchHtml, /<noscript>[\s\S]*href="\/sitemap\/"/);
    assert.match(compiledCss, /\.app-pagefind-search__button\{display:none/);
    assert.match(compiledCss, /\.js-enabled \.app-pagefind-search \.app-pagefind-search__button\{display:inline-flex\}/);
  });
});
