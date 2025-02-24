import { ChartDataSeriesSortMode, ChartProps } from "src/components/charts";
import {
  LaChartData,
  SchoolChartData,
  TrustChartData,
} from "src/components/charts/table-chart";

export type HorizontalBarChartWrapperProps<
  TData extends SchoolChartData | TrustChartData | LaChartData,
> = Pick<
  ChartProps<TData>,
  "chartTitle" | "showCopyImageButton" | "valueUnit" | "linkToEstablishment"
> & {
  children?: React.ReactNode[] | React.ReactNode;
  data: HorizontalBarChartWrapperPropsData<TData>;
  sort?: ChartDataSeriesSortMode<TData>;
  trust?: boolean;
  localAuthority?: boolean;
  tooltip?: boolean;
  xAxisLabel?: string;
};

export type HorizontalBarChartWrapperPropsData<
  TData extends SchoolChartData | TrustChartData | LaChartData,
> = Omit<HorizontalBarChartWrapperData<TData>, "dataPoints"> & {
  dataPoints: TData[];
};

export type HorizontalBarChartWrapperData<
  TData extends Omit<
    SchoolChartData | TrustChartData | LaChartData,
    "value" | "totalValue" | "schoolValue" | "centralValue"
  >,
> = {
  tableHeadings: string[];
  dataPoints: (TData &
    (
      | { value: number }
      | {
          totalValue: number;
          schoolValue: number | undefined;
          centralValue: number | undefined;
        }
    ))[];
};
