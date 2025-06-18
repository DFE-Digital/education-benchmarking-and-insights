import React from "react";
import { useChartModeContext } from "src/contexts";
import { HorizontalBarChartWrapper } from "src/composed/horizontal-bar-chart-wrapper";
import { ChartMode } from "src/components";
import { ErrorBanner } from "src/components/error-banner";
import { LaNationalRankChartProps } from "./types";

export const LaNationalRankChart: React.FC<LaNationalRankChartProps> = ({
  title,
  summary,
  prefix,
  valueLabel,
  chartData,
  notInRanking,
}) => {
  const { chartMode, setChartMode } = useChartModeContext();

  return (
    <>
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-full">
          <p className="govuk-body">{summary}</p>
        </div>
      </div>
      <div>
        <HorizontalBarChartWrapper
          chartTitle={title}
          data={chartData}
          localAuthority
          showCopyImageButton
          valueUnit="%"
          xAxisLabel={valueLabel}
        >
          <ChartMode
            chartMode={chartMode}
            handleChange={setChartMode}
            prefix={prefix}
          />
          {notInRanking && (
            <ErrorBanner
              isRendered
              message="There isn't enough information available to rank the current local authority."
            />
          )}
        </HorizontalBarChartWrapper>
      </div>
    </>
  );
};
