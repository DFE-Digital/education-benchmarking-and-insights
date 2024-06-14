import { BaseAxisProps, CartesianTickItem } from "recharts/types/util/types";
import { TickProps } from "../types";

export interface TrustTickProps
  extends Omit<BaseAxisProps, "scale">,
    Omit<TickProps, "type" | "onClick" | "ref" | "textAnchor"> {
  highlightedItemKey?: string;
  linkToTrust?: boolean;
  onClick?: (companyNumber: string) => void;
  payload: CartesianTickItem;
  trustCompanyNumberResolver?: (name: string) => string | undefined;
}
