import { CompareYourCostsProps } from "./partials/accordion-sections/types";

export type CompareYourCostsViewProps = CompareYourCostsProps & {
  costCodeMap?: CostCodeMap;
  customDataId: string | undefined;
  dispatchEventType?: string;
  phases: string[] | null;
  suppressNegativeOrZero: boolean;
  tags?: string[];
};

export type CostCodeMap = Record<string, string | string[] | undefined>;
