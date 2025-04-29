const gulp = require("gulp");
const sass = require("gulp-dart-sass");
const async = require("async");
const cleanCSS = require("gulp-clean-css");
const sourcemaps = require("gulp-sourcemaps");
const rename = require("gulp-rename");
const swc = require("gulp-swc");
const webpack = require("webpack-stream");
const named = require("vinyl-named");
const through = require("through2");

const buildSass = () => gulp.src("AssetSrc/scss/*.scss")
    .pipe(sourcemaps.init())
    .pipe(sass().on("error", sass.logError))
    .pipe(cleanCSS())
    .pipe(sourcemaps.write("./"))
    .pipe(gulp.dest("wwwroot/css"));

const buildTs = () => gulp.src("AssetSrc/ts/*.ts")
    .pipe(sourcemaps.init())
    .pipe(swc())
    .pipe(rename("main.js"))
    .pipe(sourcemaps.write("./"))
    .pipe(gulp.dest("wwwroot/js"));

// noinspection JSCheckFunctionSignatures
const buildWebpack = () => gulp.src("wwwroot/js/main.js")
    .pipe(named())
    .pipe(webpack(require("./webpack.config.cjs")))
    .pipe(sourcemaps.init({loadMaps: true}))
    .pipe(through.obj(function (file, enc, cb) {
        const isSourceMap = /\.map$/.test(file.path);
        if (!isSourceMap) this.push(file);
        cb();
    }))
    .pipe(sourcemaps.write("./"))
    .pipe(gulp.dest("wwwroot/js"));

const copyStaticAssets = () => gulp.src(["node_modules/govuk-frontend/dist/govuk/assets/**/*"], {encoding: false}).pipe(gulp.dest("wwwroot/assets"))
    .on("end", () =>
        gulp.src(["node_modules/govuk-frontend/dist/govuk/assets/images/favicon.ico"], {encoding: false})
            .pipe(gulp.dest("wwwroot/")))
    .on("end", () =>
        gulp.src(["node_modules/govuk-frontend/dist/govuk/assets/images/govuk-icon-180.png"], {encoding: false})
            .pipe(rename("apple-touch-icon.png")).pipe(gulp.dest("wwwroot/"))
            .pipe(rename("apple-touch-icon-120x120.png")).pipe(gulp.dest("wwwroot/"))
            .pipe(rename("apple-touch-icon-precomposed.png")).pipe(gulp.dest("wwwroot/")))
    .on("end", () =>
        gulp.src(["node_modules/govuk-frontend/dist/govuk/govuk-frontend.min.js*"])
            .pipe(gulp.dest("wwwroot/js/")))
    .on("end", () =>
        gulp.src(["node_modules/accessible-autocomplete/dist/accessible-autocomplete.min.js*"])
            .pipe(gulp.dest("wwwroot/js/")))
    .on("end", () =>
        gulp.src(["node_modules/front-end/dist/front-end.js"])
            .pipe(gulp.dest("wwwroot/js/")))
    .on("end", () =>
        gulp.src(["node_modules/front-end/dist/front-end.css"])
            .pipe(gulp.dest("wwwroot/css/")))
    .on("end", () =>
        gulp.src(["AssetSrc/images/*"], {encoding: false})
            .pipe(gulp.dest("wwwroot/assets/images")));

gulp.task("build-fe", () => {
    return async.series([
        //(next) => buildSass().on("end", next),
        (next) => buildTs().on("end", next),
        (next) => buildWebpack().on("end", next),
        //(next) => copyStaticAssets().on("end", next)
    ])
}); 