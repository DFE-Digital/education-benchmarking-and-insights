import {
  ChartSeriesValueUnit,
  ValueFormatterType,
} from "src/components/charts/types";
import { TableCellEstablishmentNameProps } from "./partials";
import { ProgressBanding } from "src/components/progress-banding-tag";

export type TableChartProps<
  TData extends SchoolChartData | TrustChartData | LaChartData,
> = Pick<
  TableCellEstablishmentNameProps<TData>,
  "preventFocus" | "warningTag"
> & {
  data?: TData[];
  linkToEstablishment?: boolean;
  localAuthority?: boolean;
  tableHeadings: string[];
  trust?: boolean;
  valueUnit?: ChartSeriesValueUnit | undefined;
  valueFormatter?: ValueFormatterType;
};

export type SchoolChartData = {
  urn: string;
  totalPupils: bigint;
  schoolName: string;
  schoolType: string;
  laName: string;
  value?: number;
  periodCoveredByReturn?: number;
  progressBanding?: ProgressBanding;
};

export type TrustChartData = {
  companyNumber: string;
  trustName: string;
  schoolValue?: number;
  centralValue?: number;
  totalValue?: number;
  totalPupils?: number;
  type?: "expenditure" | "balance";
};

export type LaChartData = {
  laCode: string;
  laName: string;
  totalPupils?: number;
  value?: number;
  outturn?: number;
  budget?: number;
};
