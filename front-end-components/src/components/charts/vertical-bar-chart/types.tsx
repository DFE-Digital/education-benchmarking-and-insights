import { ChartDataSeries, ChartProps, ChartSeriesValue } from "src/components";

export interface VerticalBarChartProps<TData extends ChartDataSeries>
  extends ChartProps<TData> {
  highlightedItemKeys?: ChartSeriesValue[];
  ref?: VerticalBarChartHandler;
}

export type VerticalBarChartHandler = {
  download: () => void;
};
