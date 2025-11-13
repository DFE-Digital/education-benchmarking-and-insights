import { PageActionsProps } from "src/components/page-actions";
import { CostCodeMap } from "../compare-your-costs/types";
import { CompareYourCosts2Props } from "./partials/accordion-sections/types";

export type CompareYourCosts2ViewProps = CompareYourCosts2Props &
  PageActionsProps & {
    costCodeMap?: CostCodeMap;
    customDataId: string | undefined;
    pageActionsDownloadLink?: string;
    pageActionsSaveId?: string;
    progressIndicators?: ProgressIndicators;
    suppressNegativeOrZero: boolean;
    tags?: string[];
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

export type SchoolExpenditureCommon = {
  urn: string;
  schoolType: string;
  totalPupils: bigint;
  schoolName: string;
  laName: string;
  progressBanding: ProgressBanding | undefined;
};
