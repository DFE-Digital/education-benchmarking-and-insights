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

  const values: ValueFormatterValue[] = [
    -987.65,
    0,
    1,
    2.3456789,
    12345.67,
    890123456,
    "not-a-number",
  ];

  describe("shortValueFormatter()", () => {
    describe("with default options", () => {
      const options: Partial<ValueFormatterOptions> = {};

      it("formats the values using compact notation", () => {
        const result = values.map((v) => shortValueFormatter(v, options));
        expect(result).toEqual([
          "-987.65",
          "0",
          "1",
          "2.35",
          "12.35k",
          "890.12m",
          "not-a-number",
        ]);
      });
    });

    describe("with currency option", () => {
      const options: Partial<ValueFormatterOptions> = { valueUnit: "currency" };

      it("formats the values using compact notation as GBP", () => {
        const result = values.map((v) => shortValueFormatter(v, options));
        expect(result).toEqual([
          "-£988",
          "£0",
          "£1",
          "£2.3",
          "£12k",
          "£890m",
          "not-a-number",
        ]);
      });
    });

    describe("with percent option", () => {
      const options: Partial<ValueFormatterOptions> = { valueUnit: "%" };

      it("formats the values using compact notation as percent", () => {
        const result = values.map((v) => shortValueFormatter(v, options));
        expect(result).toEqual([
          "-987.7%",
          "0%",
          "1%",
          "2.3%",
          "12,345.7%",
          "890,123,456%",
          "not-a-number",
        ]);
      });
    });
  });

  describe("statValueFormatter()", () => {
    describe("with default options", () => {
      const options: Partial<ValueFormatterOptions> = {};

      it("formats the values using number separators only", () => {
        const result = values.map((v) => statValueFormatter(v, options));
        expect(result).toEqual([
          "-988",
          "0",
          "1",
          "2",
          "12,346",
          "890,123,456",
          "not-a-number",
        ]);
      });
    });

    describe("with compact option", () => {
      const options: Partial<ValueFormatterOptions> = { compact: true };

      it("formats the values using compact notation", () => {
        const result = values.map((v) => statValueFormatter(v, options));
        expect(result).toEqual([
          "-988",
          "0",
          "1",
          "2.3",
          "12k",
          "890m",
          "not-a-number",
        ]);
      });
    });

    describe("with currency option", () => {
      const options: Partial<ValueFormatterOptions> = { valueUnit: "currency" };

      it("formats the values as GBP", () => {
        const result = values.map((v) => statValueFormatter(v, options));
        expect(result).toEqual([
          "-£988",
          "£0",
          "£1",
          "£2",
          "£12,346",
          "£890,123,456",
          "not-a-number",
        ]);
      });
    });

    describe("with percent option", () => {
      const options: Partial<ValueFormatterOptions> = { valueUnit: "%" };

      it("formats the values as percent", () => {
        const result = values.map((v) => statValueFormatter(v, options));
        expect(result).toEqual([
          "-988%",
          "0%",
          "1%",
          "2%",
          "12,346%",
          "890,123,456%",
          "not-a-number",
        ]);
      });
    });

    describe("with currency as name option", () => {
      const options: Partial<ValueFormatterOptions> = {
        valueUnit: "currency",
        currencyAsName: true,
      };

      it("formats the values as GBP in words", () => {
        const result = values.map((v) => statValueFormatter(v, options));
        expect(result).toEqual([
          "-988 british pounds",
          "0 british pounds",
          "1 british pound",
          "2 british pounds",
          "12,346 british pounds",
          "890,123,456 british pounds",
          "not-a-number",
        ]);
      });
    });
  });

  describe("fullValueFormatter()", () => {
    describe("with default options", () => {
      const options: Partial<ValueFormatterOptions> = {};

      it("formats the values to two decimal places with number separators only", () => {
        const result = values.map((v) => fullValueFormatter(v, options));
        expect(result).toEqual([
          "-987.65",
          "0",
          "1",
          "2.35",
          "12,345.67",
          "890,123,456",
          "not-a-number",
        ]);
      });
    });

    describe("fullValueFormatter()", () => {
      describe("with amount options", () => {
        const options: Partial<ValueFormatterOptions> = { valueUnit: "amount" };

        it("formats the values to two decimal places with number separators only", () => {
          const result = values.map((v) => fullValueFormatter(v, options));
          expect(result).toEqual([
            "-987.65",
            "0",
            "1",
            "2.35",
            "12,345.67",
            "890,123,456",
            "not-a-number",
          ]);
        });
      });

      describe("with currency option", () => {
        const options: Partial<ValueFormatterOptions> = {
          valueUnit: "currency",
        };

        it("formats the values to zero decimal places as GBP", () => {
          const result = values.map((v) => fullValueFormatter(v, options));
          expect(result).toEqual([
            "-£988",
            "£0",
            "£1",
            "£2",
            "£12,346",
            "£890,123,456",
            "not-a-number",
          ]);
        });
      });

      describe("with percent option", () => {
        const options: Partial<ValueFormatterOptions> = { valueUnit: "%" };

        it("formats the values to one decimal place as percent", () => {
          const result = values.map((v) => fullValueFormatter(v, options));
          expect(result).toEqual([
            "-987.7%",
            "0%",
            "1%",
            "2.3%",
            "12,345.7%",
            "890,123,456%",
            "not-a-number",
          ]);
        });
      });
    });
  });
});
