import { CompareYourCostsProps } from "./partials/accordion-sections/types";

export type CompareYourCostsViewProps = CompareYourCostsProps & {
  phases: string[] | null;
  customDataId: string | undefined;
  suppressNegativeOrZero: boolean;
};
