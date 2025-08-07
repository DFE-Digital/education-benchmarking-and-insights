import { describe, expect, it } from "@jest/globals";
import { normaliseData, getValueFormat } from "../../src/functions/utils";
import { ValueType } from "../../src/functions/index";

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
