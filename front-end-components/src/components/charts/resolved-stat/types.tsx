import { ChartDataSeries, ChartProps } from "src/components";
import { StatProps } from "../stat";

export interface ResolvedStatProps<TData extends ChartDataSeries>
  extends Omit<StatProps<TData>, "valueSuffix" | "label" | "value">,
    Pick<
      ChartProps<TData>,
      "data" | "seriesFormatter" | "seriesLabel" | "seriesLabelField"
    > {
  displayIndex: number;
  valueField: keyof TData;
}
