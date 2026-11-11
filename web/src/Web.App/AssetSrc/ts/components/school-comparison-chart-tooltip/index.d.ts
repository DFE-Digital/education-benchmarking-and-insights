export interface SchoolComparisonChartTooltipProps {
  datum: SchoolChartTooltipPropsData | null;
  x: number;
  y: number;
  focusSource?: FocusSource;
}

export interface SchoolComparisonChartTooltipPropsData {
  urn?: string;
  schoolName?: string;
  laName?: string;
  totalPupils?: number;
  periodCoveredByReturn?: number;
  progressBanding?: string;
  progressBandingColour?: string;
  shouldShowKs4ProgressTag?: boolean;
  shouldShowAttainment8Tag?: boolean;
  setType?: string;
}

export interface SchoolComparisonChartTooltipsProps {
  data: SchoolComparisonChartTooltipPropsData[];
}

export type FocusSource = "keyboard" | "mouse";
