const { src, dest, series, watch } = require("gulp");
const del = require("del");
const nunjucksRender = require("gulp-nunjucks-render");
const beautify = require("gulp-beautify");
const copy = require("gulp-copy");
const markdown = require("marked");
const DOMPurify = require("isomorphic-dompurify");
const replace = require("gulp-replace");

function clean() {
  return del.deleteAsync(["dist"]);
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

markdown.use({ renderer });

const data = {
  markdown_content: process.env.MARKDOWN_CONTENT
    ? DOMPurify.sanitize(markdown.parse(process.env.MARKDOWN_CONTENT))
    : undefined,
};

function html() {
  return src("src/templates/pages/*.+(njk)")
    .pipe(
      nunjucksRender({
        path: ["node_modules/govuk-frontend/dist", "src/templates"],
        ext: ".html",
        data,
        manageEnv,
      })
    )
    .pipe(beautify.html({ indent_size: 4, preserve_newlines: false }))
    .pipe(dest("dist"));
}

// https://frontend.design-system.service.gov.uk/import-font-and-images-assets/#copy-the-font-and-image-files-into-your-application
function assets() {
  return src([
    "node_modules/govuk-frontend/dist/govuk/assets/images/**",
    "node_modules/govuk-frontend/dist/govuk/assets/fonts/**",
    "node_modules/govuk-frontend/dist/govuk/assets/manifest.json",
    "node_modules/govuk-frontend/dist/govuk/assets/rebrand/**",
  ]).pipe(
    copy("dist/assets", {
      prefix: 5,
    })
  );
}

function stylesheets() {
  return src(["node_modules/govuk-frontend/dist/govuk/govuk-frontend.min.css*"])
    .pipe(
      copy("dist/assets/styles", {
        prefix: 4,
      })
    .pipe(replace("url(/assets", `url(../../${assetsPath}`)))
    .pipe(dest("dist/assets/styles"));
}

function watchFiles() {
  watch("src/templates/**/*", html);
}

exports.build = series(clean, html, assets, stylesheets);
exports.default = series(clean, html, assets, stylesheets, watchFiles);
