import { ProgressBanding } from "src/components";

export type ChartProgressProps = {
  options: ProgressBanding[];
  defaultSelected: ProgressBanding[];
  stacked?: boolean;
  onChanged: (selected: ProgressBanding[]) => void;
};
