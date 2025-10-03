import { describe, expect, it } from "@jest/globals";
import {
  normaliseData,
  getValueFormat,
  getGroups,
  escapeXml,
  shortValueFormatter,
  sortData,
  getDomain,
} from "../../src/functions/utils";
import { ValueType } from "../../src/functions/index";
import theoretically from "jest-theories";

describe("normaliseData", () => {
  const sampleData = [
    { category: "A", value: 50 },
    { category: "B", value: 100 },
    { category: "C", value: 0 },
    { category: "D", value: undefined },
    { category: "E", value: null },
  ];

  describe("without normaliseDefault", () => {
    it("should divide values by 100 for 'percent' type or normalise to 0", () => {
      const result = normaliseData(sampleData, "value", "percent");
      expect(result).toEqual([
        { category: "A", value: 0.5 },
        { category: "B", value: 1 },
        { category: "C", value: 0 },
        { category: "D", value: 0 },
        { category: "E", value: 0 },
      ]);
    });

    it("should return original data for 'currency' type or normalise to 0", () => {
      const result = normaliseData(sampleData, "value", "currency");
      expect(result).toEqual([
        { category: "A", value: 50 },
        { category: "B", value: 100 },
        { category: "C", value: 0 },
        { category: "D", value: 0 },
        { category: "E", value: 0 },
      ]);
    });
  });

  describe("with normaliseDefault as null", () => {
    it("should divide values by 100 for 'percent' type or normalise to null", () => {
      const result = normaliseData(sampleData, "value", "percent", null);
      expect(result).toEqual([
        { category: "A", value: 0.5 },
        { category: "B", value: 1 },
        { category: "C", value: 0 },
        { category: "D", value: null },
        { category: "E", value: null },
      ]);
    });

    it("should return original data for 'currency' type or normalise to null", () => {
      const result = normaliseData(sampleData, "value", "currency", null);
      expect(result).toEqual([
        { category: "A", value: 50 },
        { category: "B", value: 100 },
        { category: "C", value: 0 },
        { category: "D", value: null },
        { category: "E", value: null },
      ]);
    });
  });

  it("should throw for unsupported ValueType", () => {
    expect(() =>
      normaliseData(sampleData, "value", "invalid" as ValueType)
    ).toThrow("Argument out of range: unsupported ValueType 'invalid'");
  });
});

describe("sortData", () => {
  const sampleData = [
    { category: "A", value: 50 },
    { category: "B", value: 100 },
    { category: "C", value: 0 },
    { category: "D", value: undefined },
    { category: "E", value: null },
    { category: "F", value: 100 },
    { category: "G", value: 10 },
  ];

  it("should return values ascending for `asc` sort type", () => {
    sortData(sampleData, "value", "asc");
    expect(sampleData).toEqual([
      { category: "C", value: 0 },
      { category: "G", value: 10 },
      { category: "A", value: 50 },
      { category: "B", value: 100 },
      { category: "F", value: 100 },
      { category: "D", value: undefined },
      { category: "E", value: null },
    ]);
  });

  it("should return values descending for `desc` sort type", () => {
    sortData(sampleData, "value", "desc");
    expect(sampleData).toEqual([
      { category: "B", value: 100 },
      { category: "F", value: 100 },
      { category: "A", value: 50 },
      { category: "G", value: 10 },
      { category: "C", value: 0 },
      { category: "D", value: undefined },
      { category: "E", value: null },
    ]);
  });

  it("should return original values for missing sort type", () => {
    sortData(sampleData, "value");
    expect(sampleData).toEqual(sampleData);
  });
});

describe("getDomain", () => {
  const sampleData = [
    { category: "A", value: 50 },
    { category: "B", value: 100 },
    { category: "C", value: 5 },
    { category: "D", value: undefined },
    { category: "E", value: null },
    { category: "F", value: 100 },
    { category: "G", value: 10 },
  ];

  const sampleZeroData = [
    { category: "A", value: 0 },
    { category: "B", value: 0 },
    { category: "C", value: 0 },
  ];

  it("should return default domain if not supplied", () => {
    const result = getDomain(sampleData, "value");
    expect(result).toEqual([0, 100]);
  });

  it("should return custom domain if within range", () => {
    const result = getDomain(sampleData, "value", 4, 1000);
    expect(result).toEqual([4, 1000]);
  });

  it("should return fallback domain if out of range", () => {
    const result = getDomain(sampleData, "value", 6, 99);
    expect(result).toEqual([5, 100]);
  });

  it("should return arbitrary default domain if all values are 0", () => {
    const result = getDomain(sampleZeroData, "value");
    expect(result).toEqual([0, 1000]);
  });

  it("should return arbitrary domain if range is 0, 0", () => {
    const result = getDomain(sampleZeroData, "value", 0, 0);
    expect(result).toEqual([0, 1000]);
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
      "Argument out of range: unsupported ValueType 'invalid'"
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
      }
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
      }
    );
  });
});

describe("shortValueFormatter()", () => {
  describe("with default options", () => {
    const theories: { input: number | string; expected: string }[] = [
      { input: -987.65, expected: "-987.65" },
      { input: 0, expected: "0" },
      { input: 1, expected: "1" },
      { input: 2.3456789, expected: "2.35" },
      { input: 12345.67, expected: "12.35k" },
      { input: 890123456, expected: "890.12m" },
      { input: "not-a-number", expected: "not-a-number" },
    ];

    theoretically(
      "the value {input} is formatted using compact notation as {expected}",
      theories,
      ({ input, expected }) => {
        const result = shortValueFormatter(input as number, undefined);
        expect(result).toBe(expected);
      }
    );
  });

  describe("with currency option", () => {
    const theories: { input: number | string; expected: string }[] = [
      { input: 987.65, expected: "£988" },
      { input: -987.65, expected: "-£988" },
      { input: 0, expected: "£0" },
      { input: 1, expected: "£1" },
      { input: 2.3456789, expected: "£2" },
      { input: 0.27, expected: "£0" },
      { input: 0.9, expected: "£1" },
      { input: 55.3, expected: "£55" },
      { input: 12345.67, expected: "£12k" },
      { input: 890123456, expected: "£890m" },
      { input: 89123456, expected: "£89m" },
      { input: 8507, expected: "£8.5k" },
      { input: 1001111, expected: "£1m" },
      { input: 350220, expected: "£350k" },
      { input: "not-a-number", expected: "not-a-number" },
    ];

    theoretically(
      "the value {input} is formatted using compact notation (currency) as {expected}",
      theories,
      ({ input, expected }) => {
        const result = shortValueFormatter(input as number, "currency");
        expect(result).toBe(expected);
      }
    );
  });

  describe("with percent option", () => {
    const theories: { input: number | string; expected: string }[] = [
      { input: -987.65, expected: "-98,765%" },
      { input: 0, expected: "0%" },
      { input: 1, expected: "100%" },
      { input: 2.3456789, expected: "234.6%" },
      { input: 12345.67, expected: "1,234,567%" },
      { input: 890123456, expected: "89,012,345,600%" },
      { input: "not-a-number", expected: "not-a-number" },
    ];

    theoretically(
      "the value {input} is formatted using compact notation (percent) as {expected}",
      theories,
      ({ input, expected }) => {
        const result = shortValueFormatter(input as number, "percent");
        expect(result).toBe(expected);
      }
    );
  });
});
