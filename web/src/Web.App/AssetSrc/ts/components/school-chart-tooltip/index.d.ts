export interface SchoolChartTooltipProps {
  datum: SchoolChartTooltipPropsData | null;
  x: number;
  y: number;
  focusSource?: FocusSource;
}

export interface SchoolChartTooltipPropsData {
  urn?: string;
  schoolName?: string;
  laName?: string;
  totalPupils?: number;
  periodCoveredByReturn?: number;
};

export interface SchoolChartTooltipsProps {
  data: SchoolChartTooltipPropsData[];
}

export type FocusSource = "keyboard" | "mouse";
