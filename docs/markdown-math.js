function mathBlock(state, startLine, endLine, silent) {
  if (state.sCount[startLine] - state.blkIndent >= 4) {
    return false;
  }

  const startPos = state.bMarks[startLine] + state.tShift[startLine];
  const max = state.eMarks[startLine];

  if (startPos + 2 > max) {
    return false;
  }
  if (state.src.charCodeAt(startPos) !== 0x24 || state.src.charCodeAt(startPos + 1) !== 0x24) {
    return false;
  }
  if (startPos + 2 < max && state.src.charCodeAt(startPos + 2) === 0x24) {
    return false;
  }

  const firstLineAfterOpen = startPos + 2;
  const singleLineClose = state.src.indexOf('$$', firstLineAfterOpen);

  let content = '';
  let endNextLine = startLine + 1;

  if (singleLineClose !== -1 && singleLineClose < max) {
    const trailing = state.src.slice(singleLineClose + 2, max).trim();
    if (trailing === '') {
      content = state.src.slice(firstLineAfterOpen, singleLineClose).trim();
    } else {
      return false;
    }
  } else {
    let foundClose = false;
    let closePos = -1;

    for (let nextLine = startLine + 1; nextLine < endLine; nextLine++) {
      const lineStart = state.bMarks[nextLine] + state.tShift[nextLine];
      const lineEnd = state.eMarks[nextLine];

      const idx = state.src.indexOf('$$', lineStart);
      if (idx !== -1 && idx <= lineEnd) {
        const trailing = state.src.slice(idx + 2, lineEnd).trim();
        if (trailing === '') {
          foundClose = true;
          closePos = idx;
          endNextLine = nextLine + 1;
          break;
        }
      }
    }

    if (!foundClose) {
      return false;
    }

    content = state.src.slice(firstLineAfterOpen, closePos).trim();
  }

  if (silent) {
    return true;
  }

  const token = state.push('math_block', 'div', 0);
  token.block = true;
  token.content = content;
  token.map = [startLine, endNextLine];

  state.line = endNextLine;
  return true;
}

function mathInline(state, silent) {
  const start = state.pos;
  const max = state.posMax;

  if (state.src.charCodeAt(start) !== 0x24) {
    return false;
  }
  if (start + 1 < max && state.src.charCodeAt(start + 1) === 0x24) {
    return false;
  }
  if (start + 1 < max && /\d/.test(state.src.charAt(start + 1))) {
    return false;
  }
  if (start > 0 && state.src.charCodeAt(start - 1) === 0x5C) {
    return false;
  }
  if (start > 0 && /\w/.test(state.src.charAt(start - 1))) {
    return false;
  }

  let match = start + 1;
  let found = false;

  while (match < max) {
    const ch = state.src.charCodeAt(match);
    if (ch === 0x0A) {
      return false;
    }
    if (ch === 0x5C) {
      match += 2;
      continue;
    }
    if (ch === 0x24) {
      if (match + 1 < max && state.src.charCodeAt(match + 1) === 0x24) {
        match += 2;
        continue;
      }
      if (match + 1 < max && /\d/.test(state.src.charAt(match + 1))) {
        match += 1;
        continue;
      }
      if (match + 1 < max && /\w/.test(state.src.charAt(match + 1))) {
        match += 1;
        continue;
      }
      found = true;
      break;
    }
    match += 1;
  }

  if (!found) {
    return false;
  }

  const content = state.src.slice(start + 1, match).trim();
  if (content.length === 0) {
    return false;
  }

  if (!silent) {
    const token = state.push('math_inline', 'span', 0);
    token.content = content;
  }

  state.pos = match + 1;
  return true;
}

export function markdownMath(md) {
  md.block.ruler.before('fence', 'math_block', mathBlock, {
    alt: ['paragraph', 'reference', 'blockquote', 'list']
  });

  md.inline.ruler.after('backticks', 'math_inline', mathInline);

  md.renderer.rules.math_block = (tokens, idx) => {
    const token = tokens[idx];
    return `<div class="app-math app-math--block" data-math="block">${md.utils.escapeHtml(token.content)}</div>\n`;
  };

  md.renderer.rules.math_inline = (tokens, idx) => {
    const token = tokens[idx];
    return `<span class="app-math app-math--inline" data-math="inline">${md.utils.escapeHtml(token.content)}</span>`;
  };
}
