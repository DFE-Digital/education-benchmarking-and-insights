import { BaseAxisProps, CartesianTickItem } from "recharts/types/util/types";
import { TickProps } from "../types";

export interface SchoolTickProps
  extends Omit<BaseAxisProps, "scale">,
    Omit<TickProps, "type" | "onClick" | "ref" | "textAnchor"> {
  highlightedItemKey?: string;
  linkToSchool?: boolean;
  onClick?: (urn: string) => void;
  payload: CartesianTickItem;
  schoolUrnResolver?: (name: string) => string | undefined;
}
