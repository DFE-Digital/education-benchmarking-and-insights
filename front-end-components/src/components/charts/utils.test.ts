import theoretically from "jest-theories";
import {
  ChartDataSeriesSortMode,
  ValueFormatterOptions,
  ValueFormatterValue,
} from ".";
import {
  chartSeriesComparer,
  shortValueFormatter,
  statValueFormatter,
  fullValueFormatter,
  payBandFormatter,
} from "./utils";

describe("Chart utils", () => {
  describe("chartSeriesComparer()", () => {
    const data = [
      { school: "School A", urn: "1", value: 20 },
      { school: "School B", urn: "2", value: 30 },
      { school: "School C", urn: "3", value: 40 },
      { school: "School D", urn: "4", value: 10 },
      { school: "School E", urn: "5", value: 25 },
    ];

    describe("with default sort", () => {
      const sort: ChartDataSeriesSortMode<{ value: number }> = {
        dataPoint: "value",
        direction: "desc",
      };

      it("sorts the data points", () => {
        const result = data.sort((a, b) => chartSeriesComparer(a, b, sort));
        expect(result).toEqual([
          { school: "School C", urn: "3", value: 40 },
          { school: "School B", urn: "2", value: 30 },
          { school: "School E", urn: "5", value: 25 },
          { school: "School A", urn: "1", value: 20 },
          { school: "School D", urn: "4", value: 10 },
        ]);
      });
    });

    describe("by value ascending", () => {
      const sort: ChartDataSeriesSortMode<{ value: number }> = {
        dataPoint: "value",
        direction: "asc",
      };

      it("sorts the data points", () => {
        const result = data.sort((a, b) => chartSeriesComparer(a, b, sort));
        expect(result).toEqual([
          { school: "School D", urn: "4", value: 10 },
          { school: "School A", urn: "1", value: 20 },
          { school: "School E", urn: "5", value: 25 },
          { school: "School B", urn: "2", value: 30 },
          { school: "School C", urn: "3", value: 40 },
        ]);
      });
    });

    describe("by school ascending", () => {
      const sort: ChartDataSeriesSortMode<{ school: string }> = {
        dataPoint: "school",
        direction: "asc",
      };

      it("sorts the data points", () => {
        const result = data.sort((a, b) => chartSeriesComparer(a, b, sort));
        expect(result).toEqual([
          { school: "School A", urn: "1", value: 20 },
          { school: "School B", urn: "2", value: 30 },
          { school: "School C", urn: "3", value: 40 },
          { school: "School D", urn: "4", value: 10 },
          { school: "School E", urn: "5", value: 25 },
        ]);
      });
    });
  });

  describe("shortValueFormatter()", () => {
    describe("with default options", () => {
      const theories: { input: ValueFormatterValue; expected: string }[] = [
        { input: -987.65, expected: "-987.65" },
        { input: 0, expected: "0" },
        { input: 1, expected: "1" },
        { input: 2.3456789, expected: "2.35" },
        { input: 12345.67, expected: "12.35k" },
        { input: 890123456, expected: "890.12m" },
        { input: "not-a-number", expected: "not-a-number" },
      ];

      const options: Partial<ValueFormatterOptions> = {};

      theoretically(
        "the value {input} is formatted using compact notation as {expected}",
        theories,
        ({ input, expected }) => {
          const result = shortValueFormatter(input, options);
          expect(result).toBe(expected);
        }
      );
    });

    describe("with currency option", () => {
      const theories: { input: ValueFormatterValue; expected: string }[] = [
        { input: 987.65, expected: "£987.65" },
        { input: -987.65, expected: "-£987.65" },
        { input: 0, expected: "£0" },
        { input: 1, expected: "£1" },
        { input: 2.3456789, expected: "£2.35" },
        { input: 0.27, expected: "£0.27" },
        { input: 0.9, expected: "£0.90" },
        { input: 55.3, expected: "£55.30" },
        { input: 12345.67, expected: "£12k" },
        { input: 890123456, expected: "£890m" },
        { input: "not-a-number", expected: "not-a-number" },
      ];

      const options: Partial<ValueFormatterOptions> = { valueUnit: "currency" };

      theoretically(
        "the value {input} is formatted using compact notation (currency) as {expected}",
        theories,
        ({ input, expected }) => {
          const result = shortValueFormatter(input, options);
          expect(result).toBe(expected);
        }
      );
    });

    describe("with percent option", () => {
      const theories: { input: ValueFormatterValue; expected: string }[] = [
        { input: -987.65, expected: "-987.7%" },
        { input: 0, expected: "0%" },
        { input: 1, expected: "1%" },
        { input: 2.3456789, expected: "2.3%" },
        { input: 12345.67, expected: "12,345.7%" },
        { input: 890123456, expected: "890,123,456%" },
        { input: "not-a-number", expected: "not-a-number" },
      ];

      const options: Partial<ValueFormatterOptions> = { valueUnit: "%" };

      theoretically(
        "the value {input} is formatted using compact notation (percent) as {expected}",
        theories,
        ({ input, expected }) => {
          const result = shortValueFormatter(input, options);
          expect(result).toBe(expected);
        }
      );
    });
  });

  describe("statValueFormatter()", () => {
    describe("with default options", () => {
      const theories: { input: ValueFormatterValue; expected: string }[] = [
        { input: -987.65, expected: "-988" },
        { input: 0, expected: "0" },
        { input: 1, expected: "1" },
        { input: 2.3456789, expected: "2" },
        { input: 12345.67, expected: "12,346" },
        { input: 890123456, expected: "890,123,456" },
        { input: "not-a-number", expected: "not-a-number" },
      ];

      const options: Partial<ValueFormatterOptions> = {};

      theoretically(
        "the value {input} is formatted using compact notation as {expected}",
        theories,
        ({ input, expected }) => {
          const result = statValueFormatter(input, options);
          expect(result).toBe(expected);
        }
      );
    });

    describe("with compact option", () => {
      const theories: { input: ValueFormatterValue; expected: string }[] = [
        { input: -987.65, expected: "-988" },
        { input: 0, expected: "0" },
        { input: 1, expected: "1" },
        { input: 2.3456789, expected: "2.3" },
        { input: 12345.67, expected: "12k" },
        { input: 890123456, expected: "890m" },
        { input: "not-a-number", expected: "not-a-number" },
      ];

      const options: Partial<ValueFormatterOptions> = { compact: true };

      theoretically(
        "the value {input} is formatted using compact notation as {expected}",
        theories,
        ({ input, expected }) => {
          const result = statValueFormatter(input, options);
          expect(result).toBe(expected);
        }
      );
    });

    describe("with compact currency option", () => {
      const theories: { input: ValueFormatterValue; expected: string }[] = [
        { input: -987.65, expected: "-£987.65" },
        { input: 0, expected: "£0.00" },
        { input: 1, expected: "£1.00" },
        { input: 2.3456789, expected: "£2.35" },
        { input: 12345.67, expected: "£12k" },
        { input: 890123456, expected: "£890m" },
        { input: "not-a-number", expected: "not-a-number" },
      ];

      const options: Partial<ValueFormatterOptions> = {
        compact: true,
        valueUnit: "currency",
      };

      theoretically(
        "the value {input} is formatted using compact notation as {expected}",
        theories,
        ({ input, expected }) => {
          const result = statValueFormatter(input, options);
          expect(result).toBe(expected);
        }
      );
    });

    describe("with currency option", () => {
      const theories: { input: ValueFormatterValue; expected: string }[] = [
        { input: -987.65, expected: "-£988" },
        { input: 0, expected: "£0" },
        { input: 1, expected: "£1" },
        { input: 2.3456789, expected: "£2" },
        { input: 12345.67, expected: "£12,346" },
        { input: 890123456, expected: "£890,123,456" },
        { input: "not-a-number", expected: "not-a-number" },
      ];

      const options: Partial<ValueFormatterOptions> = { valueUnit: "currency" };

      theoretically(
        "the value {input} is formatted as currency as {expected}",
        theories,
        ({ input, expected }) => {
          const result = statValueFormatter(input, options);
          expect(result).toBe(expected);
        }
      );
    });

    describe("with percent option", () => {
      const theories: { input: ValueFormatterValue; expected: string }[] = [
        { input: -987.65, expected: "-987.7%" },
        { input: 0, expected: "0%" },
        { input: 1, expected: "1%" },
        { input: 2.3456789, expected: "2.3%" },
        { input: 12345.67, expected: "12,345.7%" },
        { input: 890123456, expected: "890,123,456%" },
        { input: "not-a-number", expected: "not-a-number" },
        { input: 0.28, expected: "0.3%" },
      ];

      const options: Partial<ValueFormatterOptions> = { valueUnit: "%" };

      theoretically(
        "the value {input} is formatted as percent as {expected}",
        theories,
        ({ input, expected }) => {
          const result = statValueFormatter(input, options);
          expect(result).toBe(expected);
        }
      );
    });

    describe("with currency as name option", () => {
      const theories: { input: ValueFormatterValue; expected: string }[] = [
        { input: -987.65, expected: "-988 british pounds" },
        { input: 0, expected: "0 british pounds" },
        { input: 1, expected: "1 british pound" },
        { input: 2.3456789, expected: "2 british pounds" },
        { input: 12345.67, expected: "12,346 british pounds" },
        { input: 890123456, expected: "890,123,456 british pounds" },
        { input: "not-a-number", expected: "not-a-number" },
      ];

      const options: Partial<ValueFormatterOptions> = {
        valueUnit: "currency",
        currencyAsName: true,
      };

      theoretically(
        "the value {input} is formatted as currency in words as {expected}",
        theories,
        ({ input, expected }) => {
          const result = statValueFormatter(input, options);
          expect(result).toBe(expected);
        }
      );
    });
  });

  describe("fullValueFormatter()", () => {
    describe("with default options", () => {
      const theories: { input: ValueFormatterValue; expected: string }[] = [
        { input: -987.65, expected: "-987.65" },
        { input: 0, expected: "0" },
        { input: 1, expected: "1" },
        { input: 2.3456789, expected: "2.35" },
        { input: 12345.67, expected: "12,345.67" },
        { input: 890123456, expected: "890,123,456" },
        { input: "not-a-number", expected: "not-a-number" },
      ];

      const options: Partial<ValueFormatterOptions> = {};

      theoretically(
        "the value {input} is formatted to two decimal places with number separators only as {expected}",
        theories,
        ({ input, expected }) => {
          const result = fullValueFormatter(input, options);
          expect(result).toBe(expected);
        }
      );
    });

    describe("fullValueFormatter()", () => {
      describe("with amount options", () => {
        const theories: { input: ValueFormatterValue; expected: string }[] = [
          { input: -987.65, expected: "-987.65" },
          { input: 0, expected: "0" },
          { input: 1, expected: "1" },
          { input: 2.3456789, expected: "2.35" },
          { input: 12345.67, expected: "12,345.67" },
          { input: 890123456, expected: "890,123,456" },
          { input: "not-a-number", expected: "not-a-number" },
        ];

        const options: Partial<ValueFormatterOptions> = { valueUnit: "amount" };

        theoretically(
          "the value {input} is formatted to two decimal places with number separators only as {expected}",
          theories,
          ({ input, expected }) => {
            const result = fullValueFormatter(input, options);
            expect(result).toBe(expected);
          }
        );
      });

      describe("with currency option", () => {
        const theories: { input: ValueFormatterValue; expected: string }[] = [
          { input: -987.65, expected: "-£987.65" },
          { input: 0, expected: "£0.00" },
          { input: 1, expected: "£1.00" },
          { input: 2.3456789, expected: "£2.35" },
          { input: 0.27, expected: "£0.27" },
          { input: 0.9, expected: "£0.90" },
          { input: 55.3, expected: "£55.30" },
          { input: 12345.67, expected: "£12,346" },
          { input: 890123456, expected: "£890,123,456" },
          { input: "not-a-number", expected: "not-a-number" },
        ];

        const options: Partial<ValueFormatterOptions> = {
          valueUnit: "currency",
        };

        theoretically(
          "the value {input} is formatted to zero decimal places as currency as {expected}",
          theories,
          ({ input, expected }) => {
            const result = fullValueFormatter(input, options);
            expect(result).toBe(expected);
          }
        );
      });

      describe("with percent option", () => {
        const theories: { input: ValueFormatterValue; expected: string }[] = [
          { input: -987.65, expected: "-987.7%" },
          { input: 0, expected: "0%" },
          { input: 1, expected: "1%" },
          { input: 2.3456789, expected: "2.3%" },
          { input: 12345.67, expected: "12,345.7%" },
          { input: 890123456, expected: "890,123,456%" },
          { input: "not-a-number", expected: "not-a-number" },
        ];

        const options: Partial<ValueFormatterOptions> = {
          valueUnit: "%",
        };

        theoretically(
          "the value {input} is formatted to zero decimal places as percent as {expected}",
          theories,
          ({ input, expected }) => {
            const result = fullValueFormatter(input, options);
            expect(result).toBe(expected);
          }
        );
      });
    });
  });

  describe("payBandFormatter()", () => {
    describe("when given a non-number", () => {
      it("returns message", () => {
        expect(payBandFormatter(undefined)).toBe("No data available");
        expect(payBandFormatter("string")).toBe("No data available");
      });
    });

    describe("when given a number", () => {
      describe("and value is less than or equal to 10", () => {
        it("returns '£0-£10k'", () => {
          expect(payBandFormatter(0)).toBe("£0-£10k");
          expect(payBandFormatter(10)).toBe("£0-£10k");
        });
      });

      describe("and value is greater than 380", () => {
        it("returns '£380k+'", () => {
          expect(payBandFormatter(390)).toBe("£380k+");
          expect(payBandFormatter(400)).toBe("£380k+");
        });
      });

      describe("and value is between 11 and 380", () => {
        it("returns a range in the format '£{x-10}k-{x}k'", () => {
          expect(payBandFormatter(11)).toBe("£1k-£11k");
          expect(payBandFormatter(50)).toBe("£40k-£50k");
          expect(payBandFormatter(380)).toBe("£370k-£380k");
        });
      });
    });
  });
});
