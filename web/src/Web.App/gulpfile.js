var gulp = require("gulp");
var sass = require("gulp-dart-sass");
var async = require("async");
var cleanCSS = require("gulp-clean-css");
var sourcemaps = require("gulp-sourcemaps");

const buildSass = () => gulp.src("AssetSrc/scss/*.scss")
    .pipe(sourcemaps.init())
    .pipe(sass().on("error", sass.logError))
    .pipe(sourcemaps.write("./"))
    .pipe(cleanCSS())
    .pipe(gulp.dest("wwwroot/css"));

const copyStaticAssets = () => gulp.src(["node_modules/govuk-frontend/dist/govuk/assets/**/*"], {encoding: false}).pipe(gulp.dest("wwwroot/assets")).on("end", () =>
    gulp.src(["node_modules/govuk-frontend/dist/govuk/assets/images/favicon.ico"], {encoding: false}).pipe(gulp.dest("wwwroot/"))).on("end", () =>
    gulp.src(["node_modules/govuk-frontend/dist/govuk/govuk-frontend.min.js"]).pipe(gulp.dest("wwwroot/js/"))).on("end", () =>
    gulp.src(["node_modules/front-end/dist/front-end.js"]).pipe(gulp.dest("wwwroot/js/"))).on("end", () =>
    gulp.src(["node_modules/front-end/dist/front-end.css"]).pipe(gulp.dest("wwwroot/css/"))).on("end", () =>
    gulp.src(["AssetSrc/images/*"], {encoding: false}).pipe(gulp.dest("wwwroot/assets/images")));


gulp.task("build-fe", () => {
    return async.series([
        (next) => buildSass().on("end", next),
        (next) => copyStaticAssets().on("end", next)
    ])
}); 