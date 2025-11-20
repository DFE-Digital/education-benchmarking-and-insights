import {
  PageActionsProps,
  ProgressBanding,
  ProgressIndicators,
} from "src/components";

export type CompareYourCensus2ViewProps = PageActionsProps & {
  customDataId: string | undefined;
  id: string;
  phases: string[] | null;
  progressIndicators?: ProgressIndicators;
  type: string;
};

export type SchoolCensusCommon = {
  urn: string;
  totalPupils: bigint;
  schoolName: string;
  schoolType: string;
  laName: string;
  progressBanding: ProgressBanding | undefined;
};
