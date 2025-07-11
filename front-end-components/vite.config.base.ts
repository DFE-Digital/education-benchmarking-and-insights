import { UserConfig } from "vite";
import react from "@vitejs/plugin-react";
import path from "path";

const baseConfig: UserConfig = {
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
  css: {
    preprocessorOptions: {
      scss: {
        additionalData: `
          @use "sass:color";
          @import "govuk-frontend/dist/govuk/settings/_index.scss";
          @import "govuk-frontend/dist/govuk/helpers/_index.scss";
          @import "govuk-frontend/dist/govuk/core/_govuk-frontend-properties.scss";
        `,
        quietDeps: true,

        // reduce noise from govuk-frontend
        silenceDeprecations: ["import", "mixed-decls"],
      },
    },
    devSourcemap: true,
  },
};

export default baseConfig;
