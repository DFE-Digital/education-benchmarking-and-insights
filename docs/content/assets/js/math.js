export const KATEX_CSS_URL = 'https://cdn.jsdelivr.net/npm/katex@0.16.21/dist/katex.min.css';
export const KATEX_JS_URL = 'https://cdn.jsdelivr.net/npm/katex@0.16.21/dist/katex.mjs';

async function defaultLoadKatex() {
  return import(KATEX_JS_URL);
}

export async function initMathRendering({
  doc = typeof document !== 'undefined' ? document : null,
  loadKatex = defaultLoadKatex
} = {}) {
  if (!doc) {
    return;
  }

  const elements = Array.from(doc.querySelectorAll('[data-math]')).filter(
    (el) => el.getAttribute('data-math-rendered') !== 'true'
  );

  if (elements.length === 0) {
    return;
  }

  const headLinks = doc.head?.querySelectorAll ? Array.from(doc.head.querySelectorAll('link')) : [];
  const hasCss = headLinks.some((link) => link.href === KATEX_CSS_URL);

  if (!hasCss && doc.head && typeof doc.createElement === 'function') {
    const link = doc.createElement('link');
    link.rel = 'stylesheet';
    link.href = KATEX_CSS_URL;
    doc.head.appendChild(link);
  }

  try {
    const katexModule = await loadKatex();
    const katex = katexModule?.default || katexModule;

    if (!katex || typeof katex.render !== 'function') {
      throw new Error('KaTeX render function not found');
    }

    for (const el of elements) {
      const mode = el.getAttribute('data-math');
      const displayMode = mode === 'block';
      const expression = el.textContent ?? '';

      katex.render(expression, el, {
        displayMode,
        throwOnError: false,
        trust: false
      });
      el.setAttribute('data-math-rendered', 'true');
    }
  } catch (err) {
    console.error('KaTeX rendering failed:', err);
  }
}

if (typeof window !== 'undefined' && typeof document !== 'undefined') {
  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', () => {
      initMathRendering().catch((err) => console.error('KaTeX rendering failed:', err));
    });
  } else {
    initMathRendering().catch((err) => console.error('KaTeX rendering failed:', err));
  }
}
