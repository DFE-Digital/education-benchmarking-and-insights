import { BaseAxisProps, CartesianTickItem } from "recharts/types/util/types";
import { SpecialItemFlag, TickProps } from "../types";
import { FunctionComponent } from "react";
import { TooltipProps } from "recharts/types/component/Tooltip";
import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";

export interface EstablishmentTickProps
  extends Omit<BaseAxisProps, "scale">,
    Omit<TickProps, "href" | "type" | "onClick" | "ref" | "textAnchor"> {
  establishmentKeyResolver?: (name: string) => string | undefined;
  highlightedItemKey?: string;
  href: (key: string) => string;
  linkToEstablishment?: boolean;
  onFocused?: (key: string, focused: boolean) => void;
  payload: CartesianTickItem;
  specialItemFlags?: (key: string) => SpecialItemFlag[];
  tooltip?: FunctionComponent<TooltipProps<ValueType, NameType>>;
  verticalAnchor: unknown;
}
