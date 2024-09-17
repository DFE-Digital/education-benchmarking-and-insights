import { TooltipProps } from "recharts";
import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { SpecialItemFlag } from "../types";

export interface SchoolTooltipProps<
  TValue extends ValueType,
  TName extends NameType,
> extends TooltipProps<TValue, TName> {
  specialItemFlags?: (key: string) => SpecialItemFlag[];
}
