import { describe, expect, it } from "@jest/globals";
import {
  normaliseData,
  getValueFormat,
  getGroups,
  getTextWidth,
} from "../../src/functions/utils";
import { ValueType } from "../../src/functions/index";
import theoretically from "jest-theories";

describe("normaliseData", () => {
  const sampleData = [
    { category: "A", value: 50 },
    { category: "B", value: 100 },
  ];

  it("should divide values by 100 for 'percent' type", () => {
    const result = normaliseData(sampleData, "value", "percent");
    expect(result).toEqual([
      { category: "A", value: 0.5 },
      { category: "B", value: 1 },
    ]);
  });

  it("should return original data for 'currency' type", () => {
    const result = normaliseData(sampleData, "value", "currency");
    expect(result).toEqual(sampleData);
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

  it("should return '$,~s' for 'currency'", () => {
    expect(getValueFormat("currency")).toBe("$,~s");
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

describe("getTextWidth", () => {
  describe("should return expected width for given string and font style", () => {
    const theories: { text: string; bold?: boolean; expected: number }[] = [
      { text: "", expected: 0 },
      { text: "Hello, world", expected: 73.359375 },
      { text: "hello, world", expected: 70.671875 },
      { text: "Hello, world", bold: true, expected: 77.0625 },
      { text: "hello, world", bold: true, expected: 74.578125 },
    ];

    theoretically(
      "the string {text} returns the expected width {expected}",
      theories,
      ({ text, bold, expected }) => {
        expect(getTextWidth(text, bold)).toBe(expected);
      },
    );
  });
});
