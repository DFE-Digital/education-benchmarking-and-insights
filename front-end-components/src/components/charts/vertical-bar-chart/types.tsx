import { ChartDataSeries, ChartProps, ChartSeriesValue } from "src/components";

export interface VerticalBarChartProps<TData extends ChartDataSeries>
  extends ChartProps<TData> {
  barCategoryGap?: string | number;
  highlightedItemKeys?: ChartSeriesValue[];
  legend?: boolean;
}
