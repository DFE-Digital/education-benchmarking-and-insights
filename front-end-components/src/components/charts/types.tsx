import React, { Ref, SVGProps } from "react";
import { CartesianTickItem } from "recharts/types/util/types";

export type HorizontalBarChartWrapperProps = {
  chartName: string;
  children?: React.ReactNode[] | React.ReactNode;
  data: HorizontalBarChartWrapperData;
  sort?: ChartSortMode;
};

export type HorizontalBarChartWrapperData = {
  tableHeadings: string[];
  dataPoints: ChartDataPoint[];
};

export type ChartDataPoint = {
  school: string;
  urn: string;
  value: number;
  additionalData?: (string | bigint)[];
};

export type ChartSortMode = {
  dataPoint: Exclude<keyof ChartDataPoint, "additionalData">;
  direction: "asc" | "desc";
};

export interface ChartProps<TData extends ChartDataSeries> {
  chartName: string;
  data: TData[];
  grid?: boolean;
  keyField: keyof TData;
  margin?: number;
  multiLineAxisLabel?: boolean;
  onImageLoading?: (loading: boolean) => void;
  ref?: Ref<ChartHandler>;
  seriesConfig?: ChartSeriesConfig<TData>;
  seriesLabel?: string;
  seriesLabelField: keyof TData;
  tooltip?: boolean;
  valueLabel?: string;
  valueUnit?: ChartSeriesValueUnit;
}

type ChartSeriesConfig<TData extends ChartDataSeries> = Partial<
  Record<
    keyof TData,
    {
      className?: string;
      label?: string;
      visible: boolean;
    }
  >
>;

type ChartSeriesName = string;
export type ChartSeriesValue = string | number;
export type ChartSeriesValueUnit = "%" | "currency";
export type ChartDataSeries = { [name: ChartSeriesName]: ChartSeriesValue };

export type TickProps = SVGProps<SVGGElement> & {
  index: number;
  payload: CartesianTickItem;
  visibleTicksCount: number;
};

export type ChartHandler = {
  download: () => void;
};
