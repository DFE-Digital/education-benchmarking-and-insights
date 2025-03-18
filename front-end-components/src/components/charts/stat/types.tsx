import { ReactNode } from "react";
import { ChartDataSeries, ChartProps, ChartSeriesValue } from "src/components";

export interface StatProps<TData extends ChartDataSeries>
  extends Pick<
    ChartProps<TData>,
    "chartTitle" | "valueFormatter" | "valueUnit"
  > {
  className?: string;
  compactValue?: boolean;
  label: string;
  small?: boolean;
  suffix?: string | ReactNode;
  value: ChartSeriesValue;
  valueSuffix?: string;
}
