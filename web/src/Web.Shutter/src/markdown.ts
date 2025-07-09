import { RendererObject } from "marked";

// see https://github.com/markedjs/marked/blob/master/src/Renderer.ts for overridden functions
export const markdownRenderer: RendererObject = {
  link({ href, tokens }) {
    const text = this.parser.parseInline(tokens);
    const cleanHref = encodeURI(href);
    if (cleanHref === null) {
      return text;
    }
    href = cleanHref;
    return `<a class="govuk-link" href="${href}">${text}</a>`;
  },
  paragraph({ tokens }) {
    return `<p class="govuk-body">${this.parser.parseInline(tokens)}</p>\n`;
  },
};
