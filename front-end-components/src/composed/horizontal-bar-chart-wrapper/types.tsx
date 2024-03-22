import { ChartDataSeriesSortMode } from "src/components/charts";
import { SchoolChartData } from "src/components/charts/table-chart";

export type HorizontalBarChartWrapperProps<TData extends SchoolChartData> = {
  chartName: string;
  children?: React.ReactNode[] | React.ReactNode;
  data: HorizontalBarChartWrapperPropsData<TData>;
  sort?: ChartDataSeriesSortMode<TData>;
};

export type HorizontalBarChartWrapperPropsData<TData extends SchoolChartData> =
  Omit<HorizontalBarChartWrapperData<TData>, "dataPoints"> & {
    dataPoints: TData[];
  };

export type HorizontalBarChartWrapperData<
  TData extends Omit<SchoolChartData, "value">,
> = {
  tableHeadings: string[];
  dataPoints: (TData & { value: number })[];
};
