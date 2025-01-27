import { ReactNode } from "react";
import {
  ChartDataSeries,
  ChartProps,
  ChartSeriesValueUnit,
} from "src/components/charts/types";
import { ResolvedStatProps } from "src/components/charts/resolved-stat";
import { LineChartProps } from "src/components/charts/line-chart";

export interface HistoricChartProps<T extends ChartDataSeries>
  extends Pick<LineChartProps<T>, "showCopyImageButton"> {
  chartTitle: string;
  data: T[];
  seriesConfig: ChartProps<T>["seriesConfig"];
  valueField: ResolvedStatProps<T>["valueField"];
  children?: ReactNode;
  valueUnit?: ChartSeriesValueUnit;
  axisLabel?: string;
  columnHeading?: string;
}
