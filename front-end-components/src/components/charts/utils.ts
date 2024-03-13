import {
  ChartDataPoint,
  ChartDataSeries,
  ChartDataSeriesSortMode,
  ChartSortMode,
} from ".";

// eslint-disable-next-line @typescript-eslint/no-explicit-any
function chartComparerCommon<TData extends Record<string, any>>(
  a: TData,
  b: TData,
  { dataPoint, direction }: ChartDataSeriesSortMode<TData>
) {
  if (a[dataPoint] < b[dataPoint]) {
    return direction === "asc" ? -1 : 1;
  }

  if (a[dataPoint] > b[dataPoint]) {
    return direction === "asc" ? 1 : -1;
  }

  return 0;
}

export function chartComparer(
  a: ChartDataPoint,
  b: ChartDataPoint,
  sortMode: ChartSortMode
): number {
  return chartComparerCommon(a, b, sortMode);
}

// `ChartDataSeries` differs to `ChartDataPoint` with respect to the supported value types.
// Once all charts have been switched to use `ChartDataSeries` instead then `chartComparer()`
// may be removed and `chartComparerCommon()` merged into `chartSeriesComparer()`.
export function chartSeriesComparer<TData extends ChartDataSeries>(
  a: TData,
  b: TData,
  sortMode: ChartDataSeriesSortMode<TData>
): number {
  return chartComparerCommon(a, b, sortMode);
}
