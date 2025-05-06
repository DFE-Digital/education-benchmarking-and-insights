const path = require("path");

module.exports = {
  mode: "production",
  entry: "./AssetSrc/ts/index.ts",
  module: {
    rules: [
      {
        test: /\.ts$/,
        exclude: /(node_modules)/,
        use: {
          // `.swcrc` can be used to configure swc
          loader: "swc-loader",
        },
      },
    ],
  },
  experiments: {
    outputModule: true,
  },
  output: {
    libraryTarget: "module",
    path: path.join(__dirname, "./dist"),
    filename: "[name].min.js",
  },
  devtool: "source-map",
};
