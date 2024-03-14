import { ChartDataPoint, ChartSortMode } from ".";
import { chartComparer, chartSeriesComparer } from "./utils";

describe("Chart utils", () => {
  describe("chartComparer()", () => {
    const data: ChartDataPoint[] = [
      { school: "School A", urn: "1", value: 20 },
      { school: "School B", urn: "2", value: 30 },
      { school: "School C", urn: "3", value: 40 },
      { school: "School D", urn: "4", value: 10 },
      { school: "School E", urn: "5", value: 25 },
    ];

    describe("with default sort", () => {
      const sort: ChartSortMode = {
        dataPoint: "value",
        direction: "desc",
      };

      it("sorts the data points", () => {
        const result = data.sort((a, b) => chartComparer(a, b, sort));
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
      const sort: ChartSortMode = {
        dataPoint: "value",
        direction: "asc",
      };

      it("sorts the data points", () => {
        const result = data.sort((a, b) => chartComparer(a, b, sort));
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
      const sort: ChartSortMode = {
        dataPoint: "school",
        direction: "asc",
      };

      it("sorts the data points", () => {
        const result = data.sort((a, b) => chartComparer(a, b, sort));
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

  describe("chartSeriesComparer()", () => {
    const data = [
      { school: "School A", urn: "1", value: 20 },
      { school: "School B", urn: "2", value: 30 },
      { school: "School C", urn: "3", value: 40 },
      { school: "School D", urn: "4", value: 10 },
      { school: "School E", urn: "5", value: 25 },
    ];

    describe("with default sort", () => {
      const sort: ChartSortMode = {
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
      const sort: ChartSortMode = {
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
      const sort: ChartSortMode = {
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
});
