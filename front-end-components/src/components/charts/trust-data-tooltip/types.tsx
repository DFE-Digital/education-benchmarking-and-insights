import { TooltipProps } from "recharts";
import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { ChartSeriesValueUnit } from "../types";

export interface TrustDataTooltipProps<
  TValue extends ValueType,
  TName extends NameType,
> extends TooltipProps<TValue, TName> {
  valueUnit?: ChartSeriesValueUnit;
}
