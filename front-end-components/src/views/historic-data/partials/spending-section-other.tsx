import React, { useContext } from "react";
import { LineChart } from "src/components/charts/line-chart";
import { shortValueFormatter } from "src/components/charts/utils.ts";
import { LineChartTooltip } from "src/components/charts/line-chart-tooltip";
import { ResolvedStat } from "src/components/charts/resolved-stat";
import { ChartDimensionContext } from "src/contexts";
import { Expenditure } from "src/services";
import { Loading } from "src/components/loading";

export const SpendingSectionOther: React.FC<{
  data: Expenditure[];
}> = ({ data }) => {
  const dimension = useContext(ChartDimensionContext);

  return (
    <>
      {data.length > 0 ? (
        <>
          <h3 className="govuk-heading-s">Total other costs</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Total other costs"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    totalOtherCosts: {
                      label: "Total other costs",
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
                chartName="Most recent total other costs"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="totalOtherCosts"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">Direct revenue financing costs</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Direct revenue financing costs"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    directRevenueFinancingCosts: {
                      label: "Direct revenue financing costs",
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
                chartName="Most recent direct revenue financing costs"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="directRevenueFinancingCosts"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">Grounds maintenance costs</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Grounds maintenance costs"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    groundsMaintenanceCosts: {
                      label: "Grounds maintenance costs",
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
                chartName="Most recent grounds maintenance costs"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="groundsMaintenanceCosts"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">Indirect employee expenses costs</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Indirect employee expenses costs"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    indirectEmployeeExpenses: {
                      label: "Indirect employee expenses costs",
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
                chartName="Most recent indirect employee expenses costs"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="indirectEmployeeExpenses"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">
            Interest changes for loan and bank costs
          </h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Interest changes for loan and bank costs"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    interestChargesLoanBank: {
                      label: "Interest changes for loan and bank costs",
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
                chartName="Most recent interest changes for loan and bank costs"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="interestChargesLoanBank"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">Other insurance premiums costs</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Other insurance premiums costsf"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    otherInsurancePremiumsCosts: {
                      label: "Other insurance premiums costs",
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
                chartName="Most recent other insurance premiums costs"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="otherInsurancePremiumsCosts"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">PFI charges costs</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="PFI charges costs"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    privateFinanceInitiativeCharges: {
                      label: "PFI charges costs",
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
                chartName="Most recent PFI charges costs"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="privateFinanceInitiativeCharges"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">Rents and rates costs</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Rents and rates costs"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    rentRatesCosts: {
                      label: "Rents and rates costs",
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
                chartName="Most recent rents and rates costs"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="rentRatesCosts"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">Special facilities costs</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Special facilities costs"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    specialFacilitiesCosts: {
                      label: "Special facilities costs",
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
                chartName="Most recent special facilities costss"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="specialFacilitiesCosts"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">
            Staff development and training costs
          </h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Staff development and training costss"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    staffDevelopmentTrainingCosts: {
                      label: "Staff development and training costs",
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
                chartName="Most recent staff development and training costs"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="staffDevelopmentTrainingCosts"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">Staff-related insurance costs</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Staff-related insurance costs"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    staffRelatedInsuranceCosts: {
                      label: "Staff-related insurance costs",
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
                chartName="Most recent staff-related insurance costs"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="staffRelatedInsuranceCosts"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">Supply teacher insurance costs</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Supply teacher insurance costss"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    supplyTeacherInsurableCosts: {
                      label: "Supply teacher insurance costs",
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
                chartName="Most recent supply teacher insurance costs"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="supplyTeacherInsurableCosts"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">
            Community focused school staff (maintained schools only)
          </h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Community focused school staff (maintained schools only)"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    communityFocusedSchoolStaff: {
                      label:
                        "Community focused school staff (maintained schools only)",
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
                chartName="Most recent community focused school staff (maintained schools only)"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="communityFocusedSchoolStaff"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">
            Community focused school costs (maintained schools only)
          </h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Community focused school costs (maintained schools only)"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    communityFocusedSchoolCosts: {
                      label:
                        "Community focused school costs (maintained schools only)",
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
                chartName="Most recent community focused school costs (maintained schools only)"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="communityFocusedSchoolCosts"
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
