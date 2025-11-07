import { ProgressBanding } from "src/views";

export type ChartProgressProps = {
  options: ProgressBanding[];
  selected: ProgressBanding[];
  stacked?: boolean;
  onChecked: (progress: ProgressBanding) => void;
};
