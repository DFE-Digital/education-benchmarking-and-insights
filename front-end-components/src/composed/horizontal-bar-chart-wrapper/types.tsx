import { ChartDataSeriesSortMode, ChartProps } from "src/components/charts";
import {
  LaChartData,
  SchoolChartData,
  TrustChartData,
} from "src/components/charts/table-chart";
import { DimensionChartOverride } from "src/composed/dimensioned-chart/types";

export type HorizontalBarChartWrapperProps<
  TData extends SchoolChartData | TrustChartData | LaChartData,
> = Pick<
  ChartProps<TData>,
  | "chartTitle"
  | "linkToEstablishment"
  | "legend"
  | "legendContent"
  | "legendHorizontalAlign"
  | "legendVerticalAlign"
  | "missingDataKeys"
  | "progressAboveAverageKeys"
  | "progressWellAboveAverageKeys"
  | "showCopyImageButton"
  | "valueUnit"
  | "warningTag"
> & {
  children?: React.ReactNode[] | React.ReactNode;
  costCodesUnderTitle?: boolean;
  data: HorizontalBarChartWrapperPropsData<TData>;
  localAuthority?: boolean;
  override?: DimensionChartOverride;
  sort?: ChartDataSeriesSortMode<TData>;
  tooltip?: boolean;
  trust?: boolean;
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
