import { ReactNode } from "react";
import { ChartDataSeries, ChartProps } from "src/components/charts/types";
import { ResolvedStatProps } from "src/components/charts/resolved-stat";

export interface HistoricChartProps<T extends ChartDataSeries> {
  chartName: string;
  data: T[];
  seriesConfig: ChartProps<T>["seriesConfig"];
  valueField: ResolvedStatProps<T>["valueField"];
  children?: ReactNode;
}
