import React, { useContext } from "react";
import { LineChart } from "src/components/charts/line-chart";
import { shortValueFormatter } from "src/components/charts/utils.ts";
import { LineChartTooltip } from "src/components/charts/line-chart-tooltip";
import { ResolvedStat } from "src/components/charts/resolved-stat";
import { ChartDimensionContext } from "src/contexts";
import { Income } from "src/services";
import { Loading } from "src/components/loading";

export const IncomeSectionDirectRevenue: React.FC<{ data: Income[] }> = ({
  data,
}) => {
  const dimension = useContext(ChartDimensionContext);

  return (
    <>
      {data.length > 0 ? (
        <>
          <h3 className="govuk-heading-s">
            Direct revenue financing (capital reserves transfers)
          </h3>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
              <div style={{ height: 200 }}>
                <LineChart
                  chartName="Direct revenue financing (capital reserves transfers)"
                  data={data}
                  grid
                  highlightActive
                  keyField="yearEnd"
                  margin={20}
                  seriesConfig={{
                    directRevenueFinancing: {
                      label: "Direct revenue financing",
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
                chartName="Most recent direct revenue financing"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="directRevenueFinancing"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <details className="govuk-details">
            <summary className="govuk-details__summary">
              <span className="govuk-details__summary-text">
                More about direct revenue financing
              </span>
            </summary>
            <div className="govuk-details__text">
              <p>This includes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>
                  all amounts transferred to CI04 to be accumulated to fund
                  capital works (it may include receipts from insurance claims
                  for capital losses received into income under I11)
                </li>
                <li>
                  any amount transferred to a local authority reserve to part
                  fund a capital scheme delivered by the local authority
                </li>
                <li>
                  any repayment of principal on a capital loan from the local
                  authority
                </li>
              </ul>
              <p>It excludes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>funds specifically provided for capital purposes</li>
              </ul>
            </div>
          </details>
        </>
      ) : (
        <Loading />
      )}
    </>
  );
};
