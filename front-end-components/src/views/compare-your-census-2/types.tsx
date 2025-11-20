import { PageActionsProps, ProgressIndicators } from "src/components";

export type CompareYourCensus2ViewProps = PageActionsProps & {
  customDataId: string | undefined;
  id: string;
  phases: string[] | null;
  progressIndicators?: ProgressIndicators;
  type: string;
};
