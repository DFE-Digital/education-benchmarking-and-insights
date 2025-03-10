import { ChartProps } from "src/components/charts";
import { LaChartData } from "src/components/charts/table-chart";

export type HorizontalBarChartMultiSeriesProps<TData extends LaChartData> =
  Pick<
    ChartProps<TData>,
    | "chartTitle"
    | "keyField"
    | "seriesConfig"
    | "seriesLabelField"
    | "showCopyImageButton"
    | "valueUnit"
  > & {
    children?: React.ReactNode[] | React.ReactNode;
    data: HorizontalBarChartMultiSeriesPropsData<TData>;
    xAxisLabel?: string;
  };

export type HorizontalBarChartMultiSeriesPropsData<TData extends LaChartData> =
  Omit<HorizontalBarChartMultiSeriesData<TData>, "dataPoints"> & {
    dataPoints: TData[];
  };

export type HorizontalBarChartMultiSeriesData<
  TData extends Omit<LaChartData, "value" | "budget" | "outturn">,
> = {
  tableHeadings: string[];
  dataPoints: (TData & {
    value: number;
    budget: number | undefined;
    outturn: number | undefined;
  })[];
};
