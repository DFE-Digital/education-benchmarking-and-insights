import { ReactNode } from "react";
import {
  ChartSeriesValue,
  ChartSeriesValueUnit,
} from "src/components/charts/types";
import { ResolvedStatProps } from "src/components/charts/resolved-stat";
import {
  HistoryBase,
  LocalAuthoritySection251,
  LocalAuthoritySection251History,
} from "src/services";
import { LineChartProps } from "src/components/charts/line-chart";

export interface HistoricChartSection251Props<
  TData extends LocalAuthoritySection251,
> extends Pick<
    LineChartProps<TData>,
    | "legend"
    | "legendIconSize"
    | "legendIconType"
    | "legendHorizontalAlign"
    | "legendVerticalAlign"
    | "legendWrapperStyle"
    | "showCopyImageButton"
  > {
  chartTitle: string;
  data: LocalAuthoritySection251History<TData>[] | undefined;
  valueField: ResolvedStatProps<TData>["valueField"];
  children?: ReactNode;
  valueUnit?: ChartSeriesValueUnit;
  axisLabel?: string;
}

export type Section251HistoryValue = HistoryBase & {
  outturn?: ChartSeriesValue;
  budget?: ChartSeriesValue;
};
