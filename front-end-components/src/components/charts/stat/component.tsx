import { StatProps } from "src/components/charts/stat";
import {
  ChartDataSeries,
  ChartSeriesValue,
  ChartSeriesValueUnit,
} from "src/components";
import classNames from "classnames";

export function Stat<TData extends ChartDataSeries>(props: StatProps<TData>) {
  const {
    chartName,
    className,
    compactValue,
    label,
    suffix,
    value,
    valueSuffix,
    valueUnit,
  } = props;

  return (
    <div
      className={classNames(className, "chart-stat-wrapper")}
      aria-label={chartName}
      role="note"
    >
      <div className="chart-stat-label">
        <span className="govuk-body-s govuk-!-font-weight-bold">{label}</span>
      </div>
      <div className="chart-stat-value govuk-body-l govuk-!-font-size-36">
        {compactValue ? (
          <abbr
            className="summary"
            title={valueFormatter(value, valueUnit)}
            aria-label={valueFormatter(value, valueUnit, false, true)}
          >
            {valueFormatter(value, valueUnit, true)}
          </abbr>
        ) : (
          <span>{valueFormatter(value, valueUnit)}</span>
        )}
        {valueSuffix && (
          <span className="chart-stat-value-suffix">{valueSuffix}</span>
        )}
      </div>
      {suffix && (
        <div className="chart-stat-suffix">
          <span className="govuk-body-s">{suffix}</span>
        </div>
      )}
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
