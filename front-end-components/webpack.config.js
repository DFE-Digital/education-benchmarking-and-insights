import path from 'path';

export default {
    entry: "./src/main.tsx",
    output: {
        path: path.resolve(__dirname, "dist"),
        filename: "front-end.js",
    },
    resolve: {
        extensions: [".tsx", ".ts", ".js"],
    },
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                exclude: /node_modules/,
                use: {
                    loader: "ts-loader",
                },
            },
        ],
    }
};