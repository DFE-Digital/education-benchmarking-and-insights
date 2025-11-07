import { CostCodeMap } from "../compare-your-costs/types";
import { CompareYourCosts2Props } from "./partials/accordion-sections/types";

export type CompareYourCosts2ViewProps = CompareYourCosts2Props & {
  costCodeMap?: CostCodeMap;
  customDataId: string | undefined;
  dispatchEventType?: string;
  suppressNegativeOrZero: boolean;
  tags?: string[];
  progressIndicators?: ProgressIndicators;
};

export enum ProgressBanding {
  WellBelowAverage = "WellBelowAverage",
  BelowAverage = "BelowAverage",
  Average = "Average",
  AboveAverage = "AboveAverage",
  WellAboveAverage = "WellAboveAverage",
}

export type KS4ProgressBanding = {
  urn: string;
  banding: ProgressBanding;
};

export type ProgressIndicators = KS4ProgressBanding[];
