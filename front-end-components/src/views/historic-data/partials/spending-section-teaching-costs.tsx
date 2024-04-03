import React, { useContext } from "react";
import { LineChart } from "src/components/charts/line-chart";
import { shortValueFormatter } from "src/components/charts/utils.ts";
import { LineChartTooltip } from "src/components/charts/line-chart-tooltip";
import { ResolvedStat } from "src/components/charts/resolved-stat";
import { ChartDimensionContext } from "src/contexts";
import { Expenditure } from "src/services";
import { Loading } from "src/components/loading";

export const SpendingSectionTeachingCosts: React.FC<{
  data: Expenditure[];
}> = ({ data }) => {
  const dimension = useContext(ChartDimensionContext);

  return (
    <>
      {data.length > 0 ? (
        <>
          <h3 className="govuk-heading-s">
            Total Teaching and teaching support staff costs{" "}
          </h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Total Teaching and teaching support staff costs"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    totalTeachingSupportStaffCosts: {
                      label: "Total Teaching and teaching support staff costs",
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
                chartName="Most recent total Teaching and teaching support staff costs"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="totalTeachingSupportStaffCosts"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">Teaching staff costs</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Teaching staff costs"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    teachingStaffCosts: {
                      label: "Teaching staff costs",
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
                chartName="Most recent teaching staff costs"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="teachingStaffCosts"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">Supply teaching staff</h3>
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
                    supplyTeachingStaffCosts: {
                      label: "Supply teaching staff",
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
                chartName="Most recent supply teaching staff"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="supplyTeachingStaffCosts"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">Educational consultancy</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Educational consultancy"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    educationalConsultancyCosts: {
                      label: "Educational consultancy",
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
                chartName="Most recent educational consultancy"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="educationalConsultancyCosts"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">Education support staff</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Education support staff"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    educationSupportStaffCosts: {
                      label: "Education support staff",
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
                chartName="Most recent education support staff"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="educationSupportStaffCosts"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">Agency supply teaching staff</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Agency supply teaching staff"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    agencySupplyTeachingStaffCosts: {
                      label: "Agency supply teaching staff",
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
                chartName="Most recent agency supply teaching staff"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="agencySupplyTeachingStaffCosts"
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
