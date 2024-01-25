import React, { createRef, useContext } from "react";
import {
  DownloadHandle,
  HorizontalBarChart,
} from "src/components/charts/horizontal-bar-chart";
import { TableChart } from "src/components/charts/table-chart";
import { ChartModeContext } from "src/contexts";
import { Loading } from "src/components/loading";
import { HorizontalBarChartWrapperProps } from "src/components/charts";
import { ChartModeChart, ChartModeTable } from "src/components";

export const HorizontalBarChartWrapper: React.FC<
  HorizontalBarChartWrapperProps
> = (props) => {
  const { data, children, chartId } = props;
  const mode = useContext(ChartModeContext);
  const ref = createRef<DownloadHandle>();

  const renderView = (displayMode: string) => {
    switch (displayMode) {
      case ChartModeChart:
        return (
          <HorizontalBarChart
            chartId={chartId}
            data={data.dataPoints}
            ref={ref}
          />
        );
      case ChartModeTable:
        return (
          <TableChart
            tableHeadings={data.tableHeadings}
            data={data.dataPoints}
          />
        );
      default:
        return <Loading />;
    }
  };

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
          {data.dataPoints.length > 0 ? renderView(mode) : <Loading />}
        </div>
      </div>
    </>
  );
};
