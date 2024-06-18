import { LabelProps } from "recharts";
import { BaseAxisProps } from "recharts/types/util/types";
import {
  ChartDataSeries,
  ChartProps,
  ValueFormatterProps,
} from "src/components";

export interface HorizontalBarChartProps<TData extends ChartDataSeries>
  extends ChartProps<TData>,
    Pick<BaseAxisProps, "tick"> {
  tickWidth?: number;
  labelListSeriesName?: keyof TData;
}

export interface LabelListContentProps
  extends Omit<LabelProps, "formatter">,
    ValueFormatterProps {}
