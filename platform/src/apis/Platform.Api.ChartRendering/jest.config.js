/** @type {import('ts-jest').JestConfigWithTsJest} **/
export default {
  testEnvironment: "node",
  testMatch: ["**/tests/**/*.[t]s?(x)"],
  transform: {
    // eslint-disable-next-line no-useless-escape
    "^.+\.tsx?$": ["ts-jest", { diagnostics: { ignoreCodes: ["TS151001"] } }],
  },
  modulePathIgnorePatterns: [
    "<rootDir>/node_modules",
    "<rootDir>/out",
    "<rootDir>/src/openapi",
  ],
  reporters: [
    "default",
    [
      "jest-junit",
      {
        suiteName: "Chart Rendering API",
        outputDirectory: "./tests/out",
        outputName: "test-output.xml",
      },
    ],
  ],
  // https://github.com/jestjs/jest/issues/14911#issuecomment-1954962725
  moduleNameMapper: {
    "^d3-(.+)$": "<rootDir>/node_modules/d3-$1/dist/d3-$1.js",
  },
};
