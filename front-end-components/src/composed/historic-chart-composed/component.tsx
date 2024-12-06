import { useContext } from "react";
import { ChartModeChart } from "src/components";
import { HistoricChartProps } from "src/composed/historic-chart-composed";
import { LineChart } from "src/components/charts/line-chart";
import {
  shortValueFormatter,
  statValueFormatter,
  fullValueFormatter,
} from "src/components/charts/utils.ts";
import { LineChartTooltip } from "src/components/charts/line-chart-tooltip";
import { ResolvedStat } from "src/components/charts/resolved-stat";
import { ChartDataSeries } from "src/components/charts/types";
import { ChartDimensionContext, useChartModeContext } from "src/contexts";

export function HistoricChart<TData extends ChartDataSeries>({
  chartName,
  data,
  seriesConfig,
  valueField,
  children,
  valueUnit,
  axisLabel,
  columnHeading,
}: HistoricChartProps<TData>) {
  const { chartMode } = useChartModeContext();
  const dimension = useContext(ChartDimensionContext);

  return (
    <>
      {children}
      {chartMode == ChartModeChart ? (
        <div className="govuk-grid-row">
          <div className="govuk-grid-column-three-quarters">
            <div style={{ height: 200 }}>
              <LineChart
                chartName={chartName}
                data={data}
                grid
                highlightActive
                keyField="term"
                margin={20}
                seriesConfig={seriesConfig}
                seriesLabel={axisLabel ?? dimension.label}
                seriesLabelField="term"
                valueFormatter={shortValueFormatter}
                valueUnit={valueUnit ?? dimension.unit}
                tooltip={(t) => (
                  <LineChartTooltip
                    {...t}
                    valueFormatter={(v) =>
                      shortValueFormatter(v, {
                        valueUnit: valueUnit ?? dimension.unit,
                      })
                    }
                  />
                )}
              />
            </div>
          </div>
          <aside className="govuk-grid-column-one-quarter">
            <ResolvedStat
              chartName={`Most recent ${chartName.toLowerCase()}`}
              className="chart-stat-line-chart"
              compactValue
              data={data}
              displayIndex={data.length - 1}
              seriesLabelField="term"
              valueField={valueField}
              valueFormatter={statValueFormatter}
              valueUnit={valueUnit ?? dimension.unit}
            />
          </aside>
        </div>
      ) : (
        <div className="govuk-grid-row">
          <div className="govuk-grid-column-three-quarters">
            <table className="govuk-table">
              <thead className="govuk-table__head">
                <tr className="govuk-table__row">
                  <th className="govuk-table__header govuk-!-width-one-half">
                    Year
                  </th>
                  <th className="govuk-table__header">
                    {columnHeading ?? dimension.heading}
                  </th>
                </tr>
              </thead>
              <tbody className="govuk-table__body">
                {data.map((item) => (
                  <tr className="govuk-table__row" key={item.year as string}>
                    <td className="govuk-table__cell">{String(item.term)}</td>
                    <td className="govuk-table__cell">
                      {fullValueFormatter(item[valueField], {
                        valueUnit: valueUnit ?? dimension.unit,
                      })}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      )}
    </>
  );
}
