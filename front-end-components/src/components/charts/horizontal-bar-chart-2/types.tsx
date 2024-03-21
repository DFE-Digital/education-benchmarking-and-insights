import { LabelProps } from "recharts";
import { BaseAxisProps } from "recharts/types/util/types";
import {
  ChartDataSeries,
  ChartProps,
  ChartSeriesValue,
  ValueFormatterProps,
} from "src/components";

export interface HorizontalBarChartProps<TData extends ChartDataSeries>
  extends ChartProps<TData>,
    Pick<BaseAxisProps, "tick"> {
  barCategoryGap?: string | number;
  highlightedItemKeys?: ChartSeriesValue[];
  legend?: boolean;
  tickWidth?: number;
}

export interface LabelListContentProps
  extends Omit<LabelProps, "formatter">,
    ValueFormatterProps {}
