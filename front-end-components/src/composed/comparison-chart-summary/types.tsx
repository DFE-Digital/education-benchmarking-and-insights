import {
  ChartDataSeries,
  ChartProps,
  ChartSortDirection,
} from "src/components";

export type ComparisonChartSummaryComposedProps<TData extends ChartDataSeries> =
  Pick<ChartProps<TData>, "chartName" | "data" | "keyField" | "valueUnit"> & {
    highlightedItemKey?: string;
    sortDirection: ChartSortDirection;
    valueField: keyof TData;
  };
