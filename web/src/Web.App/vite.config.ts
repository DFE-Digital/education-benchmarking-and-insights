import { dirname, resolve } from 'node:path'
import { fileURLToPath, URL } from "node:url";
import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";
import vueJsx from "@vitejs/plugin-vue-jsx";
import vueDevTools from "vite-plugin-vue-devtools";

const __dirname = dirname(fileURLToPath(import.meta.url))

// https://vite.dev/config/
export default defineConfig({
  plugins: [vue(), vueJsx(), vueDevTools()],
  resolve: {
    alias: {
      "@": fileURLToPath(new URL("./AssetSrc/ts", import.meta.url)),
    },
  },
  build: {
    lib: {
      entry: resolve(__dirname, "AssetSrc/ts/main.ts"),
      name: "main",
      // the proper extensions will be added
      fileName: "main",
    },
    rollupOptions: {
      // make sure to externalize deps that shouldn't be bundled into your library
      external: [],
      output: {
        // Provide global variables to use in the UMD build for externalized deps
        globals: {},
      },
    },
    sourcemap: true,
    outDir: "dist/vite"
  },
  define: {
    "process.env.NODE_ENV": JSON.stringify(process.env.NODE_ENV),
  }
});
