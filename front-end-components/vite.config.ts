import { defineConfig } from "vite";
import path from "path";
import baseConfig from "./vite.config.base";

// https://vitejs.dev/config/
export default defineConfig({
  ...baseConfig,
  build: {
    lib: {
      entry: path.resolve(__dirname, "src/main.tsx"),
      name: "Education Benchmarking - Front-end components",
      formats: ["es"],
      fileName: `front-end`,
    },
    rollupOptions: {
      output: {
        assetFileNames: "front-end.[ext]",
      },
    },
  },
  define: { "process.env.NODE_ENV": '"production"' },
});
