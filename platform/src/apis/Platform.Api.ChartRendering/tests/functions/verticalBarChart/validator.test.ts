import { describe, expect, test } from "@jest/globals";
import { validatePayload } from "../../../src/functions/verticalBarChart/validator";
import { VerticalBarChartPayload } from "../../../src/functions/verticalBarChart";

describe("vertical bar chart", () => {
  describe("validates payload", () => {
    describe("with valid single payload", () => {
      test("returns no errors", () => {
        const expected: string[] = [];
        const payload: VerticalBarChartPayload = {
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
        const payload: VerticalBarChartPayload = [
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
        const payload: VerticalBarChartPayload = {
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
        const payload: VerticalBarChartPayload = [];

        expect(validatePayload(payload)).toStrictEqual(expected);
      });
    });

    describe("with missing ids in array payload", () => {
      test("returns error", () => {
        const expected: string[] = ["Missing id for chart at index 0"];
        const payload: VerticalBarChartPayload = [
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
        const payload: VerticalBarChartPayload = [
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
  });
});
