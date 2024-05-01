import { TooltipProps } from "recharts";
import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";

export interface SchoolTooltipProps<
  TValue extends ValueType,
  TName extends NameType,
> extends TooltipProps<TValue, TName> {}
