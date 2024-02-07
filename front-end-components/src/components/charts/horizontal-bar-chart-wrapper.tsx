import React, { createRef, useContext, useMemo } from "react";
import {
  DownloadHandle,
  HorizontalBarChart,
} from "src/components/charts/horizontal-bar-chart";
import { TableChart } from "src/components/charts/table-chart";
import { ChartModeContext } from "src/contexts";
import { Loading } from "src/components/loading";
import {
  ChartSortMode,
  HorizontalBarChartWrapperProps,
} from "src/components/charts";
import { ChartModeChart, ChartModeTable } from "src/components";
import { chartComparer } from "./utils";

const defaultSort: ChartSortMode = {
  direction: "desc",
  dataPoint: "value",
};

export const HorizontalBarChartWrapper: React.FC<
  HorizontalBarChartWrapperProps
> = (props) => {
  const { data, children, chartName, sort } = props;
  const mode = useContext(ChartModeContext);
  const ref = createRef<DownloadHandle>();

  // if a `sort` is not provided, the default sorting method will be used (value DESC)
  const sortedDataPoints = useMemo(() => {
    return data.dataPoints.sort((a, b) =>
      chartComparer(a, b, sort ?? defaultSort)
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
              >
                <HorizontalBarChart
                  chartName={chartName}
                  data={sortedDataPoints}
                  ref={ref}
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
};
