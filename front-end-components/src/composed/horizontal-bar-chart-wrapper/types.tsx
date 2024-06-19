import { ChartDataSeriesSortMode, ChartProps } from "src/components/charts";
import {
  SchoolChartData,
  TrustChartData,
} from "src/components/charts/table-chart";

export type HorizontalBarChartWrapperProps<
  TData extends SchoolChartData | TrustChartData,
> = Pick<ChartProps<TData>, "chartName" | "valueUnit"> & {
  children?: React.ReactNode[] | React.ReactNode;
  data: HorizontalBarChartWrapperPropsData<TData>;
  sort?: ChartDataSeriesSortMode<TData>;
  trust?: boolean;
};

export type HorizontalBarChartWrapperPropsData<
  TData extends SchoolChartData | TrustChartData,
> = Omit<HorizontalBarChartWrapperData<TData>, "dataPoints"> & {
  dataPoints: TData[];
};

export type HorizontalBarChartWrapperData<
  TData extends Omit<
    SchoolChartData | TrustChartData,
    "value" | "totalValue" | "schoolValue" | "centralValue"
  >,
> = {
  tableHeadings: string[];
  dataPoints: (TData &
    (
      | { value: number }
      | { totalValue: number; schoolValue: number; centralValue: number }
    ))[];
};
