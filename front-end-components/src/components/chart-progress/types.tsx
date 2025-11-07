import { ProgressBanding } from "src/views";

export type ChartProgressProps = {
  options: ProgressBanding[];
  defaultSelected: ProgressBanding[];
  stacked?: boolean;
  onChanged: (selected: ProgressBanding[]) => void;
};
