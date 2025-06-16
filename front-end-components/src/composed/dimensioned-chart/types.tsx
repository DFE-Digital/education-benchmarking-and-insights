import { ReactNode } from "react";
import {
  ChartSeriesValueUnit,
  Dimension,
  ValueFormatterType,
} from "src/components";
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
  dimensions?: Dimension[];
  override?: DimensionChartOverride;
};

export type DimensionChartOverride = {
  valueUnit: ChartSeriesValueUnit;
  valueLabel: string;
  valueFormatter: ValueFormatterType;
  suppressNegativeOrZero: boolean;
  suppressNegativeOrZeroMessage: string;
  customTooltip?: "HighExec";
};

export type DimensionedChartProps<
  TData extends SchoolChartData | TrustChartData,
> = Pick<
  HorizontalBarChartWrapperProps<TData>,
  "showCopyImageButton" | "trust"
> & {
  charts: DimensionedChart<TData>[];
  dimension: Dimension;
  dimensions?: Dimension[];
  handleDimensionChange?: (dimension: string) => void;
  hasNoData?: boolean;
  options?: ReactNode;
  topLevel?: boolean;
};
