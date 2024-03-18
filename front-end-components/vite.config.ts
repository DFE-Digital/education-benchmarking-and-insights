import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import path from "path";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  build: {
    lib: {
      entry: [
        path.resolve(__dirname, "src/front-end.ts"),
        path.resolve(__dirname, "src/server.ts"),
      ],
      name: "Education Benchmarking - Front-end components",
      formats: ["cjs", "es"],
    },
    rollupOptions: {
      output: {
        assetFileNames: "front-end.[ext]",
        chunkFileNames: "[name]-[format].js",
        globals: {
          react: "React",
          "react-dom": "ReactDOM",
          "react-dom/server": "ReactDOMServer",
        },
      },
    },
    sourcemap: true,
  },
  define: { "process.env.NODE_ENV": '"production"' },
  resolve: {
    alias: {
      src: "/src",
      "@govuk-react/icon-crown": path.resolve(
        __dirname,
        "node_modules/@govuk-react/icon-crown"
      ),
    },
  },
});
