import { ReactNode } from "react";
import { ChartDataSeries, ChartProps, ChartSeriesValue } from "src/components";

export interface StatProps<TData extends ChartDataSeries>
  extends Pick<ChartProps<TData>, "chartName" | "valueUnit"> {
  className?: string;
  compactValue?: boolean;
  label: string;
  suffix?: string | ReactNode;
  value: ChartSeriesValue;
  valueSuffix?: string;
}
