export interface SchoolChartTooltipProps {
datum: SchoolChartTooltipPropsData | null;
x: number;
y: number;
}

export interface SchoolChartTooltipPropsData {
  urn?: string;
  schoolName?: string;
  laName?: string;
  schoolType?: string;
  totalPupils?: number;
  periodCoveredByReturn?: number;
};
