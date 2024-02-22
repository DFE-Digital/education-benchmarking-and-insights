import { Ref } from "react";
import { ChartDataSeries, ChartProps, ChartSeriesValue } from "src/components";

export interface VerticalBarChartProps<TData extends ChartDataSeries>
  extends ChartProps<TData> {
  highlightedItemKeys?: ChartSeriesValue[];
  legend?: boolean;
  ref?: Ref<VerticalBarChartHandler>;
}

export type VerticalBarChartHandler = {
  download: () => void;
};
