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
} from "src/components/charts/utils.ts";
import { ChartProps } from "src/components/charts/types";
import { ChartDimensionContext, useChartModeContext } from "src/contexts";
import { SchoolHistoryBase } from "src/services";
import { HistoricDataTooltip } from "src/components/charts/historic-data-tooltip";

export function HistoricChart2<TData extends SchoolHistoryBase>({
  chartName,
  data,
  valueField,
  children,
  valueUnit,
  axisLabel,
  columnHeading,
  ...props
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
    school: {
      label: dimension.label,
      visible: true,
    },
    comparatorSetAverage: {
      label: "average across comparator set",
      visible: true,
    },
    nationalAverage: {
      label: "national average across phase type",
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
                seriesLabel={axisLabel ?? dimension.label}
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
                    dimension={dimension.heading}
                  />
                )}
                {...props}
              />
            </div>
          </div>
          <aside className="govuk-grid-column-one-quarter"></aside>
        </div>
      ) : (
        <div className="govuk-grid-row">
          <div className="govuk-grid-column-three-quarters">
            <table className="govuk-table">
              <thead className="govuk-table__head">
                <tr className="govuk-table__row">
                  <th className="govuk-table__header">Year</th>
                  <th className="govuk-table__header">
                    {columnHeading ?? dimension.heading}
                  </th>
                  <th className="govuk-table__header">
                    Average across comparator set
                  </th>
                  <th className="govuk-table__header">
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
