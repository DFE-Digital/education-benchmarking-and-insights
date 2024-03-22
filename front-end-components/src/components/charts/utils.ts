import {
  ChartDataPoint,
  ChartDataSeries,
  ChartDataSeriesSortMode,
  ChartSortMode,
  ValueFormatterOptions,
  ValueFormatterValue,
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

export function lineChartValueFormatter(
  value: ValueFormatterValue,
  options?: Partial<ValueFormatterOptions>
): string {
  if (typeof value !== "number") {
    return String(value) || "";
  }

  return new Intl.NumberFormat("en-GB", {
    notation: "compact",
    compactDisplay: "short",
    style: options?.valueUnit === "currency" ? "currency" : undefined,
    currency: options?.valueUnit === "currency" ? "GBP" : undefined,
  })
    .format(value)
    .toLowerCase();
}

export function statValueFormatter(
  value: ValueFormatterValue,
  options?: Partial<ValueFormatterOptions>
): string {
  if (typeof value !== "number") {
    return String(value) || "";
  }

  return new Intl.NumberFormat("en-GB", {
    notation: options?.compact ? "compact" : undefined,
    compactDisplay: options?.compact ? "short" : undefined,
    style: options?.valueUnit === "currency" ? "currency" : undefined,
    currency: options?.valueUnit === "currency" ? "GBP" : undefined,
    currencyDisplay: options?.currencyAsName ? "name" : "symbol",
    maximumFractionDigits: options?.compact ? undefined : 0,
  })
    .format(value)
    .toLowerCase();
}
