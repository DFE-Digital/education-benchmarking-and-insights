import { ChartDataPoint, ChartSortMode } from ".";

export function chartComparer(
  a: ChartDataPoint,
  b: ChartDataPoint,
  { dataPoint, direction }: ChartSortMode
): number {
  if (a[dataPoint] < b[dataPoint]) {
    return direction === "asc" ? -1 : 1;
  }

  if (a[dataPoint] > b[dataPoint]) {
    return direction === "asc" ? 1 : -1;
  }

  return 0;
}
