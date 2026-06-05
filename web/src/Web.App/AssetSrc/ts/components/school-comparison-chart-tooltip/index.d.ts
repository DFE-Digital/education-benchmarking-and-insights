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
  shouldShowTag?: boolean;
}

export interface SchoolComparisonChartTooltipsProps {
  data: SchoolComparisonChartTooltipPropsData[];
}

export type FocusSource = "keyboard" | "mouse";
