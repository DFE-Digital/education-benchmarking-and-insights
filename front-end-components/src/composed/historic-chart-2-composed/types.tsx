import { ReactNode } from "react";
import {
  ChartSeriesValue,
  ChartSeriesValueUnit,
} from "src/components/charts/types";
import { ResolvedStatProps } from "src/components/charts/resolved-stat";
import { HistoryBase, SchoolHistoryComparison } from "src/services";
import { LineChartProps } from "src/components/charts/line-chart";
import { Dimension } from "src/components";

export interface HistoricChart2Props<T extends HistoryBase>
  extends Pick<
    LineChartProps<T>,
    | "legend"
    | "legendIconSize"
    | "legendIconType"
    | "legendHorizontalAlign"
    | "legendVerticalAlign"
    | "legendWrapperStyle"
  > {
  chartTitle: string;
  data: SchoolHistoryComparison<T>;
  valueField: ResolvedStatProps<T>["valueField"];
  children?: ReactNode;
  valueUnit?: ChartSeriesValueUnit;
  axisLabel?: string;
  columnHeading?: string;
  perUnitDimension: Dimension;
}

export type SchoolHistoryValue = HistoryBase & {
  school?: ChartSeriesValue;
  comparatorSetAverage?: ChartSeriesValue;
  nationalAverage?: ChartSeriesValue;
};
