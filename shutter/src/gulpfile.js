import { src, dest, series, watch } from "gulp";
import { deleteAsync } from "del";
import nunjucksRender from "gulp-nunjucks-render";
import beautify from "gulp-beautify";
import copy from "gulp-copy";
import { use as useMarkdownExtension, parse as parseMarkdown } from "marked";
import DOMPurify from "isomorphic-dompurify";
import replace from "gulp-replace";
const output = "dist";

function clean() {
  return deleteAsync([output]);
}

const assetsPath = process.env.ASSETS_PATH ?? "/assets";
const manageEnv = function (environment) {
  environment.addGlobal("govukRebrand", false);
  environment.addGlobal("assetPath", assetsPath);
};

// see https://github.com/markedjs/marked/blob/master/src/Renderer.ts for overridden functions
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

useMarkdownExtension({ renderer });

const data = {
  markdown_content: process.env.MARKDOWN_CONTENT
    ? DOMPurify.sanitize(parseMarkdown(process.env.MARKDOWN_CONTENT))
    : undefined,
};

function html() {
  return src("views/*.+(njk)")
    .pipe(
      nunjucksRender({
        path: ["node_modules/govuk-frontend/dist", "views"],
        ext: ".html",
        data,
        manageEnv,
      })
    )
    .pipe(beautify.html({ indent_size: 4, preserve_newlines: false }))
    .pipe(dest(output));
}

// https://frontend.design-system.service.gov.uk/import-font-and-images-assets/#copy-the-font-and-image-files-into-your-application
function assets() {
  return src([
    "node_modules/govuk-frontend/dist/govuk/assets/images/**",
    "node_modules/govuk-frontend/dist/govuk/assets/fonts/**",
    "node_modules/govuk-frontend/dist/govuk/assets/manifest.json",
    "node_modules/govuk-frontend/dist/govuk/assets/rebrand/**",
  ]).pipe(
    copy(`${output}/assets`, {
      prefix: 5,
    })
  );
}

function stylesheets() {
  return src(["node_modules/govuk-frontend/dist/govuk/govuk-frontend.min.css*"])
    .pipe(
      copy(`${output}/assets/styles`, {
        prefix: 4,
      }).pipe(replace("url(/assets", `url(../../${assetsPath}`))
    )
    .pipe(dest(`${output}/assets/styles`));
}

function watchFiles() {
  watch("views/**/*", html);
}

export const build = series(clean, html, assets, stylesheets);
export const _default = series(clean, html, assets, stylesheets, watchFiles);
export { _default as default };
