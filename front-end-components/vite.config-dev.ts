import { defineConfig } from "vite";
import baseConfig from "./vite.config.base";

// https://vitejs.dev/config/
export default defineConfig({
  ...baseConfig,
  appType: "mpa",
  build: {
    sourcemap: true,
  },
  server: {
    proxy: {
      "/api": {
        target: "https://localhost:7095/api",
        changeOrigin: true,
        secure: false,
        rewrite: (path) => path.replace(/^\/api/, ""),
      },
      "/assets": {
        target:
          "http://localhost:5173/node_modules/govuk-frontend/dist/govuk/assets",
        rewrite: (path) => path.replace(/^\/assets/, ""),
      },
    },
  },
});
