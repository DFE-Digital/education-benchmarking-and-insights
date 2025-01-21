import React from "react";
import { CostCategories, ChartDimensions } from "src/components";
import { ChartDimensionContext } from "src/contexts";
import { HorizontalBarChartWrapper } from "src/composed/horizontal-bar-chart-wrapper";
import { ErrorBanner } from "src/components/error-banner";
import {
  SchoolChartData,
  TrustChartData,
} from "src/components/charts/table-chart";
import { SectionProps } from "./types";

export function Section<TData extends SchoolChartData | TrustChartData>({
  charts,
  dimension,
  dimensions,
  handleDimensionChange,
  hasNoData,
  options,
  topLevel,
}: SectionProps<TData>) {
  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    if (handleDimensionChange) {
      handleDimensionChange(event.target.value);
    }
  };

  return hasNoData ? (
    <ErrorBanner
      isRendered={hasNoData}
      message="There isn't enough information available to create a set of similar schools."
    />
  ) : (
    charts.map(({ title, data }, i) => {
      const chartName = title.toLowerCase().replace(/\W/g, " ").trim();
      const chartId = chartName.replace(/\s/g, "-");

      return (
        <ChartDimensionContext.Provider key={chartId} value={dimension}>
          <HorizontalBarChartWrapper
            chartName={chartName}
            chartTitle={title}
            data={data}
          >
            {topLevel ? (
              <h2 className="govuk-heading-m">{title}</h2>
            ) : (
              <h3 className="govuk-heading-s">{title}</h3>
            )}
            {i === 0 &&
              (options ?? (
                <ChartDimensions
                  dimensions={dimensions || CostCategories}
                  elementId={chartId}
                  handleChange={handleSelectChange}
                  value={dimension.value}
                />
              ))}
          </HorizontalBarChartWrapper>
        </ChartDimensionContext.Provider>
      );
    })
  );
}
