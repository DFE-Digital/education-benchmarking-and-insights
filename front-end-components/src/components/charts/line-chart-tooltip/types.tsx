import { TooltipProps } from "recharts";
import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { ChartDataSeries } from "src/components";
import { LineChartProps } from "src/components/charts/line-chart";

export interface LineChartTooltipProps<
  TData extends ChartDataSeries,
  TValue extends ValueType,
  TName extends NameType,
> extends TooltipProps<TValue, TName>,
    Pick<LineChartProps<TData>, "valueFormatter" | "valueUnit"> {}
