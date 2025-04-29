module.exports = {
    mode: "production",
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /(node_modules)/,
                use: {
                    // `.swcrc` can be used to configure swc
                    loader: "swc-loader"
                }
            }
        ]
    },
    experiments: {
        outputModule: true
    },
    output: {
        libraryTarget: "module",
        filename: "main.min.js",
        clean: true,
    },
    devtool: "source-map"
};