import { describe, expect, test } from "@jest/globals";
import { validatePayload } from "../../../src/functions/lineChart/validator";
import { LineChartPayload } from "../../../src/functions/lineChart";

describe("line chart validator", () => {
  describe("validates payload", () => {
    describe("with valid single payload", () => {
      test("returns no errors", () => {
        const expected: string[] = [];
        const payload: LineChartPayload = {
          data: [
            {
              key: "2024/25",
              value: 10,
            },
          ],
          keyField: "key",
          valueField: "value",
        };

        expect(validatePayload(payload)).toStrictEqual(expected);
      });
    });

    describe("with valid array payload", () => {
      test("returns no errors", () => {
        const expected: string[] = [];
        const payload: LineChartPayload = [
          {
            id: "chart-1",
            data: [
              {
                key: "2024/25",
                value: 10,
              },
            ],
            keyField: "key",
            valueField: "value",
          },
        ];

        expect(validatePayload(payload)).toStrictEqual(expected);
      });
    });

    describe("with missing data in single payload", () => {
      test("returns error", () => {
        const expected: string[] = ["Missing chart data"];
        const payload: LineChartPayload = {
          data: [],
          keyField: "key",
          valueField: "value",
        };

        expect(validatePayload(payload)).toStrictEqual(expected);
      });
    });

    describe("with missing definitions in array payload", () => {
      test("returns error", () => {
        const expected: string[] = ["Missing chart definitions"];
        const payload: LineChartPayload = [];

        expect(validatePayload(payload)).toStrictEqual(expected);
      });
    });

    describe("with missing ids in array payload", () => {
      test("returns error", () => {
        const expected: string[] = ["Missing id for chart at index 0"];
        const payload: LineChartPayload = [
          {
            data: [
              {
                key: "2024/25",
                value: 10,
              },
            ],
            keyField: "key",
            valueField: "value",
          },
        ];

        expect(validatePayload(payload)).toStrictEqual(expected);
      });
    });

    describe("with missing data in array payload", () => {
      test("returns error", () => {
        const expected: string[] = ["Missing chart data for id"];
        const payload: LineChartPayload = [
          {
            id: "id",
            data: [],
            keyField: "key",
            valueField: "value",
          },
        ];

        expect(validatePayload(payload)).toStrictEqual(expected);
      });
    });

    describe("with undefined payload", () => {
      test("returns error", () => {
        const expected: string[] = ["Invalid payload"];
        expect(validatePayload(undefined)).toStrictEqual(expected);
      });
    });

    describe("with unsupported payload", () => {
      test("returns error", () => {
        const expected: string[] = ["Missing chart data"];
        expect(validatePayload({} as LineChartPayload)).toStrictEqual(expected);
      });
    });
  });
});
