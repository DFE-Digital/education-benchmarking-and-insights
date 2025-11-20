import {
  PageActionsProps,
  ProgressBanding,
  ProgressIndicators,
} from "src/components";
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

export type SchoolExpenditureCommon = {
  urn: string;
  schoolType: string;
  totalPupils: bigint;
  schoolName: string;
  laName: string;
  progressBanding: ProgressBanding | undefined;
};
