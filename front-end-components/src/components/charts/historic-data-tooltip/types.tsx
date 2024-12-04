import { TooltipProps } from "recharts";
import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { ChartDataSeries } from "../types";
import { LineChartProps } from "../line-chart";

export interface HistoricTooltipProps<
  TData extends ChartDataSeries,
  TValue extends ValueType,
  TName extends NameType,
> extends TooltipProps<TValue, TName>,
    Pick<LineChartProps<TData>, "valueFormatter" | "valueUnit"> {
  dimension: string;
}
