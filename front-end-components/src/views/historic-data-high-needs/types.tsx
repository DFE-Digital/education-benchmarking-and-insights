import { ReactNode } from "react";
import { ResolvedStatProps } from "src/components/charts/resolved-stat";
import { HistoricChartSection251Props } from "src/composed/historic-chart-section-251-composed";
import { LocalAuthoritySection251 } from "src/services";

export type HistoricDataHighNeedsProps = {
  code: string;
  load?: boolean;
  fetchTimeout?: number;
};

export type HistoricDataHighNeedsViewProps = Omit<
  HistoricDataHighNeedsProps,
  "load"
> & {
  preLoadSections?: HistoricDataHighNeedsSectionName[];
};

export type HistoricDataHighNeedsSectionName = "section-251" | "send-2";

export type HistoricChartSection251Section<
  TData extends LocalAuthoritySection251,
> = {
  heading: string;
  summary?: string;
  charts: HistoricDataHighNeedsSection251Chart<TData>[];
};

export type HistoricDataHighNeedsSection251Chart<
  TData extends LocalAuthoritySection251,
> = Pick<HistoricChartSection251Props<TData>, "valueUnit" | "axisLabel"> & {
  name: string;
  field: ResolvedStatProps<TData>["valueField"];
  details?: {
    label: string;
    content: ReactNode;
  };
};
