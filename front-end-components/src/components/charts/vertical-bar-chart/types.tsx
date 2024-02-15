export interface ChartProps<TData extends ChartDataSeries> {
  chartName: string;
  data: TData[];
  highlightedItemKeys?: ChartSeriesValue[];
  keyField: keyof TData;
  margin?: number;
  ref?: ChartHandler;
  seriesConfig?: ChartSeriesConfig<TData>;
  seriesLabel?: string;
  seriesLabelField: keyof TData;
  valueLabel?: string;
}

type ChartSeriesConfig<TData extends ChartDataSeries> = Partial<
  Record<
    keyof TData,
    {
      visible: boolean;
      className?: string;
    }
  >
>;

type ChartSeriesName = string;
type ChartSeriesValue = string | number;
export type ChartDataSeries = { [name: ChartSeriesName]: ChartSeriesValue };

export type ChartHandler = {
  download: () => void;
};
