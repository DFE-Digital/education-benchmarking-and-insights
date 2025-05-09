/** @type {import('ts-jest').JestConfigWithTsJest} **/
export default {
  testEnvironment: "node",
  testMatch: ["**/tests/**/*.[jt]s?(x)"],
  transform: {
    // eslint-disable-next-line no-useless-escape
    "^.+\.tsx?$": ["ts-jest", { diagnostics: { ignoreCodes: ["TS151001"] } }],
  },
  modulePathIgnorePatterns: ["<rootDir>/out"],
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
};
