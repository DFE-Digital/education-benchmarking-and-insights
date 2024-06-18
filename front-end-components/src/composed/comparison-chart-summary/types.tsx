import {
  ChartDataSeries,
  ChartProps,
  ChartSortDirection,
} from "src/components";

export type ComparisonChartSummaryComposedProps<TData extends ChartDataSeries> =
  Pick<
    ChartProps<TData>,
    "chartName" | "data" | "keyField" | "valueUnit" | "suffix"
  > & {
    highlightedItemKey?: string;
    sortDirection: ChartSortDirection;
    chartStats: ComparisonChartStats;
    valueField: keyof TData;
    hasIncompleteData: boolean;
  };

export type ComparisonChartStats = {
  average: number;
  difference: number;
  percentDifference: number;
};
