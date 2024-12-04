import { ReactNode } from "react";
import {
  ChartSeriesValue,
  ChartSeriesValueUnit,
} from "src/components/charts/types";
import { ResolvedStatProps } from "src/components/charts/resolved-stat";
import { SchoolHistoryBase, SchoolHistoryComparison } from "src/services";
import { LineChartProps } from "src/components/charts/line-chart";

export interface HistoricChart2Props<T extends SchoolHistoryBase>
  extends Pick<
    LineChartProps<T>,
    | "legend"
    | "legendIconType"
    | "legendHorizontalAlign"
    | "legendVerticalAlign"
  > {
  chartName: string;
  data: SchoolHistoryComparison<T>;
  valueField: ResolvedStatProps<T>["valueField"];
  children?: ReactNode;
  valueUnit?: ChartSeriesValueUnit;
  axisLabel?: string;
  columnHeading?: string;
}

export type SchoolHistoryValue = SchoolHistoryBase & {
  school?: ChartSeriesValue;
  comparatorSetAverage?: ChartSeriesValue;
  nationalAverage?: ChartSeriesValue;
};
