import React, { Ref, SVGProps } from "react";
import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { ContentType } from "recharts/types/component/Tooltip";
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
  hideXAxis?: boolean;
  hideYAxis?: boolean;
  highlightActive?: boolean;
  keyField: keyof TData;
  labels?: boolean;
  margin?: number;
  multiLineAxisLabel?: boolean;
  onImageLoading?: (loading: boolean) => void;
  ref?: Ref<ChartHandler>;
  seriesConfig?: ChartSeriesConfig<TData>;
  seriesLabel?: string;
  seriesLabelField: keyof TData;
  tooltip?: boolean | ContentType<ValueType, NameType>;
  valueLabel?: string;
  valueUnit?: ChartSeriesValueUnit;
  suffix?: string;
}

export interface ChartSeriesConfigItem {
  className?: string;
  label?: string;
  visible: boolean;
  formatter?: (value: ChartSeriesValue) => ChartSeriesValue;
}

type ChartSeriesConfig<TData extends ChartDataSeries> = Partial<
  Record<keyof TData, ChartSeriesConfigItem>
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

export type ChartSortDirection = "asc" | "desc";

export type ChartDataSeriesSortMode<TData extends ChartDataSeries> = {
  dataPoint: keyof TData;
  direction: ChartSortDirection;
};

export type ChartDataAverage = "mean" | "median" | "mode";
