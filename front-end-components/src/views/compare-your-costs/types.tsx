import { CompareYourCostsProps } from "./partials/accordion-sections/types";

export type CompareYourCostsViewProps = CompareYourCostsProps & {
  customDataId: string | undefined;
  dispatchEventType?: string;
  phases: string[] | null;
  suppressNegativeOrZero: boolean;
};
