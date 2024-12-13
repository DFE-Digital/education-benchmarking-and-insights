import { ReactNode } from "react";
import { ResolvedStatProps } from "src/components/charts/resolved-stat";
import { HistoricChart2Props } from "src/composed/historic-chart-2-composed";
import { SchoolHistoryBase } from "src/services";

export type HistoricData2Props = {
  type: string;
  id: string;
  overallPhase?: string;
  financeType?: string;
  load?: boolean;
};

export type HistoricData2ViewProps = Omit<HistoricData2Props, "load"> & {
  preLoadSections?: HistoricData2SectionName[];
};

export type HistoricData2SectionName =
  | "spending"
  | "income"
  | "balance"
  | "census";

export type HistoricData2Section<T extends SchoolHistoryBase> = {
  heading: string;
  charts: HistoricData2SectionChart<T>[];
};

export type HistoricData2SectionChart<T extends SchoolHistoryBase> = Pick<
  HistoricChart2Props<T>,
  "valueUnit" | "axisLabel" | "columnHeading" | "perUnitDimension"
> & {
  name: string;
  field: ResolvedStatProps<T>["valueField"];
  type?: "school" | "trust" | "local-authority";
  details?: {
    label: string;
    content: ReactNode;
  };
};
