import { src, dest, series, watch } from "gulp";
import { deleteAsync } from "del";
import nunjucksRender from "gulp-nunjucks-render";
import beautify from "gulp-beautify";
import copy from "gulp-copy";
import replace from "gulp-replace";
import { options } from "./markdown.js";

const assetsPath = process.env.ASSETS_PATH ?? "/assets";
const markdownContent = process.env.MARKDOWN_CONTENT ?? undefined;
const output = "dist";

function clean() {
  return deleteAsync([output]);
}

function html() {
  return src("views/*.+(njk)")
    .pipe(nunjucksRender(options(markdownContent, assetsPath)))
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
      })
        // update hard-coded asset paths in stylesheet when running in local dev mode
        .pipe(replace("url(/assets", `url(../../${assetsPath}`))
    )
    .pipe(dest(`${output}/assets/styles`));
}

function watchFiles() {
  watch("views/**/*", html);
}

export const build = series(clean, html, assets, stylesheets);
export const _default = series(clean, html, assets, stylesheets, watchFiles);
export { _default as default };
