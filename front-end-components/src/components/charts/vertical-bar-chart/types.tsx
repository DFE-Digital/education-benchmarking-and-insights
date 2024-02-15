export interface ChartProps<TData extends ChartDataSeries> {
  chartName: string;
  data: TData[];
  gridEnabled?: boolean;
  highlightedItemKeys?: ChartSeriesValue[];
  keyField: keyof TData;
  margin?: number;
  ref?: ChartHandler;
  seriesConfig?: ChartSeriesConfig<TData>;
  seriesLabel?: string;
  seriesLabelField: keyof TData;
  seriesLabelRotate?: boolean;
  legendEnabled?: boolean;
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
