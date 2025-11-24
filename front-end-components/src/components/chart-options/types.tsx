import { ChartPhasesProps } from "../chart-phases";

export type ChartOptionsProps = Partial<ChartPhasesProps> & {
  className?: string;
  stacked?: boolean;
};
