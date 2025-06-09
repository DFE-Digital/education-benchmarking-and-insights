import { CompareYourCostsProps } from "./partials/accordion-sections/types";

export type CompareYourCostsViewProps = CompareYourCostsProps & {
  costCodeMap?: Record<string, string>;
  customDataId: string | undefined;
  dispatchEventType?: string;
  phases: string[] | null;
  suppressNegativeOrZero: boolean;
  tags?: string[];
};
