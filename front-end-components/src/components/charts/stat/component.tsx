import { StatProps } from "src/components/charts/stat";
import { ChartDataSeries } from "src/components";
import classNames from "classnames";

export function Stat<TData extends ChartDataSeries>(props: StatProps<TData>) {
  const {
    chartName,
    className,
    compactValue,
    label,
    suffix,
    value,
    valueFormatter,
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
            title={
              valueFormatter
                ? valueFormatter(value, { valueUnit })
                : String(value)
            }
            aria-label={
              valueFormatter
                ? valueFormatter(value, { valueUnit, currencyAsName: true })
                : String(value)
            }
          >
            {valueFormatter
              ? valueFormatter(value, { valueUnit, compact: true })
              : String(value)}
          </abbr>
        ) : (
          <span>
            {valueFormatter
              ? valueFormatter(value, { valueUnit })
              : String(value)}
          </span>
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
