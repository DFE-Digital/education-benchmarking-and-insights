import {
  ChartDataSeries,
  ChartDataSeriesSortMode,
  ChartProps,
} from "src/components/charts";
import {
  SchoolChartData,
  TrustChartData,
} from "src/components/charts/table-chart";
import { SortedChartDataSeries } from "src/components/charts/utils";

export type HorizontalBarChartWrapperProps<TData extends ChartDataSeries> =
  Pick<ChartProps<TData>, "chartName" | "valueUnit"> & {
    children?: React.ReactNode[] | React.ReactNode;
    data: HorizontalBarChartWrapperPropsData<TData>;
    sort?: ChartDataSeriesSortMode<SortedChartDataSeries>;
    trust?: boolean;
    showEstimatedValues?: boolean;
  };

export type HorizontalBarChartWrapperPropsData<TData extends ChartDataSeries> =
  Omit<HorizontalBarChartWrapperData<TData>, "dataPoints"> & {
    dataPoints: TData[];
  };

export type HorizontalBarChartWrapperData<
  TData extends Omit<
    SchoolChartData | TrustChartData,
    | "value"
    | "totalValue"
    | "schoolValue"
    | "centralValue"
    | "estimatedValueDifference"
  >,
> = {
  tableHeadings: string[];
  dataPoints: (TData &
    (
      | { value: number }
      | { totalValue: number; schoolValue: number; centralValue: number }
    ))[];
};
