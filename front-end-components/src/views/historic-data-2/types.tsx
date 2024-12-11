import { Dimension } from "src/components";
import { ResolvedStatProps } from "src/components/charts/resolved-stat";
import { SchoolHistoryBase } from "src/services";

export type HistoricData2Props = {
  type: string;
  id: string;
  overallPhase?: string;
  financeType?: string;
};

export type HistoricData2Section<T extends SchoolHistoryBase> = {
  heading: string;
  charts: HistoricData2SectionChart<T>[];
};

type HistoricData2SectionChart<T extends SchoolHistoryBase> = {
  name: string;
  field: ResolvedStatProps<T>["valueField"] | "totalCateringCostsField";
  type?: "school" | "trust" | "local-authority";
  perUnitDimension: Dimension;
};
