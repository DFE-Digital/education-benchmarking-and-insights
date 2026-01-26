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
  headTeachers?: string;
  deputyHeadTeachers?: string;
  assistantHeadTeachers?: string;
  leadershipNonTeachers?: string;
}

export interface SeniorLeadershipChartTooltipsProps {
  data: SeniorLeadershiplChartTooltipPropsData[];
}

export type FocusSource = "keyboard" | "mouse";
