import assert from 'node:assert/strict';
import { describe, it } from 'node:test';
import {
  MAX_QUERY_LENGTH,
  buildSearchPageUrl,
  initPagefindSearch,
  isEditableTarget,
  normalizeResultUrl,
  resolveSearchPaths,
  sanitizeSearchQuery
} from '../content/assets/js/pagefind-search.js';

class FakeClassList {
  constructor(owner) {
    this.owner = owner;
  }

  contains(token) {
    return this.owner.className.split(/\s+/).filter(Boolean).includes(token);
  }
}

class FakeElement {
  constructor(doc, tagName) {
    this.ownerDocument = doc;
    this.tagName = tagName.toUpperCase();
    this.id = '';
    this.className = '';
    this.type = '';
    this.href = '';
    this.src = '';
    this.value = '';
    this.textContent = '';
    this.open = false;
    this.isContentEditable = false;
    this.attributes = new Map();
    this.children = [];
    this.listeners = new Map();
    this.classList = new FakeClassList(this);
  }

  set innerHTML(_val) {
    throw new Error('innerHTML assignment is forbidden by security policy');
  }

  setAttribute(name, value) {
    const str = String(value);
    this.attributes.set(name, str);
    if (name === 'id') this.id = str;
    if (name === 'class') this.className = str;
    if (name === 'open') this.open = true;
  }

  getAttribute(name) {
    return this.attributes.get(name) ?? null;
  }

  hasAttribute(name) {
    return this.attributes.has(name);
  }

  removeAttribute(name) {
    this.attributes.delete(name);
    if (name === 'open') this.open = false;
  }

  append(...nodes) {
    for (const node of nodes) {
      if (typeof node === 'string') {
        const span = new FakeElement(this.ownerDocument, '#text');
        span.textContent = node;
        this.children.push(span);
      } else {
        this.children.push(node);
      }
    }
  }

  appendChild(node) {
    this.children.push(node);
    return node;
  }

  replaceChildren(...nodes) {
    this.children = [];
    this.append(...nodes);
  }

  addEventListener(type, handler) {
    const handlers = this.listeners.get(type) ?? [];
    handlers.push(handler);
    this.listeners.set(type, handlers);
  }

  dispatchEvent(event) {
    if (!event.target) {
      event.target = this;
    }
    const handlers = this.listeners.get(event.type) ?? [];
    for (const handler of handlers) {
      handler(event);
    }
    return !event.defaultPrevented;
  }

  focus() {
    this.ownerDocument.activeElement = this;
  }

  click() {
    this.dispatchEvent(new FakeEvent('click'));
  }

  showModal() {
    this.open = true;
    this.attributes.set('open', '');
  }

  close() {
    this.open = false;
    this.attributes.delete('open');
    this.dispatchEvent(new FakeEvent('close'));
  }

  querySelector(selector) {
    for (const child of this.children) {
      if (matchesSelector(child, selector)) {
        return child;
      }
      const nested = child.querySelector?.(selector);
      if (nested) {
        return nested;
      }
    }
    return null;
  }

  get fullText() {
    if (this.children.length === 0) {
      return this.textContent;
    }
    return this.children.map((child) => child.fullText).join('');
  }
}

function matchesSelector(el, selector) {
  if (selector.startsWith('#')) {
    return el.id === selector.slice(1);
  }
  if (selector.startsWith('.')) {
    return el.classList.contains(selector.slice(1));
  }
  return el.tagName === selector.toUpperCase();
}

class FakeEvent {
  constructor(type, props = {}) {
    this.type = type;
    this.defaultPrevented = false;
    Object.assign(this, props);
  }

  preventDefault() {
    this.defaultPrevented = true;
  }
}

function createFakeEnvironment({
  url = 'http://localhost:8080/',
  includeSearchPageMount = false,
  scriptShouldFail = false
} = {}) {
  const listeners = new Map();
  const doc = {
    readyState: 'complete',
    activeElement: null,
    head: null,
    body: null,
    createElement(tag) {
      return new FakeElement(doc, tag);
    },
    getElementById(id) {
      return doc.body.querySelector(`#${id}`) || doc.head.querySelector(`#${id}`);
    },
    addEventListener(type, handler) {
      const list = listeners.get(type) ?? [];
      list.push(handler);
      listeners.set(type, list);
    },
    dispatchEvent(event) {
      const list = listeners.get(event.type) ?? [];
      for (const handler of list) {
        handler(event);
      }
      return !event.defaultPrevented;
    }
  };

  doc.head = new FakeElement(doc, 'head');
  doc.body = new FakeElement(doc, 'body');

  const triggerButton = doc.createElement('button');
  triggerButton.setAttribute('id', 'header-search-button');
  doc.body.appendChild(triggerButton);

  let pageMount = null;
  if (includeSearchPageMount) {
    pageMount = doc.createElement('div');
    pageMount.setAttribute('id', 'pagefind-page-search');
    doc.body.appendChild(pageMount);
  }

  const parsedUrl = new URL(url);
  const uiInstances = [];
  let loadedScripts = [];

  const win = {
    Event: FakeEvent,
    location: {
      get href() {
        return parsedUrl.href;
      },
      get search() {
        return parsedUrl.search;
      }
    },
    history: {
      replaceState(_state, _title, nextUrl) {
        const updated = new URL(nextUrl);
        parsedUrl.href = updated.href;
      }
    },
    PagefindUI: undefined
  };

  const loadScript = async (scriptUrl) => {
    loadedScripts.push(scriptUrl);
    if (scriptShouldFail) {
      throw new Error('Network error');
    }
    win.PagefindUI = class FakePagefindUI {
      constructor(options) {
        this.options = options;
        this.triggeredQueries = [];
        const container =
          typeof options.element === 'string'
            ? doc.body.querySelector(options.element)
            : options.element;
        const input = doc.createElement('input');
        input.setAttribute('class', 'pagefind-ui__search-input');
        container.appendChild(input);
        this.input = input;
        uiInstances.push(this);
      }

      triggerSearch(query) {
        this.triggeredQueries.push(query);
        this.input.value = query;
      }
    };
  };

  return { doc, win, triggerButton, pageMount, uiInstances, loadedScripts, loadScript };
}

describe('Pagefind client search controller behaviour', () => {
  it('TC-06: resolves bundlePath, baseUrl, and result links accurately for root and GitHub Pages subpath', () => {
    const cases = [
      {
        moduleUrl: 'http://localhost:8080/assets/js/pagefind-search.js',
        expectedBase: '/',
        expectedBundle: '/pagefind/',
        rawResultUrl: '/explanation/architecture/',
        expectedResultUrl: '/explanation/architecture/',
        expectedSearchUrl: '/search/?q=docker'
      },
      {
        moduleUrl:
          'https://dfe-digital.github.io/education-benchmarking-and-insights/assets/js/pagefind-search.js',
        expectedBase: '/education-benchmarking-and-insights/',
        expectedBundle: '/education-benchmarking-and-insights/pagefind/',
        rawResultUrl: '/explanation/architecture/',
        expectedResultUrl: '/education-benchmarking-and-insights/explanation/architecture/',
        expectedSearchUrl: '/education-benchmarking-and-insights/search/?q=docker'
      }
    ];

    for (const testCase of cases) {
      const { baseUrl, bundlePath } = resolveSearchPaths(testCase.moduleUrl);
      assert.equal(baseUrl, testCase.expectedBase);
      assert.equal(bundlePath, testCase.expectedBundle);
      assert.equal(normalizeResultUrl(testCase.rawResultUrl, baseUrl), testCase.expectedResultUrl);
      assert.equal(
        normalizeResultUrl(testCase.expectedResultUrl, baseUrl),
        testCase.expectedResultUrl
      );
      assert.equal(buildSearchPageUrl(baseUrl, 'docker'), testCase.expectedSearchUrl);
    }
  });

  it('TC-14: does not load pagefind-ui.js on regular content pages until search is opened', () => {
    const env = createFakeEnvironment({ url: 'http://localhost:8080/explanation/' });
    initPagefindSearch({
      doc: env.doc,
      win: env.win,
      moduleUrl: 'http://localhost:8080/assets/js/pagefind-search.js',
      loadScript: env.loadScript
    });

    assert.equal(env.loadedScripts.length, 0);
  });

  it('TC-04 & TC-16: opens accessible dialog via button or shortcut, focuses input, updates full-search link, and restores focus on Escape', async () => {
    const env = createFakeEnvironment({ url: 'http://localhost:8080/' });
    const controller = initPagefindSearch({
      doc: env.doc,
      win: env.win,
      moduleUrl: 'http://localhost:8080/assets/js/pagefind-search.js',
      loadScript: env.loadScript
    });

    env.triggerButton.focus();
    await controller.openModal(env.triggerButton);

    const dialog = env.doc.getElementById('pagefind-search-dialog');
    assert.equal(dialog.open, true);
    assert.equal(dialog.getAttribute('aria-labelledby'), 'pagefind-dialog-title');
    assert.deepEqual(env.loadedScripts, ['/pagefind/pagefind-ui.js']);

    const modalInput = dialog.querySelector('.pagefind-ui__search-input');
    assert.equal(env.doc.activeElement, modalInput);

    modalInput.value = 'cfr metrics';
    dialog.querySelector('#pagefind-modal-mount').dispatchEvent(
      new FakeEvent('input', { target: modalInput })
    );
    assert.equal(
      dialog.querySelector('#pagefind-full-search-link').href,
      '/search/?q=cfr+metrics'
    );

    env.doc.dispatchEvent(new FakeEvent('keydown', { key: 'Escape', target: modalInput }));
    assert.equal(dialog.open, false);
    assert.equal(env.doc.activeElement, env.triggerButton);
  });

  it('TC-12: ignores slash (/) shortcut when typing inside form controls or contenteditable elements', () => {
    const env = createFakeEnvironment({ url: 'http://localhost:8080/' });
    initPagefindSearch({
      doc: env.doc,
      win: env.win,
      moduleUrl: 'http://localhost:8080/assets/js/pagefind-search.js',
      loadScript: env.loadScript
    });

    const textInput = env.doc.createElement('input');
    const slashInInput = new FakeEvent('keydown', { key: '/', target: textInput });
    env.doc.dispatchEvent(slashInInput);

    assert.equal(slashInInput.defaultPrevented, false);
    assert.equal(env.doc.getElementById('pagefind-search-dialog'), null);
    assert.equal(isEditableTarget({ tagName: 'TEXTAREA' }), true);
    assert.equal(isEditableTarget({ isContentEditable: true }), true);
  });

  it('TC-05: hydrates /search/?q= query parameter and synchronises input edits via history.replaceState', async () => {
    const env = createFakeEnvironment({
      url: 'http://localhost:8080/search/?q=databricks',
      includeSearchPageMount: true
    });

    const controller = initPagefindSearch({
      doc: env.doc,
      win: env.win,
      moduleUrl: 'http://localhost:8080/assets/js/pagefind-search.js',
      loadScript: env.loadScript
    });
    await controller.pageReady;

    assert.equal(env.uiInstances.length, 1);
    assert.deepEqual(env.uiInstances[0].triggeredQueries, ['databricks']);
    assert.deepEqual(env.uiInstances[0].options.openFilters, ['section']);

    const searchInput = env.uiInstances[0].input;
    searchInput.value = 'mermaid';
    env.pageMount.dispatchEvent(new FakeEvent('input', { target: searchInput }));
    assert.equal(env.win.location.search, '?q=mermaid');

    searchInput.value = '   ';
    env.pageMount.dispatchEvent(new FakeEvent('input', { target: searchInput }));
    assert.equal(env.win.location.search, '');
  });

  it('TC-11 & TC-13: caps ?q= hydration at 256 characters and encodes metacharacters/XSS strings safely without innerHTML', async () => {
    const longUntrusted = `${'a'.repeat(400)}<script>alert(1)</script>[(+*?\\`;
    assert.equal(sanitizeSearchQuery(longUntrusted).length, MAX_QUERY_LENGTH);

    const encodedUrl = `http://localhost:8080/search/?q=${encodeURIComponent(longUntrusted)}`;
    const env = createFakeEnvironment({
      url: encodedUrl,
      includeSearchPageMount: true
    });

    const controller = initPagefindSearch({
      doc: env.doc,
      win: env.win,
      moduleUrl: 'http://localhost:8080/assets/js/pagefind-search.js',
      loadScript: env.loadScript
    });
    await controller.pageReady;

    assert.equal(env.uiInstances[0].triggeredQueries[0].length, MAX_QUERY_LENGTH);
    const builtLink = buildSearchPageUrl('/', '[(+*?\\<script>alert(1)</script>');
    assert.match(builtLink, /^\/search\/\?q=/);
    assert.doesNotMatch(builtLink, /<script>/);
  });

  it('TC-10: renders accessible fallback notice with sitemap link when Pagefind script fails to load', async () => {
    const env = createFakeEnvironment({
      url: 'https://dfe-digital.github.io/education-benchmarking-and-insights/search/',
      includeSearchPageMount: true,
      scriptShouldFail: true
    });

    const controller = initPagefindSearch({
      doc: env.doc,
      win: env.win,
      moduleUrl:
        'https://dfe-digital.github.io/education-benchmarking-and-insights/assets/js/pagefind-search.js',
      loadScript: env.loadScript
    });
    await controller.pageReady;

    const fallbackLink = env.pageMount.querySelector('.govuk-link');
    assert.equal(fallbackLink.href, '/education-benchmarking-and-insights/sitemap/');
    assert.match(env.pageMount.fullText, /Search is currently unavailable/);
  });
});
