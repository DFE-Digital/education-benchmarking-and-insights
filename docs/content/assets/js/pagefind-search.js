export const MAX_QUERY_LENGTH = 256;

export function resolveSearchPaths(moduleUrl) {
  const baseUrl = new URL('../../', moduleUrl).pathname;
  const bundlePath = new URL('../../pagefind/', moduleUrl).pathname;
  return { baseUrl, bundlePath };
}

export function sanitizeSearchQuery(rawQuery) {
  if (typeof rawQuery !== 'string') {
    return '';
  }
  return rawQuery.slice(0, MAX_QUERY_LENGTH);
}

export function normalizeResultUrl(rawUrl, baseUrl) {
  if (typeof rawUrl !== 'string' || /^(https?:)?\/\//.test(rawUrl)) {
    return rawUrl;
  }
  const normalizedBase = baseUrl.endsWith('/') ? baseUrl : `${baseUrl}/`;
  if (rawUrl.startsWith(normalizedBase)) {
    return rawUrl;
  }
  const stripped = rawUrl.replace(/^\/+/, '');
  return `${normalizedBase}${stripped}`;
}

export function buildSearchPageUrl(baseUrl, rawQuery) {
  const normalizedBase = baseUrl.endsWith('/') ? baseUrl : `${baseUrl}/`;
  const searchPath = `${normalizedBase}search/`;
  const cleanQuery = sanitizeSearchQuery(rawQuery).trim();
  if (!cleanQuery) {
    return searchPath;
  }
  const params = new URLSearchParams({ q: cleanQuery });
  return `${searchPath}?${params.toString()}`;
}

export function isEditableTarget(target) {
  if (!target || typeof target !== 'object') {
    return false;
  }
  const tagName = typeof target.tagName === 'string' ? target.tagName.toUpperCase() : '';
  if (tagName === 'INPUT' || tagName === 'TEXTAREA' || tagName === 'SELECT') {
    return true;
  }
  return Boolean(target.isContentEditable);
}

function renderFallbackNotice(doc, container, baseUrl) {
  container.replaceChildren();
  const paragraph = doc.createElement('p');
  paragraph.className = 'govuk-body app-pagefind-fallback';
  paragraph.append('Search is currently unavailable. Browse all pages using the ');

  const sitemapLink = doc.createElement('a');
  sitemapLink.className = 'govuk-link';
  const normalizedBase = baseUrl.endsWith('/') ? baseUrl : `${baseUrl}/`;
  sitemapLink.href = `${normalizedBase}sitemap/`;
  sitemapLink.textContent = 'sitemap';

  paragraph.append(sitemapLink, '.');
  container.appendChild(paragraph);
}

function defaultLoadScript(doc, scriptUrl) {
  return new Promise((resolve, reject) => {
    const script = doc.createElement('script');
    script.src = scriptUrl;
    script.async = true;
    script.addEventListener('load', () => resolve());
    script.addEventListener('error', () => reject(new Error(`Failed to load ${scriptUrl}`)));
    doc.head.appendChild(script);
  });
}

function buildDialogElement(doc, baseUrl) {
  const dialog = doc.createElement('dialog');
  dialog.id = 'pagefind-search-dialog';
  dialog.className = 'app-pagefind-dialog';
  dialog.setAttribute('aria-labelledby', 'pagefind-dialog-title');

  const header = doc.createElement('div');
  header.className = 'app-pagefind-dialog__header';

  const heading = doc.createElement('h2');
  heading.id = 'pagefind-dialog-title';
  heading.className = 'govuk-heading-m app-pagefind-dialog__title';
  heading.textContent = 'Search documentation';

  const closeButton = doc.createElement('button');
  closeButton.type = 'button';
  closeButton.className = 'govuk-link app-pagefind-dialog__close';
  closeButton.setAttribute('aria-label', 'Close search dialog');
  closeButton.textContent = 'Close';

  header.append(heading, closeButton);

  const body = doc.createElement('div');
  body.id = 'pagefind-modal-mount';
  body.className = 'app-pagefind-dialog__body';

  const footer = doc.createElement('div');
  footer.className = 'app-pagefind-dialog__footer';

  const fullSearchLink = doc.createElement('a');
  fullSearchLink.id = 'pagefind-full-search-link';
  fullSearchLink.className = 'govuk-link app-pagefind-dialog__footer-link';
  fullSearchLink.href = buildSearchPageUrl(baseUrl, '');
  fullSearchLink.textContent = 'Open full search page with section filters';

  footer.appendChild(fullSearchLink);
  dialog.append(header, body, footer);
  doc.body.appendChild(dialog);

  return { dialog, closeButton, body, fullSearchLink };
}

export function initPagefindSearch({
  doc = document,
  win = window,
  moduleUrl = import.meta.url,
  loadScript = (url) => defaultLoadScript(doc, url)
} = {}) {
  const { baseUrl, bundlePath } = resolveSearchPaths(moduleUrl);
  let uiScriptPromise = null;
  let modalState = null;
  let modalMounted = false;
  let lastTriggerElement = null;

  const ensurePagefindUiLoaded = () => {
    if (typeof win.PagefindUI === 'function') {
      return Promise.resolve();
    }
    if (!uiScriptPromise) {
      uiScriptPromise = loadScript(`${bundlePath}pagefind-ui.js`).catch((error) => {
        uiScriptPromise = null;
        throw error;
      });
    }
    return uiScriptPromise;
  };

  const createUiOptions = (extraOptions) => ({
    bundlePath,
    baseUrl,
    showSubResults: true,
    showImages: false,
    resetStyles: false,
    translations: {
      placeholder: 'Search documentation',
      zero_results: 'No results found for [SEARCH_TERM]'
    },
    processResult: (result) => {
      if (result && typeof result === 'object') {
        result.url = normalizeResultUrl(result.url, baseUrl);
        if (Array.isArray(result.sub_results)) {
          for (const sub of result.sub_results) {
            sub.url = normalizeResultUrl(sub.url, baseUrl);
          }
        }
      }
      return result;
    },
    ...extraOptions
  });

  const closeDialog = (dialog) => {
    if (typeof dialog.close === 'function') {
      if (dialog.open) {
        dialog.close();
      }
    } else if (dialog.hasAttribute('open')) {
      dialog.removeAttribute('open');
      dialog.dispatchEvent(new win.Event('close'));
    }
  };

  const openModal = async (triggerElement) => {
    lastTriggerElement = triggerElement || doc.activeElement;

    if (!modalState) {
      modalState = buildDialogElement(doc, baseUrl);

      modalState.closeButton.addEventListener('click', () => {
        closeDialog(modalState.dialog);
      });

      modalState.dialog.addEventListener('click', (event) => {
        if (event.target === modalState.dialog) {
          closeDialog(modalState.dialog);
        }
      });

      modalState.dialog.addEventListener('close', () => {
        if (lastTriggerElement && typeof lastTriggerElement.focus === 'function') {
          lastTriggerElement.focus();
        }
      });

      modalState.body.addEventListener('input', (event) => {
        const target = event.target;
        if (target && target.classList?.contains('pagefind-ui__search-input')) {
          modalState.fullSearchLink.href = buildSearchPageUrl(baseUrl, target.value);
        }
      });
    }

    if (typeof modalState.dialog.showModal === 'function') {
      if (!modalState.dialog.open) {
        modalState.dialog.showModal();
      }
    } else {
      modalState.dialog.setAttribute('open', '');
    }

    if (!modalMounted) {
      try {
        await ensurePagefindUiLoaded();
        if (typeof win.PagefindUI !== 'function') {
          throw new Error('PagefindUI constructor unavailable');
        }
        new win.PagefindUI(
          createUiOptions({
            element: modalState.body,
            autofocus: true
          })
        );
        modalMounted = true;
      } catch {
        renderFallbackNotice(doc, modalState.body, baseUrl);
        return;
      }
    }

    const searchInput = modalState.body.querySelector('.pagefind-ui__search-input');
    if (searchInput && typeof searchInput.focus === 'function') {
      searchInput.focus();
    }
  };

  const triggerButton = doc.getElementById('header-search-button');
  if (triggerButton) {
    triggerButton.addEventListener('click', () => {
      void openModal(triggerButton);
    });
  }

  doc.addEventListener('keydown', (event) => {
    const isCmdOrCtrlK =
      (event.ctrlKey || event.metaKey) &&
      !event.altKey &&
      typeof event.key === 'string' &&
      event.key.toLowerCase() === 'k';

    if (isCmdOrCtrlK) {
      event.preventDefault();
      if (modalState?.dialog.hasAttribute('open') || modalState?.dialog.open) {
        closeDialog(modalState.dialog);
      } else {
        void openModal(doc.activeElement || triggerButton);
      }
      return;
    }

    if (
      event.key === 'Escape' &&
      modalState &&
      (modalState.dialog.open || modalState.dialog.hasAttribute('open'))
    ) {
      closeDialog(modalState.dialog);
      return;
    }

    if (
      event.key === '/' &&
      !event.ctrlKey &&
      !event.metaKey &&
      !event.altKey &&
      !isEditableTarget(event.target)
    ) {
      event.preventDefault();
      void openModal(doc.activeElement || triggerButton);
    }
  });

  const pageSearchContainer = doc.getElementById('pagefind-page-search');
  const pageReady = pageSearchContainer
    ? (async () => {
        try {
          await ensurePagefindUiLoaded();
          if (typeof win.PagefindUI !== 'function') {
            throw new Error('PagefindUI constructor unavailable');
          }
          const ui = new win.PagefindUI(
            createUiOptions({
              element: pageSearchContainer,
              autofocus: true,
              openFilters: ['section']
            })
          );

          const rawQuery = new URLSearchParams(win.location.search).get('q');
          const cleanQuery = sanitizeSearchQuery(rawQuery);
          if (cleanQuery && typeof ui.triggerSearch === 'function') {
            ui.triggerSearch(cleanQuery);
          }

          pageSearchContainer.addEventListener('input', (event) => {
            const target = event.target;
            if (target && target.classList?.contains('pagefind-ui__search-input')) {
              const updatedUrl = new URL(win.location.href);
              const nextQuery = sanitizeSearchQuery(target.value);
              if (nextQuery.trim()) {
                updatedUrl.searchParams.set('q', nextQuery);
              } else {
                updatedUrl.searchParams.delete('q');
              }
              win.history.replaceState(null, '', updatedUrl.toString());
            }
          });
        } catch {
          renderFallbackNotice(doc, pageSearchContainer, baseUrl);
        }
      })()
    : Promise.resolve();

  return { openModal, pageReady };
}

if (typeof document !== 'undefined' && typeof window !== 'undefined') {
  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', () => {
      initPagefindSearch();
    });
  } else {
    initPagefindSearch();
  }
}
