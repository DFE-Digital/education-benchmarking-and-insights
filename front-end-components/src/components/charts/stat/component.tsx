import { StatProps } from "src/components/charts/stat";
import {
  ChartDataSeries,
  ChartSeriesValue,
  ChartSeriesValueUnit,
} from "src/components";
import { useMemo } from "react";
import classNames from "classnames";

export function Stat<TData extends ChartDataSeries>(props: StatProps<TData>) {
  const {
    chartName,
    className,
    data,
    displayIndex,
    seriesLabelField,
    valueField,
    valueUnit,
  } = props;

  const entry = useMemo(() => {
    const entry = Object.entries(data)[displayIndex];
    if (!entry) {
      return null;
    }

    return { label: entry[1][seriesLabelField], value: entry[1][valueField] };
  }, [data, displayIndex, seriesLabelField, valueField]);

  // do not render anything if a match could not be located based on the available data
  if (!entry) {
    return null;
  }

  return (
    <div
      className={classNames(className, "chart-stat-wrapper")}
      aria-label={chartName}
      role="note"
    >
      <div className="chart-stat-label">
        <span className="govuk-body-s govuk-!-font-weight-bold">
          {entry.label}
        </span>
      </div>
      <div className="chart-stat-value govuk-body-l govuk-!-font-size-36">
        <abbr
          className="summary"
          title={valueFormatter(entry.value, valueUnit)}
          aria-label={valueFormatter(entry.value, valueUnit, false, true)}
        >
          {valueFormatter(entry.value, valueUnit, true)}
        </abbr>
      </div>
    </div>
  );
}

function valueFormatter(
  value: ChartSeriesValue | undefined,
  valueUnit?: ChartSeriesValueUnit,
  compact?: boolean,
  currencyAsName?: boolean
) {
  if (typeof value !== "number") {
    return value || "";
  }

  return new Intl.NumberFormat("en-GB", {
    notation: compact ? "compact" : undefined,
    compactDisplay: compact ? "short" : undefined,
    style: valueUnit === "currency" ? "currency" : undefined,
    currency: valueUnit === "currency" ? "GBP" : undefined,
    currencyDisplay: currencyAsName ? "name" : "symbol",
    maximumFractionDigits: compact ? undefined : 0,
  })
    .format(value)
    .toLowerCase();
}
