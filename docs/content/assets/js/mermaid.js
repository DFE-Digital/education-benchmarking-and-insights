import mermaid from 'https://cdn.jsdelivr.net/npm/mermaid@11/dist/mermaid.esm.min.mjs';

mermaid.initialize({
  startOnLoad: false,
  theme: 'default'
});

const style = document.createElement('style');
style.textContent = `
  .mermaid {
    background-color: white;
    padding: 10px;
    margin-bottom: 20px;
    text-align: center;
  }
  .mermaid svg {
    max-width: 100%;
    height: auto;
  }
`;
document.head.appendChild(style);

document.addEventListener('DOMContentLoaded', async () => {
  // Select mermaid blocks (handling both standard and govuk-plugin generated classes)
  const mermaidBlocks = document.querySelectorAll('pre[class*="mermaid"] code, pre code[class*="mermaid"], .language-mermaid');

  if (mermaidBlocks.length === 0) return;

  for (const block of mermaidBlocks) {
    const pre = block.closest('pre') || block;
    const div = document.createElement('div');
    div.className = 'mermaid';
    // Use textContent to get the raw Mermaid code, bypassing any syntax highlighting HTML spans
    div.textContent = block.textContent.trim();
    pre.replaceWith(div);
  }

  try {
    await mermaid.run();
  } catch (err) {
    console.error('Mermaid render failed:', err);
  }
});
