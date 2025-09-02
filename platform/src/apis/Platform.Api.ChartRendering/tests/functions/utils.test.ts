import { describe, expect, it } from "@jest/globals";
import {
  normaliseData,
  getValueFormat,
  getGroups,
  escapeXml,
} from "../../src/functions/utils";
import { ValueType } from "../../src/functions/index";
import theoretically from "jest-theories";

describe("normaliseData", () => {
  const sampleData = [
    { category: "A", value: 50 },
    { category: "B", value: 100 },
    { category: "C", value: 0 },
    { category: "D", value: undefined },
  ];

  it("should divide values by 100 for 'percent' type", () => {
    const result = normaliseData(sampleData, "value", "percent");
    expect(result).toEqual([
      { category: "A", value: 0.5 },
      { category: "B", value: 1 },
      { category: "C", value: 0 },
      { category: "D", value: 0 },
    ]);
  });

  it("should return original data for 'currency' type", () => {
    const result = normaliseData(sampleData, "value", "currency");
    expect(result).toEqual([
      { category: "A", value: 50 },
      { category: "B", value: 100 },
      { category: "C", value: 0 },
      { category: "D", value: 0 },
    ]);
  });

  it("should throw for unsupported ValueType", () => {
    expect(() =>
      normaliseData(sampleData, "value", "invalid" as ValueType),
    ).toThrow("Argument out of range: unsupported ValueType 'invalid'");
  });
});

describe("getValueFormat", () => {
  it("should return '.1%' for 'percent'", () => {
    expect(getValueFormat("percent")).toBe(".1%");
  });

  it("should return '$,.1~s' for 'currency'", () => {
    expect(getValueFormat("currency")).toBe("$,.1~s");
  });

  it("should return '$,.1~s' for 'currency' when maximum value >= 1000", () => {
    expect(getValueFormat("currency", 1000)).toBe("$,.1~s");
  });

  it("should return '$.0f' for 'currency' when maximum value < 1000", () => {
    expect(getValueFormat("currency", 999)).toBe("$.0f");
  });

  it("should throw for unsupported ValueType", () => {
    expect(() => getValueFormat("invalid" as ValueType)).toThrow(
      "Argument out of range: unsupported ValueType 'invalid'",
    );
  });
});

describe("getGroups", () => {
  const sampleGroups = {
    "group-1": ["123", "456"],
    "group-2": ["123"],
  };

  it("should return empty array for empty groupedKeys", () => {
    expect(getGroups(undefined, "123")).toStrictEqual([]);
  });

  describe("should return expected array for matching key in groupedKeys", () => {
    const theories: { input: string; expected: string[] }[] = [
      { input: "123", expected: ["group-1", "group-2"] },
      { input: "456", expected: ["group-1"] },
      { input: "789", expected: [] },
    ];

    theoretically(
      "the key {input} returns the expected group(s) {expected}",
      theories,
      ({ input, expected }) => {
        expect(getGroups(sampleGroups, input)).toStrictEqual(expected);
      },
    );
  });
});

describe("escapeXml", () => {
  describe("should return expected escaped value", () => {
    const theories: { text?: string; expected: string }[] = [
      { expected: "" },
      { text: "Hello, world", expected: "Hello, world" },
      { text: "Hello & world", expected: "Hello &amp; world" },
      { text: "Hello, <world>", expected: "Hello, &lt;world&gt;" },
      { text: "'Hello', world", expected: "&apos;Hello&apos;, world" },
      { text: '"Hello", world', expected: "&quot;Hello&quot;, world" },
    ];

    theoretically(
      "the string {text} returns the expected escaped value {expected}",
      theories,
      ({ text, expected }) => {
        expect(escapeXml(text)).toBe(expected);
      },
    );
  });
});
