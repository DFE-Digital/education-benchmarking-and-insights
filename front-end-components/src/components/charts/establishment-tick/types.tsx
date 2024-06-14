import { BaseAxisProps, CartesianTickItem } from "recharts/types/util/types";
import { TickProps } from "../types";

export interface EstablishmentTickProps
  extends Omit<BaseAxisProps, "scale">,
    Omit<TickProps, "href" | "type" | "onClick" | "ref" | "textAnchor"> {
  highlightedItemKey?: string;
  linkToEstablishment?: boolean;
  href: (key: string) => string;
  payload: CartesianTickItem;
  establishmentKeyResolver?: (name: string) => string | undefined;
  verticalAnchor: unknown;
}
