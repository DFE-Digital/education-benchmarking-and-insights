import { BaseAxisProps } from "recharts/types/util/types";
import { ChartDataSeries, ChartProps, ChartSeriesValue } from "src/components";

export interface HorizontalBarChartProps<TData extends ChartDataSeries>
  extends ChartProps<TData>,
    Pick<BaseAxisProps, "tick"> {
  barCategoryGap?: string | number;
  highlightedItemKeys?: ChartSeriesValue[];
  legend?: boolean;
  tickWidth?: number;
}
