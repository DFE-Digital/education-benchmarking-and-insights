import { ChartDataSeries, ChartProps } from "src/components";

export interface StatProps<TData extends ChartDataSeries>
  extends Pick<
    ChartProps<TData>,
    "chartName" | "data" | "seriesLabelField" | "valueUnit"
  > {
  className?: string;
  displayIndex: number;
  valueField: keyof TData;
}
