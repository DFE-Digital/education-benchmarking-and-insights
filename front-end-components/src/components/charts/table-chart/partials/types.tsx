import { LaChartData, SchoolChartData, TrustChartData } from "../types";

export type TableCellEstablishmentNameProps<
  TData extends SchoolChartData | TrustChartData | LaChartData,
> = Omit<SelectedAnchorProps, "identifier" | "label"> & {
  row: TData;
};

export type SelectedAnchorProps = Omit<AnchorProps, "href"> & {
  identifier: string;
  localAuthority?: boolean;
  trust?: boolean;
};

export type AnchorProps = {
  href: string;
  label: string;
  preventFocus?: boolean;
};
