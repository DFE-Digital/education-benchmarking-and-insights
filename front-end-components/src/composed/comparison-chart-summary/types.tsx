import {
  ChartDataAverage,
  ChartDataSeries,
  ChartProps,
  ChartSortDirection,
} from "src/components";

export type ComparisonChartSummaryComposedProps<TData extends ChartDataSeries> =
  Pick<
    ChartProps<TData>,
    "chartName" | "data" | "keyField" | "valueUnit" | "suffix"
  > & {
    averageType?: ChartDataAverage;
    highlightedItemKey?: string;
    sortDirection: ChartSortDirection;
    valueField: keyof TData;
  };
