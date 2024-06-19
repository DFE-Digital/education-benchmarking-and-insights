import { ChartSeriesValueUnit } from "src/components/charts/types";

export type TableChartProps<TData extends SchoolChartData | TrustChartData> = {
  tableHeadings: string[];
  data?: TData[];
  preventFocus?: boolean;
  valueUnit?: ChartSeriesValueUnit | undefined;
  trust?: boolean;
};

export type SchoolChartData = {
  urn: string;
  totalPupils: bigint;
  schoolName: string;
  schoolType: string;
  laName: string;
  value?: number;
};

export type TrustChartData = {
  companyNumber: string;
  trustName: string;
  schoolValue?: number;
  centralValue?: number;
  totalValue?: number;
  type?: "expenditure" | "balance";
};
