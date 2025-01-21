import { ReactNode } from "react";
import { Dimension } from "src/components";
import {
  SchoolChartData,
  TrustChartData,
} from "src/components/charts/table-chart";
import { HorizontalBarChartWrapperProps } from "src/composed/horizontal-bar-chart-wrapper";

type DimensionedChart<TData extends SchoolChartData | TrustChartData> = Pick<
  HorizontalBarChartWrapperProps<TData>,
  "data"
> & {
  selector?: boolean;
  title: string;
};

export type DimensionedChartProps<
  TData extends SchoolChartData | TrustChartData,
> = {
  charts: DimensionedChart<TData>[];
  dimension: Dimension;
  dimensions?: Dimension[];
  handleDimensionChange?: (dimension: string) => void;
  hasNoData?: boolean;
  options?: ReactNode;
  topLevel?: boolean;
  trust?: boolean;
};
