import { ChartSeriesValueUnit } from "src/components/charts/types";

// todo: refactor to use discriminated union over TData/type
export type TableChartProps<
  TData extends SchoolChartData | TrustChartData | LaChartData,
> = {
  tableHeadings: string[];
  data?: TData[];
  preventFocus?: boolean;
  valueUnit?: ChartSeriesValueUnit | undefined;
  trust?: boolean;
  localAuthority?: boolean;
};

export type SchoolChartData = {
  urn: string;
  totalPupils: bigint;
  schoolName: string;
  schoolType: string;
  laName: string;
  value?: number;
  periodCoveredByReturn?: number;
};

export type TrustChartData = {
  companyNumber: string;
  trustName: string;
  schoolValue?: number;
  centralValue?: number;
  totalValue?: number;
  type?: "expenditure" | "balance";
};

export type LaChartData = {
  laCode: string;
  laName: string;
  value?: number;
};
