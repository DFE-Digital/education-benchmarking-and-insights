import {
  ChartDataSeries,
  ChartDataSeriesSortMode,
  ValueFormatterOptions,
  ValueFormatterValue,
} from ".";

export function chartSeriesComparer<TData extends ChartDataSeries>(
  a: TData,
  b: TData,
  { dataPoint, direction }: ChartDataSeriesSortMode<TData>
): number {
  if (a[dataPoint] < b[dataPoint]) {
    return direction === "asc" ? -1 : 1;
  }

  if (a[dataPoint] > b[dataPoint]) {
    return direction === "asc" ? 1 : -1;
  }

  return 0;
}

export function shortValueFormatter(
  value: ValueFormatterValue,
  options?: Partial<ValueFormatterOptions>
): string {
  if (typeof value !== "number") {
    return String(value) || "";
  }

  return new Intl.NumberFormat("en-GB", {
    notation: options?.valueUnit === "%" ? undefined : "compact",
    compactDisplay: options?.valueUnit === "%" ? undefined : "short",
    style:
      options?.valueUnit === "currency"
        ? "currency"
        : options?.valueUnit === "%"
          ? "percent"
          : undefined,
    currency: options?.valueUnit === "currency" ? "GBP" : undefined,
    maximumFractionDigits:
      options?.valueUnit === "currency"
        ? value > 1000
          ? 1
          : 0
        : options?.valueUnit === "%"
          ? 1
          : 2,
    minimumFractionDigits: 0,
  })
    .format(options?.valueUnit === "%" ? value / 100 : value)
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
    style:
      options?.valueUnit === "currency"
        ? "currency"
        : options?.valueUnit === "%"
          ? "percent"
          : undefined,
    currency: options?.valueUnit === "currency" ? "GBP" : undefined,
    currencyDisplay: options?.currencyAsName ? "name" : "symbol",
    maximumFractionDigits:
      options?.valueUnit === "%"
        ? 1
        : options?.compact
          ? options?.valueUnit === "currency"
            ? value > 1000
              ? 1
              : 0
            : undefined
          : 0,
    minimumFractionDigits:
      options?.compact && options?.valueUnit === "currency" ? 0 : undefined,
  })
    .format(options?.valueUnit === "%" ? value / 100 : value)
    .toLowerCase();
}

export function fullValueFormatter(
  value: ValueFormatterValue,
  options?: Partial<ValueFormatterOptions>
): string {
  if (typeof value !== "number") {
    return value ? String(value) : "";
  }

  return new Intl.NumberFormat("en-GB", {
    style:
      options?.valueUnit === "currency"
        ? "currency"
        : options?.valueUnit === "%"
          ? "percent"
          : undefined,
    currency: options?.valueUnit === "currency" ? "GBP" : undefined,
    maximumFractionDigits:
      options?.valueUnit === "currency"
        ? 0
        : options?.valueUnit === "%"
          ? 1
          : 2,
  })
    .format(options?.valueUnit === "%" ? value / 100 : value)
    .toLowerCase();
}

export function payBandFormatter(
  value: ValueFormatterValue,
  options?: Partial<ValueFormatterOptions>
): string {
  if (
    typeof value !== "number" ||
    (options?.forDisplay === true && value == 0)
  ) {
    return "No data available";
  }

  if (value > 380) {
    return "£380k+";
  }
  if (value <= 10) {
    return "£0-£10k";
  }
  return `£${value - 10}k-£${value}k`;
}
