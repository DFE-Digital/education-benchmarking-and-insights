import { CSSProperties, PureComponent, Ref, SVGProps } from "react";
import { CategoricalChartState } from "recharts/types/chart/types";
import {
  HorizontalAlignmentType,
  IconType,
  VerticalAlignmentType,
} from "recharts/types/component/DefaultLegendContent";
import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { ContentType } from "recharts/types/component/Tooltip";
import { CartesianTickItem } from "recharts/types/util/types";
import { DownloadMode } from "src/services";

export interface ChartProps<TData extends ChartDataSeries>
  extends ValueFormatterProps {
  barCategoryGap?: string | number;
  barGap?: string | number;
  barHeight?: number;
  chartTitle: string;
  className?: string;
  data: TData[];
  grid?: boolean;
  hideXAxis?: boolean;
  hideYAxis?: boolean;
  highlightActive?: boolean;
  highlightedItemKeys?: ChartSeriesValue[];
  specialItemKeys?: Partial<Record<SpecialItemFlag, ChartSeriesValue[]>>;
  keyField: keyof TData;
  labels?: boolean;
  legend?: boolean;
  legendIconSize?: number;
  legendIconType?: IconType | "default";
  legendHorizontalAlign?: HorizontalAlignmentType;
  legendVerticalAlign?: VerticalAlignmentType;
  legendWrapperStyle?: CSSProperties;
  linkToEstablishment?: boolean;
  margin?: number;
  missingDataKeys?: string[];
  multiLineAxisLabel?: boolean;
  onImageCopied?: (fileName: string) => void;
  onImageLoading?: (loading: boolean) => void;
  ref?: Ref<ChartHandler>;
  seriesConfig?: ChartSeriesConfig<TData>;
  seriesLabel?: string;
  seriesLabelField: keyof TData;
  seriesFormatter?: (value: unknown) => string;
  showCopyImageButton?: boolean;
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
  style?: "dashed";
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
  download: (mode: DownloadMode) => Promise<void>;
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
  forDisplay: boolean;
}

export interface ValueFormatterProps {
  valueFormatter?: ValueFormatterType;
}

export type ValueFormatterType = (
  value: ValueFormatterValue,
  options?: Partial<ValueFormatterOptions>
) => string;

export type SpecialItemFlag = "partYear" | "missingData";

export type CategoricalChartWrapper = PureComponent<
  unknown,
  CategoricalChartState
> & {
  container?: HTMLElement;
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  handleItemMouseEnter: (el: any) => void;
};
