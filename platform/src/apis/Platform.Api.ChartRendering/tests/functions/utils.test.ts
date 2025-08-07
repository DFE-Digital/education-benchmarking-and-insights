import { describe, expect, it } from "@jest/globals";
import {
  normaliseChartData,
  getChartValueFormat,
} from "../../src/functions/utils";
import { ChartValueType } from "../../src/functions/index";

describe("normaliseChartData", () => {
  const sampleData = [
    { category: "A", value: 50 },
    { category: "B", value: 100 },
  ];

  it("should divide values by 100 for 'percent' type", () => {
    const result = normaliseChartData(sampleData, "value", "percent");
    expect(result).toEqual([
      { category: "A", value: 0.5 },
      { category: "B", value: 1 },
    ]);
  });

  it("should return original data for 'currency' type", () => {
    const result = normaliseChartData(sampleData, "value", "currency");
    expect(result).toEqual(sampleData);
  });

  it("should throw for unsupported ChartValueType", () => {
    expect(() =>
      normaliseChartData(sampleData, "value", "invalid" as ChartValueType),
    ).toThrow("Argument out of range: unsupported ChartValueType 'invalid'");
  });
});

describe("getChartValueFormat", () => {
  it("should return '.1%' for 'percent'", () => {
    expect(getChartValueFormat("percent")).toBe(".1%");
  });

  it("should return '$,~s' for 'currency'", () => {
    expect(getChartValueFormat("currency")).toBe("$,~s");
  });

  it("should throw for unsupported ChartValueType", () => {
    expect(() => getChartValueFormat("invalid" as ChartValueType)).toThrow(
      "Argument out of range: unsupported ChartValueType 'invalid'",
    );
  });
});
