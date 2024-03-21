import { BaseAxisProps, CartesianTickItem } from "recharts/types/util/types";

export interface YTickProps extends Omit<BaseAxisProps, "scale"> {
  highlightedItemKey?: string;
  linkToSchool?: boolean;
  onClick?: (urn: string) => void;
  payload: CartesianTickItem;
  schoolUrnResolver?: (name: string) => string | undefined;
}
