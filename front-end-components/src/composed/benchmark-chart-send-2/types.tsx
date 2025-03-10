import { ChartSeriesValue } from "src/components/charts/types";
import { ResolvedStatProps } from "src/components/charts/resolved-stat";
import { LocalAuthoritySend2Benchmark } from "src/services";
import { LaChartData } from "src/components/charts/table-chart";

export interface BenchmarkChartSend2Props<
  TData extends LocalAuthoritySend2Benchmark,
> {
  chartTitle: string;
  data: TData[] | undefined;
  valueField: ResolvedStatProps<TData>["valueField"];
}

export type Send2BenchmarkValue = LaChartData & {
  outturn?: ChartSeriesValue;
  budget?: ChartSeriesValue;
};
