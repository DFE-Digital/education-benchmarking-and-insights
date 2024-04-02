import React, { useContext } from "react";
import { LineChart } from "src/components/charts/line-chart";
import { shortValueFormatter } from "src/components/charts/utils.ts";
import { LineChartTooltip } from "src/components/charts/line-chart-tooltip";
import { ResolvedStat } from "src/components/charts/resolved-stat";
import { ChartDimensionContext } from "src/contexts";
import { Income } from "src/services";
import { Loading } from "src/components/loading";

export const IncomeSectionGrantFunding: React.FC<{ data: Income[] }> = ({
  data,
}) => {
  const dimension = useContext(ChartDimensionContext);

  return (
    <>
      {data.length > 0 ? (
        <>
          <h3 className="govuk-heading-s">Grant funding total</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Grant funding total"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    totalGrantFunding: {
                      label: "total grant funding",
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
                chartName="Most recent grant funding total"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="totalGrantFunding"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">Direct grants</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Direct grants"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    directGrants: {
                      label: "Direct grants",
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
                chartName="Most recent direct grants"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="directGrants"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <details className="govuk-details">
            <summary className="govuk-details__summary">
              <span className="govuk-details__summary-text">
                More about direct grants
              </span>
            </summary>
            <div className="govuk-details__text">
              <p>Where there is funding, direct grants include:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>pre-16 funding</li>
                <li>post-16 funding</li>
                <li>
                  Department of Education (DfE)/Education Funding Agency (EFA)
                  revenue grants
                </li>
                <li>other DfE or EFA revenue grants</li>
                <li>
                  other income (local authority and other government grants)
                </li>
                <li>government source (non-grant)</li>
              </ul>
            </div>
          </details>
          <h3 className="govuk-heading-s">Pre-16 and post-16 funding</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Pre-16 and post-16 funding"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    prePost16Funding: {
                      label: "Pre-16 and post-16 funding",
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
                chartName="Most recent pre-16 and post-16 funding"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="prePost16Funding"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">Other DfE/EFA revenue grants </h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Other DfE/EFA revenue grants "
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    otherDfeGrants: {
                      label: "Other DfE/EFA revenue grants ",
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
                chartName="Most recent other DfE/EFA revenue grants "
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="otherDfeGrants"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">
            Other income (local authority and other government grants)
          </h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Other income (local authority and other government grants)l"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    otherIncomeGrants: {
                      label:
                        "Other income (local authority and other government grants)",
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
                chartName="Most recent other income (local authority and other government grants)"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="otherIncomeGrants"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">Government source (non-grant)</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Government source (non-grant)"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    governmentSource: {
                      label: "Government source (non-grant)",
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
                chartName="Most recent government source (non-grant)"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="governmentSource"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">Community grants</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Community grantsl"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    communityGrants: {
                      label: "Community grants",
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
                chartName="Most recent community grants"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="communityGrants"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <h3 className="govuk-heading-s">Academies</h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Academies"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    academies: {
                      label: "Academies",
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
                chartName="Most recent academies "
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="academies"
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
