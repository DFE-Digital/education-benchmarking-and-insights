import { ChartSeriesValueUnit } from "src/components/charts/types";

export type TableChartProps<TData extends SchoolChartData> = {
  tableHeadings: string[];
  data?: TData[];
  preventFocus?: boolean;
  valueUnit?: ChartSeriesValueUnit | undefined;
};

export type SchoolChartData = {
  urn: string;
  totalPupils: bigint;
  schoolName: string;
  schoolType: string;
  laName: string;
  value: number;
};
