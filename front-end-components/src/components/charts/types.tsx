import { Ref, SVGProps } from "react";
import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { ContentType } from "recharts/types/component/Tooltip";
import { CartesianTickItem } from "recharts/types/util/types";

export interface ChartProps<TData extends ChartDataSeries>
  extends ValueFormatterProps {
  barCategoryGap?: string | number;
  chartName: string;
  data: TData[];
  grid?: boolean;
  hideXAxis?: boolean;
  hideYAxis?: boolean;
  highlightActive?: boolean;
  highlightedItemKeys?: ChartSeriesValue[];
  keyField: keyof TData;
  labels?: boolean;
  legend?: boolean;
  margin?: number;
  multiLineAxisLabel?: boolean;
  onImageLoading?: (loading: boolean) => void;
  ref?: Ref<ChartHandler>;
  seriesConfig?: ChartSeriesConfig<TData>;
  seriesLabel?: string;
  seriesLabelField: keyof TData;
  suffix?: string;
  tooltip?: ContentType<ValueType, NameType>;
  valueLabel?: string;
  valueUnit?: ChartSeriesValueUnit;
}

export interface ChartSeriesConfigItem extends ValueFormatterProps {
  className?: string;
  label?: string;
  visible: boolean;
  stackId?: number;
}

type ChartSeriesConfig<TData extends ChartDataSeries> = Partial<
  Record<keyof TData, ChartSeriesConfigItem>
>;

type ChartSeriesName = string;
export type ChartSeriesValue = string | number | bigint | boolean;
export type ChartSeriesValueUnit = "%" | "currency" | "amount";
export type ChartDataSeries = { [name: ChartSeriesName]: ChartSeriesValue };

export type TickProps = SVGProps<SVGGElement> & {
  index: number;
  payload: CartesianTickItem;
  visibleTicksCount: number;
};

export type ChartHandler = {
  download: () => Promise<void>;
};

export type ChartSortDirection = "asc" | "desc";

export type ChartDataSeriesSortMode<TData extends ChartDataSeries> = {
  dataPoint: keyof TData;
  direction: ChartSortDirection;
};

export type ChartDataAverage = "mean" | "median" | "mode";

export type ValueFormatterValue = ChartSeriesValue | ValueType | undefined;

export interface ValueFormatterOptions {
  valueUnit: ChartSeriesValueUnit;
  compact: boolean;
  currencyAsName: boolean;
}

export interface ValueFormatterProps {
  valueFormatter?: (
    value: ValueFormatterValue,
    options?: Partial<ValueFormatterOptions>
  ) => string;
}
