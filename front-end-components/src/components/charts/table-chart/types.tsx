import { ChartSeriesValueUnit } from "src/components/charts/types";

export type TableChartProps<TData extends SchoolChartData> = {
  tableHeadings: string[];
  data?: TData[];
  preventFocus?: boolean;
  valueUnit?: ChartSeriesValueUnit | undefined;
};

export type SchoolChartData = {
  urn: string;
  name: string;
  value: number;
  schoolType: string;
  localAuthority: string;
  numberOfPupils: bigint;
};
