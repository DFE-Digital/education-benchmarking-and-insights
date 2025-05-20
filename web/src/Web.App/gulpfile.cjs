/* eslint-disable @typescript-eslint/no-require-imports */
const gulp = require("gulp");
const sass = require("gulp-dart-sass");
const async = require("async");
const cleanCSS = require("gulp-clean-css");
const sourcemaps = require("gulp-sourcemaps");
const rename = require("gulp-rename");

const buildSass = () =>
  gulp
    .src("AssetSrc/scss/*.scss")
    .pipe(sourcemaps.init())
    .pipe(sass().on("error", sass.logError))
    .pipe(cleanCSS())
    .pipe(sourcemaps.write("./"))
    .pipe(gulp.dest("wwwroot/css"));

const copyStaticAssets = () =>
  gulp
    .src(["node_modules/govuk-frontend/dist/govuk/assets/**/*"], {
      encoding: false,
    })
    .pipe(gulp.dest("wwwroot/assets"))
    .on("end", () =>
      gulp
        .src(
          ["node_modules/govuk-frontend/dist/govuk/assets/images/favicon.ico"],
          { encoding: false }
        )
        .pipe(gulp.dest("wwwroot/"))
    )
    .on("end", () =>
      gulp
        .src(
          [
            "node_modules/govuk-frontend/dist/govuk/assets/images/govuk-icon-180.png",
          ],
          { encoding: false }
        )
        .pipe(rename("apple-touch-icon.png"))
        .pipe(gulp.dest("wwwroot/"))
        .pipe(rename("apple-touch-icon-120x120.png"))
        .pipe(gulp.dest("wwwroot/"))
        .pipe(rename("apple-touch-icon-precomposed.png"))
        .pipe(gulp.dest("wwwroot/"))
    )
    .on("end", () =>
      gulp
        .src(["node_modules/govuk-frontend/dist/govuk/govuk-frontend.min.js*"])
        .pipe(gulp.dest("wwwroot/js/"))
    )
    .on("end", () =>
      gulp
        .src([
          "node_modules/accessible-autocomplete/dist/accessible-autocomplete.min.js*",
        ])
        .pipe(gulp.dest("wwwroot/js/"))
    )
    .on("end", () =>
      gulp
        .src(["node_modules/front-end/dist/front-end.js"])
        .pipe(gulp.dest("wwwroot/js/"))
    )
    .on("end", () =>
      gulp
        .src(["node_modules/front-end/dist/front-end.css"])
        .pipe(gulp.dest("wwwroot/css/"))
    )
    .on("end", () =>
      gulp
        .src(["AssetSrc/images/*"], { encoding: false })
        .pipe(gulp.dest("wwwroot/assets/images"))
    )
    .on("end", () =>
      gulp
        .src(["dist/vite/*"], { encoding: false })
        .pipe(gulp.dest("wwwroot/js/"))
    );

gulp.task("build-fe", () => {
  return async.series([
    (next) => buildSass().on("end", next),
    (next) => copyStaticAssets().on("end", next)
  ]);
});
