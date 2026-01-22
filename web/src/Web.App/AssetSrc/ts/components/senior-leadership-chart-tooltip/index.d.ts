export interface SeniorLeadershipChartTooltipProps {
  datum: SeniorLeadershipChartTooltipPropsData | null;
  x: number;
  y: number;
  focusSource?: FocusSource;
}

export interface SeniorLeadershipChartTooltipPropsData {
  urn?: string;
  schoolName?: string;
  laName?: string;
  totalPupils?: number;
  headTeachers?: number;
  deputyHeadTeachers?: number;
  assistantHeadTeachers?: number;
  leadershipNonTeachers?: number;
}

export interface SeniorLeadershipChartTooltipsProps {
  data: SeniorLeadershiplChartTooltipPropsData[];
}

export type FocusSource = "keyboard" | "mouse";
