import { useContext, useMemo } from "react";
import { ChartModeChart } from "src/components";
import {
  HistoricChart2Props,
  SchoolHistoryValue,
} from "src/composed/historic-chart-2-composed";
import { LineChart } from "src/components/charts/line-chart";
import {
  shortValueFormatter,
  fullValueFormatter,
  statValueFormatter,
} from "src/components/charts/utils.ts";
import { ChartProps } from "src/components/charts/types";
import { ChartDimensionContext, useChartModeContext } from "src/contexts";
import { HistoryBase } from "src/services";
import { HistoricDataTooltip } from "src/components/charts/historic-data-tooltip";
import { ResolvedStat } from "src/components/charts/resolved-stat";

export function HistoricChart2<TData extends HistoryBase>({
  axisLabel,
  chartName,
  children,
  columnHeading,
  data,
  legend,
  legendHorizontalAlign,
  legendIconSize,
  legendIconType,
  legendVerticalAlign,
  perUnitDimension,
  valueField,
  valueUnit,
}: HistoricChart2Props<TData>) {
  const { chartMode } = useChartModeContext();
  const dimension = useContext(ChartDimensionContext);

  const mergedData = useMemo(() => {
    const result: SchoolHistoryValue[] = [];

    data.school?.forEach((s) => {
      const year = s.year;
      const schoolValue = s[valueField] as number;
      const comparatorSetAverage = data?.comparatorSetAverage?.find(
        (c) => c.year == year
      );
      const comparatorSetAverageValue =
        comparatorSetAverage && (comparatorSetAverage[valueField] as number);
      const nationalAverage = data?.nationalAverage?.find(
        (c) => c.year == year
      );
      const nationalAverageValue =
        nationalAverage && (nationalAverage[valueField] as number);

      result.push({
        year,
        term: s.term,
        school:
          schoolValue === undefined ||
          schoolValue === null ||
          isNaN(schoolValue)
            ? undefined
            : schoolValue,
        comparatorSetAverage:
          comparatorSetAverageValue === undefined ||
          comparatorSetAverageValue === null ||
          isNaN(comparatorSetAverageValue)
            ? undefined
            : comparatorSetAverageValue,
        nationalAverage:
          nationalAverageValue === undefined ||
          nationalAverageValue === null ||
          isNaN(nationalAverageValue)
            ? undefined
            : nationalAverageValue,
      });
    });

    return result;
  }, [data, valueField]);

  const seriesConfig: ChartProps<SchoolHistoryValue>["seriesConfig"] = {
    nationalAverage: {
      label: "national average across phase type",
      visible: true,
    },
    comparatorSetAverage: {
      label: "average across comparator set",
      visible: true,
    },
    school: {
      label:
        axisLabel ??
        (dimension.value === "PerUnit"
          ? perUnitDimension.label
          : dimension.label),
      visible: true,
    },
  };

  return (
    <>
      {children}
      {chartMode == ChartModeChart ? (
        <div className="govuk-grid-row">
          <div className="govuk-grid-column-three-quarters">
            <div style={{ height: 200 }}>
              <LineChart
                chartName={chartName}
                className="historic-chart-2"
                data={mergedData}
                grid
                highlightActive
                keyField="term"
                margin={20}
                seriesConfig={seriesConfig}
                seriesLabelField="term"
                valueFormatter={shortValueFormatter}
                valueUnit={valueUnit ?? dimension.unit}
                tooltip={(t) => (
                  <HistoricDataTooltip
                    {...t}
                    valueFormatter={(v) =>
                      shortValueFormatter(v, {
                        valueUnit: valueUnit ?? dimension.unit,
                      })
                    }
                    dimension={columnHeading ?? dimension.heading}
                  />
                )}
                legend={legend === undefined ? true : legend}
                legendIconSize={legendIconSize || 24}
                legendIconType={
                  legend === undefined ? "default" : legendIconType
                }
                legendHorizontalAlign={
                  legendHorizontalAlign === undefined
                    ? "center"
                    : legendHorizontalAlign
                }
                legendVerticalAlign={
                  legendVerticalAlign === undefined
                    ? "bottom"
                    : legendVerticalAlign
                }
              />
            </div>
          </div>
          <aside className="govuk-grid-column-one-quarter">
            <ResolvedStat
              chartName={`Most recent ${chartName.toLowerCase()}`}
              className="chart-stat-line-chart"
              compactValue
              data={data.school || []}
              displayIndex={(data.school?.length || 0) - 1}
              seriesLabelField="term"
              valueField={valueField}
              valueFormatter={statValueFormatter}
              valueUnit={valueUnit ?? dimension.unit}
            />
          </aside>
        </div>
      ) : (
        <div className="govuk-grid-row">
          <div className="govuk-grid-column-full">
            <table className="govuk-table">
              <thead className="govuk-table__head">
                <tr className="govuk-table__row">
                  <th className="govuk-table__header govuk-!-width-one-quarter">
                    Year
                  </th>
                  <th className="govuk-table__header govuk-!-width-one-quarter">
                    {columnHeading ?? dimension.heading}
                  </th>
                  <th className="govuk-table__header govuk-!-width-one-quarter">
                    Average across comparator set
                  </th>
                  <th className="govuk-table__header govuk-!-width-one-quarter">
                    National average across phase type
                  </th>
                </tr>
              </thead>
              <tbody className="govuk-table__body">
                {data.school?.map((item) => {
                  const year = item.year;
                  const schoolValue = item[valueField] as number;
                  const comparatorSetAverage = data.comparatorSetAverage?.find(
                    (c) => c.year == year
                  );
                  const comparatorSetAverageValue = comparatorSetAverage
                    ? (comparatorSetAverage[valueField] as number)
                    : undefined;
                  const nationalAverage = data.nationalAverage?.find(
                    (c) => c.year == year
                  );
                  const nationalAverageValue = nationalAverage
                    ? (nationalAverage[valueField] as number)
                    : undefined;

                  return (
                    <tr className="govuk-table__row" key={year}>
                      <td className="govuk-table__cell">{String(item.term)}</td>
                      <td className="govuk-table__cell">
                        {fullValueFormatter(
                          schoolValue === undefined ||
                            schoolValue === null ||
                            isNaN(schoolValue)
                            ? undefined
                            : schoolValue,
                          {
                            valueUnit: valueUnit ?? dimension.unit,
                          }
                        )}
                      </td>
                      <td className="govuk-table__cell">
                        {fullValueFormatter(
                          comparatorSetAverageValue === undefined ||
                            comparatorSetAverageValue === null ||
                            isNaN(comparatorSetAverageValue)
                            ? undefined
                            : comparatorSetAverageValue,
                          {
                            valueUnit: valueUnit ?? dimension.unit,
                          }
                        )}
                      </td>
                      <td className="govuk-table__cell">
                        {fullValueFormatter(
                          nationalAverageValue === undefined ||
                            nationalAverageValue === null ||
                            isNaN(nationalAverageValue)
                            ? undefined
                            : nationalAverageValue,
                          {
                            valueUnit: valueUnit ?? dimension.unit,
                          }
                        )}
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        </div>
      )}
    </>
  );
}
