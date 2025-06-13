import { TooltipProps } from "recharts";
import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { ChartSeriesValueUnit } from "../types";

export interface PayBandDataTooltipProps<
  TValue extends ValueType,
  TName extends NameType,
> extends TooltipProps<TValue, TName> {
  valueFormatter: (value: ValueType) => string;
  valueUnit?: ChartSeriesValueUnit;
}
