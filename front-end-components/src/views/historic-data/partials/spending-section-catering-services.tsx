import React, { useContext } from "react";
import { LineChart } from "src/components/charts/line-chart";
import { shortValueFormatter } from "src/components/charts/utils.ts";
import { LineChartTooltip } from "src/components/charts/line-chart-tooltip";
import { ResolvedStat } from "src/components/charts/resolved-stat";
import { ChartDimensionContext } from "src/contexts";
import { Expenditure } from "src/services";
import { Loading } from "src/components/loading";

export const SpendingSectionCateringServices: React.FC<{
  data: Expenditure[];
}> = ({ data }) => {
  const dimension = useContext(ChartDimensionContext);

  return (
    <>
      {data.length > 0 ? (
        <>
          <h3 className="govuk-heading-s">Total gross catering costs</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Total gross catering costs"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    totalGrossCateringCosts: {
                      label: "Total gross catering costs",
                      visible: true,
                    },
                  }}
                  seriesLabel={dimension.label}
                  seriesLabelField="yearEnd"
                  valueFormatter={shortValueFormatter}
                  valueUnit={dimension.unit}
                  tooltip={(t) => (
                    <LineChartTooltip
                      {...t}
                      valueFormatter={(v) =>
                        shortValueFormatter(v, { valueUnit: dimension.unit })
                      }
                    />
                  )}
                />
              </div>
            </div>
            <aside className="govuk-grid-column-one-quarter">
              <ResolvedStat
                chartName="Most recent total gross catering costs"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="totalGrossCateringCosts"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">Catering staff costs</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Catering staff costs"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    cateringStaffCosts: {
                      label: "Catering staff costs",
                      visible: true,
                    },
                  }}
                  seriesLabel={dimension.label}
                  seriesLabelField="yearEnd"
                  valueFormatter={shortValueFormatter}
                  valueUnit={dimension.unit}
                  tooltip={(t) => (
                    <LineChartTooltip
                      {...t}
                      valueFormatter={(v) =>
                        shortValueFormatter(v, { valueUnit: dimension.unit })
                      }
                    />
                  )}
                />
              </div>
            </div>
            <aside className="govuk-grid-column-one-quarter">
              <ResolvedStat
                chartName="Most recent catering staff costs"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="cateringStaffCosts"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">Catering supplies costs</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Supply teaching staff"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    cateringSuppliesCosts: {
                      label: "Catering supplies costs",
                      visible: true,
                    },
                  }}
                  seriesLabel={dimension.label}
                  seriesLabelField="yearEnd"
                  valueFormatter={shortValueFormatter}
                  valueUnit={dimension.unit}
                  tooltip={(t) => (
                    <LineChartTooltip
                      {...t}
                      valueFormatter={(v) =>
                        shortValueFormatter(v, { valueUnit: dimension.unit })
                      }
                    />
                  )}
                />
              </div>
            </div>
            <aside className="govuk-grid-column-one-quarter">
              <ResolvedStat
                chartName="Most recent catering supplies costs"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="cateringSuppliesCosts"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
        </>
      ) : (
        <Loading />
      )}
    </>
  );
};
