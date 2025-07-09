import gts from "gts/.prettierrc.json" with { type: "json" };

/**
 * @see https://prettier.io/docs/configuration
 * @type {import("prettier").Config}
 */
const config = {
  ...gts,
  bracketSpacing: true,
  tabWidth: 2,
  semi: true,
  singleQuote: false,
};

export default config;
