/**
 * @import {RendererObject} from "marked"
 */
import { use, parse } from "marked";
import DOMPurify from "isomorphic-dompurify";

export const options = (markdown, assetsPath) => ({
  path: ["node_modules/govuk-frontend/dist", "views"],
  ext: ".html",
  data: {
    markdown_content: markdown
      ? DOMPurify.sanitize(parse(markdown))
      : undefined,
  },
  manageEnv: (environment) => {
    environment.addGlobal("govukRebrand", false);
    environment.addGlobal("assetPath", assetsPath);
  },
});

// see https://github.com/markedjs/marked/blob/master/src/Renderer.ts for overridden functions
/** @type {RendererObject} */
const renderer = {
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

use({ renderer });
