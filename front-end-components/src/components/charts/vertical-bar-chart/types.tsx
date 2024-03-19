import { ChartDataSeries, ChartProps, ChartSeriesValue } from "src/components";

export interface VerticalBarChartProps<TData extends ChartDataSeries>
  extends ChartProps<TData> {
  barCategoryGap?: string | number;
  includeNegativeValues?: boolean;
  highlightedItemKeys?: ChartSeriesValue[];
  legend?: boolean;
}
