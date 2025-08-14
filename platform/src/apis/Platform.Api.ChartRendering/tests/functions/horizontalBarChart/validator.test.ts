import { describe, expect, test } from "@jest/globals";
import { validatePayload } from "../../../src/functions/horizontalBarChart/validator";
import { HorizontalBarChartPayload } from "../../../src/functions/horizontalBarChart";

describe("horizontal bar chart", () => {
  describe("validates payload", () => {
    describe("with valid single payload", () => {
      test("returns no errors", () => {
        const expected: string[] = [];
        const payload: HorizontalBarChartPayload = {
          data: [
            {
              key: "",
              value: 0,
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
        const payload: HorizontalBarChartPayload = [
          {
            id: "id",
            data: [
              {
                key: "",
                value: 0,
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
        const payload: HorizontalBarChartPayload = {
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
        const payload: HorizontalBarChartPayload = [];

        expect(validatePayload(payload)).toStrictEqual(expected);
      });
    });

    describe("with missing ids in array payload", () => {
      test("returns error", () => {
        const expected: string[] = ["Missing id for chart at index 0"];
        const payload: HorizontalBarChartPayload = [
          {
            data: [
              {
                key: "",
                value: 0,
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
        const payload: HorizontalBarChartPayload = [
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
        expect(validatePayload({} as HorizontalBarChartPayload)).toStrictEqual(
          expected,
        );
      });
    });
  });
});
