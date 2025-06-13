import React from "react";
import { CostCategories, ChartDimensions } from "src/components";
import { ChartDimensionContext } from "src/contexts";
import { HorizontalBarChartWrapper } from "src/composed/horizontal-bar-chart-wrapper";
import { ErrorBanner } from "src/components/error-banner";
import {
  SchoolChartData,
  TrustChartData,
} from "src/components/charts/table-chart";
import { DimensionedChartProps } from "./types";

export function DimensionedChart<
  TData extends SchoolChartData | TrustChartData,
>({
  charts,
  dimension,
  dimensions,
  handleDimensionChange,
  hasNoData,
  options,
  topLevel,
  ...props
}: DimensionedChartProps<TData>) {
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
    charts.map(({ data, selector, title, override, ...chart }, i) => {
      const chartId = title
        .toLowerCase()
        .replace(/\W/g, " ")
        .replace(/\s{2,}/g, " ")
        .trim()
        .replace(/\s/g, "-");

      return (
        <ChartDimensionContext.Provider key={chartId} value={dimension}>
          <HorizontalBarChartWrapper
            chartTitle={title}
            data={data}
            linkToEstablishment
            tooltip
            override={override}
            {...props}
          >
            {topLevel ? (
              <h2 className="govuk-heading-m">{title}</h2>
            ) : (
              <h3 className="govuk-heading-s">{title}</h3>
            )}
            {(i === 0 || selector) &&
              (options ?? (
                <ChartDimensions
                  dimensions={chart.dimensions || dimensions || CostCategories}
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
