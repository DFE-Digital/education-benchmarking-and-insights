import { ReactNode } from "react";
import { Dimension } from "src/components";
import {
  SchoolChartData,
  TrustChartData,
} from "src/components/charts/table-chart";
import { HorizontalBarChartWrapperProps } from "src/composed/horizontal-bar-chart-wrapper";

export type SectionProps<TData extends SchoolChartData | TrustChartData> = {
  charts: (Pick<HorizontalBarChartWrapperProps<TData>, "data"> & {
    title: string;
  })[];
  dimension: Dimension;
  dimensions?: Dimension[];
  handleDimensionChange?: (dimension: string) => void;
  hasNoData: boolean;
  options?: ReactNode;
  topLevel?: boolean;
};

export type TotalExpenditureProps = {
  schools: TotalExpenditureData[];
};

export type TotalExpenditureData = {
  urn: string;
  schoolType: string;
  totalExpenditure: number;
  totalPupils: bigint;
  schoolName: string;
  laName: string;
};
