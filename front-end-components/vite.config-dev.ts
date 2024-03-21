import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import path from "path";

// https://vitejs.dev/config/
export default defineConfig({
  appType: "mpa",
  plugins: [react()],
  resolve: {
    alias: {
      src: "/src",
      "@govuk-react/icon-crown": path.resolve(
        __dirname,
        "node_modules/@govuk-react/icon-crown"
      ),
    },
  },
  server: {
    proxy: {
      "/api": {
        target: "https://localhost:44393/api",
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
