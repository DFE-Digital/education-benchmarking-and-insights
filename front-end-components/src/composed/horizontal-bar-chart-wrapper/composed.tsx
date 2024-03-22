import { createRef, useContext, useMemo } from "react";
import { HorizontalBarChart } from "src/components/charts/horizontal-bar-chart-2";
import { TableChart, SchoolChartData } from "src/components/charts/table-chart";
import {
  ChartDimensionContext,
  ChartModeContext,
  SelectedSchoolContext,
} from "src/contexts";
import { Loading } from "src/components/loading";
import { ChartHandler } from "src/components/charts";
import { HorizontalBarChartWrapperProps } from "src/composed/horizontal-bar-chart-wrapper";
import { ChartModeChart, ChartModeTable } from "src/components";
import { chartSeriesComparer } from "src/components/charts/utils";
import { SchoolTick } from "src/components/charts/school-tick";
import { SchoolWorkforceTooltip } from "src/components/charts/school-workforce-tooltip";

export function HorizontalBarChartWrapper<TData extends SchoolChartData>(
  props: HorizontalBarChartWrapperProps<TData>
) {
  const { data, children, chartName, sort } = props;
  const mode = useContext(ChartModeContext);
  const dimension = useContext(ChartDimensionContext);
  const selectedSchool = useContext(SelectedSchoolContext);
  const ref = createRef<ChartHandler>();

  // if a `sort` is not provided, the default sorting method will be used (value DESC)
  const sortedDataPoints = useMemo(() => {
    return data.dataPoints.sort((a, b) =>
      chartSeriesComparer(
        a,
        b,
        sort ?? {
          direction: "desc",
          dataPoint: "value",
        }
      )
    );
  }, [data.dataPoints, sort]);

  return (
    <>
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-two-thirds">{children}</div>
        {mode == ChartModeChart && (
          <div className="govuk-grid-column-one-third">
            <button
              className="govuk-button govuk-button--secondary"
              data-module="govuk-button"
              onClick={() => ref.current && ref.current.download()}
            >
              Save as image
            </button>
          </div>
        )}
      </div>
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-full">
          {sortedDataPoints.length > 0 ? (
            <>
              <div
                className={
                  mode == ChartModeChart ? "" : "govuk-visually-hidden"
                }
                style={{ height: 700 }}
              >
                <HorizontalBarChart
                  barCategoryGap={2}
                  chartName={chartName}
                  data={sortedDataPoints}
                  highlightActive
                  highlightedItemKeys={
                    selectedSchool ? [selectedSchool.urn] : undefined
                  }
                  keyField="urn"
                  labels
                  margin={20}
                  ref={ref}
                  seriesConfig={
                    {
                      value: {
                        visible: true,
                        valueFormatter: (value: string) =>
                          value
                            ? parseFloat(value.toString()).toFixed(1)
                            : String(value),
                      },
                    } as object // todo: fix typing issue
                  }
                  seriesLabelField="name"
                  tickWidth={400}
                  tick={(t) => (
                    <SchoolTick
                      {...t}
                      highlightedItemKey={selectedSchool?.urn}
                      linkToSchool
                      onClick={(urn) => {
                        urn && (window.location.href = `/school/${urn}`);
                      }}
                      schoolUrnResolver={(name) =>
                        data.dataPoints.find((d) => d.name === name)?.urn
                      }
                    />
                  )}
                  tooltip={(t) => <SchoolWorkforceTooltip {...t} />}
                  valueLabel={dimension}
                />
              </div>
              <div
                className={
                  mode == ChartModeTable ? "" : "govuk-visually-hidden"
                }
              >
                <TableChart
                  tableHeadings={data.tableHeadings}
                  data={sortedDataPoints}
                />
              </div>
            </>
          ) : (
            <Loading />
          )}
        </div>
      </div>
    </>
  );
}
