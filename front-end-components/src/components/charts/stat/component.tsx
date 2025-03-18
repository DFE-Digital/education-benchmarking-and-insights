import { StatProps } from "src/components/charts/stat";
import { ChartDataSeries } from "src/components";
import classNames from "classnames";
import "./styles.scss";

export function Stat<TData extends ChartDataSeries>({
  chartTitle,
  className,
  compactValue,
  label,
  small,
  suffix,
  value,
  valueFormatter,
  valueSuffix,
  valueUnit,
}: StatProps<TData>) {
  return (
    <div
      className={classNames(className, "chart-stat-wrapper", {
        "chart-stat-wrapper__small": small,
      })}
      aria-label={chartTitle}
      role="note"
    >
      <div className="chart-stat-label">{label}</div>
      <div className="chart-stat-value">
        {compactValue ? (
          <span
            aria-label={
              valueFormatter
                ? valueFormatter(value, { valueUnit, currencyAsName: true })
                : String(value)
            }
          >
            {valueFormatter
              ? valueFormatter(value, { valueUnit, compact: true })
              : String(value)}
          </span>
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
