import { BaseAxisProps, CartesianTickItem } from "recharts/types/util/types";
import { TickProps } from "../types";
import { FunctionComponent } from "react";
import { TooltipProps } from "recharts/types/component/Tooltip";
import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";

export interface EstablishmentTickProps
  extends Omit<BaseAxisProps, "scale">,
    Omit<TickProps, "href" | "type" | "onClick" | "ref" | "textAnchor"> {
  highlightedItemKey?: string;
  linkToEstablishment?: boolean;
  href: (key: string) => string;
  payload: CartesianTickItem;
  establishmentKeyResolver?: (name: string) => string | undefined;
  verticalAnchor: unknown;
  tooltip?: FunctionComponent<TooltipProps<ValueType, NameType>>;
}
