// @ts-check
import eslint from "@eslint/js";
import tseslint from "typescript-eslint";
import { fixupConfigRules } from "@eslint/compat";
import { defineConfig  } from "eslint/config";
import reactRefresh from "eslint-plugin-react-refresh";
import path from "node:path";
import { fileURLToPath } from "node:url";
import js from "@eslint/js";
import { FlatCompat } from "@eslint/eslintrc";

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);
const compat = new FlatCompat({
  baseDirectory: __dirname,
  recommendedConfig: eslint.configs.recommended,
  ...tseslint.configs.recommended,
  allConfig: js.configs.all,
});

export default defineConfig(
  {
    ignores: ["**/dist", "**/*.cjs", "**/*.mjs", "**/__*"],
  },
  ...fixupConfigRules(
    compat.extends(
      "eslint:recommended",
      "plugin:@typescript-eslint/recommended",
      "plugin:react-hooks/recommended",
      "plugin:eslint-plugin-prettier/recommended"
    )
  ),
  {
    plugins: {
      "react-refresh": reactRefresh,
    },
    rules: {
      "react-refresh/only-export-components": [
        "warn",
        {
          allowConstantExport: true,
        },
      ],

      "@typescript-eslint/no-empty-object-type": [
        "warn",
        {
          allowInterfaces: "with-single-extends",
        },
      ],
    },
  }
);
