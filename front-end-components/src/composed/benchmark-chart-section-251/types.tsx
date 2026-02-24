import { ResolvedStatProps } from "src/components/charts/resolved-stat";
import { SourceInfoProps } from "src/components/source-info";
import {
  LocalAuthoritySection251,
  LocalAuthoritySection251Benchmark,
} from "src/services";

export interface BenchmarkChartSection251Props<
  TData extends LocalAuthoritySection251,
> {
  chartTitle: string;
  data: LocalAuthoritySection251Benchmark<TData>[] | undefined;
  valueField: ResolvedStatProps<TData>["valueField"];
  sourceInfo?: SourceInfoProps;
}
