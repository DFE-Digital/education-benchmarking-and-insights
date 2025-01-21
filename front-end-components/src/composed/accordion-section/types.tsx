import {
  SchoolChartData,
  TrustChartData,
} from "src/components/charts/table-chart";
import { DimensionedChartProps } from "../dimensioned-chart";

export type AccordionSectionProps<
  TData extends SchoolChartData | TrustChartData,
> = Omit<DimensionedChartProps<TData>, "dimensions" | "topLevel"> & {
  index: number;
  title: string;
};
