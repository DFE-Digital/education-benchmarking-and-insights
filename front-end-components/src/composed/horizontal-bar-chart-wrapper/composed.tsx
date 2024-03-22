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
import {
  chartSeriesComparer,
  shortValueFormatter,
} from "src/components/charts/utils";
import { SchoolTick } from "src/components/charts/school-tick";
import { SchoolWorkforceTooltip } from "src/components/charts/school-workforce-tooltip";

export function HorizontalBarChartWrapper<TData extends SchoolChartData>(
  props: HorizontalBarChartWrapperProps<TData>
) {
  const { chartName, children, data, sort, valueUnit } = props;
  const mode = useContext(ChartModeContext);
  const dimension = useContext(ChartDimensionContext);
  const selectedSchool = useContext(SelectedSchoolContext);
  const ref = createRef<ChartHandler>();

  // if a `sort` is not provided, the default sorting method will be used (value DESC)
  const sortedDataPoints = useMemo(() => {
    return data.dataPoints
      .map((d) =>
        isFinite(d.value) && !isNaN(d.value) ? d : { ...d, value: 0 }
      )
      .sort((a, b) =>
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
              {mode == ChartModeChart && (
                <div style={{ height: 700 }}>
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
                          valueFormatter: (v: number) =>
                            shortValueFormatter(v, { valueUnit }),
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
                    valueFormatter={shortValueFormatter}
                    valueLabel={dimension}
                    valueUnit={valueUnit}
                  />
                </div>
              )}
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
