import { SVGProps } from "react";
import { CartesianTickItem } from "recharts/types/util/types";

export interface ChartProps<TData extends ChartDataSeries> {
  chartName: string;
  data: TData[];
  grid?: boolean;
  highlightedItemKeys?: ChartSeriesValue[];
  keyField: keyof TData;
  legend?: boolean;
  margin?: number;
  multiLineAxisLabel?: boolean;
  ref?: ChartHandler;
  seriesConfig?: ChartSeriesConfig<TData>;
  seriesLabel?: string;
  seriesLabelField: keyof TData;
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
type ChartSeriesValue = string | number;
type ChartSeriesValueUnit = "%";
export type ChartDataSeries = { [name: ChartSeriesName]: ChartSeriesValue };

export type ChartHandler = {
  download: () => void;
};

export type TickProps = SVGProps<SVGGElement> & {
  index: number;
  payload: CartesianTickItem;
  visibleTicksCount: number;
};
