var gulp = require("gulp");
var sass = require("gulp-dart-sass");
var async = require("async");
var rename = require("gulp-rename");
var cleanCSS = require('gulp-clean-css');
var uglify = require('gulp-uglify');
var rimraf = require("rimraf");
var connect = require('gulp-connect');
var concat = require('gulp-concat');
var sourcemaps = require('gulp-sourcemaps');
var babel = require('gulp-babel');
var webpack = require('webpack-stream');
var ContextReplacementPlugin = require('webpack').ContextReplacementPlugin;
var path = require('path');
var SourceMapDevToolPlugin = require('webpack').SourceMapDevToolPlugin;
const buildSass = () => gulp.src("AssetSrc/scss/*.scss")
	.pipe(sass().on("error", sass.logError))
	.pipe(cleanCSS())
	.pipe(gulp.dest("wwwroot/css"));

const copyGovukAssets = () => gulp.src(["node_modules/govuk-frontend/dist/govuk/assets/**/*"]).pipe(gulp.dest("wwwroot/assets")).on("end", () =>
	gulp.src(["node_modules/govuk-frontend/dist/govuk/govuk-frontend.min.js"]).pipe(gulp.dest("wwwroot/js/")));

let paths = {
	src: "AssetSrc/",
	dist: "wwwroot/",
	temp: ".temp/"
};

paths.assetsDest = paths.dist + "assets/";

paths.css = paths.temp + "css/**/*.css";
paths.css = paths.temp + "css/**/*.css";
paths.minCss = paths.temp + "css/**/*.min.css";
paths.scss = paths.src + "scss/**/*.scss";
paths.cssDest = paths.assetsDest + "css/";

paths.favicon = paths.src + "favicon.ico";
paths.robots = paths.src + "robots.txt";
paths.appleicon = paths.src + "apple-touch-icon.png";

paths.images = paths.src + "images/**/*";
paths.imagesDest = paths.assetsDest + "images/";

paths.js = [paths.src + "js"];

paths.jsDest = paths.dist + "/js/";

const buildJs = () => gulp.src(paths.js, { sourcemaps: true })
	.pipe(webpack({
		entry: "./" + paths.src + 'js/site.js',
		devtool: false,
		mode: 'production',
		module: {
			rules: [{
				test: /\.jsx$/,
				use: {
					loader: 'babel-loader',
					options: {
						presets: ['@babel/preset-env', '@babel/preset-react'],
					},
				},
			},
				{
					test: /\.m?js$/,
					use: {
						loader: 'babel-loader',
						options: {
							presets: ['@babel/preset-env']
						}
					}
				}],
		},
		plugins: [
			new ContextReplacementPlugin(/moment[\/\\]locale$/, /en\-gb/),
			new SourceMapDevToolPlugin({
				filename: '[file].map'
			})
		],
		output: {
			path: path.resolve(paths.jsDest),
			filename: 'main.js',
			publicPath: '/js/'
		},
		resolve: {
			modules: paths.js.concat('node_modules'),
			extensions: ['.js', '.jsx', '.mjs']
		}
	}))
	.pipe(gulp.dest(paths.jsDest));


gulp.task("build-fe", () => {
	return async.series([
		(next) => buildSass().on("end", next),
		(next) => buildJs().on("end", next),
		(next) => copyGovukAssets().on("end", next)
	])
}); 