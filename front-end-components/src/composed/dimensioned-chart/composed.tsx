import { useContext } from "react";
import {
  CostCategories,
  ChartDimensions,
  ProgressBanding,
} from "src/components";
import {
  ChartDimensionContext,
  SuppressNegativeOrZeroContext,
} from "src/contexts";
import { HorizontalBarChartWrapper } from "src/composed/horizontal-bar-chart-wrapper";
import { ErrorBanner } from "src/components/error-banner";
import {
  SchoolChartData,
  TrustChartData,
} from "src/components/charts/table-chart";
import { DimensionedChartProps } from "./types";
import { CostCodesList } from "src/components/cost-codes-list";

export function DimensionedChart<
  TData extends SchoolChartData | TrustChartData,
>({
  charts,
  costCodesUnderTitle,
  dimension,
  dimensions,
  handleDimensionChange,
  hasNoData,
  options,
  progressIndicators,
  topLevel,
  ...props
}: DimensionedChartProps<TData>) {
  const { suppressNegativeOrZero, message } = useContext(
    SuppressNegativeOrZeroContext
  );

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
          <SuppressNegativeOrZeroContext.Provider
            value={{
              suppressNegativeOrZero:
                override?.suppressNegativeOrZero?.suppressNegativeOrZero ??
                suppressNegativeOrZero,
              message: override?.suppressNegativeOrZero?.message ?? message,
            }}
          >
            <HorizontalBarChartWrapper
              chartTitle={title}
              costCodesUnderTitle={costCodesUnderTitle}
              data={data}
              linkToEstablishment
              tooltip
              override={override}
              progressAboveAverageKeys={
                progressIndicators
                  ? Object.entries(progressIndicators)
                      .filter((e) => e[1] === ProgressBanding.AboveAverage)
                      .map((e) => e[0])
                  : undefined
              }
              progressWellAboveAverageKeys={
                progressIndicators
                  ? Object.entries(progressIndicators)
                      .filter((e) => e[1] === ProgressBanding.WellAboveAverage)
                      .map((e) => e[0])
                  : undefined
              }
              {...props}
            >
              {topLevel ? (
                <h2 className="govuk-heading-m">{title}</h2>
              ) : (
                <h3 className="govuk-heading-s">{title}</h3>
              )}
              {costCodesUnderTitle && <CostCodesList category={title} />}
              {(i === 0 || selector) &&
                (options ?? (
                  <ChartDimensions
                    dimensions={
                      chart.dimensions || dimensions || CostCategories
                    }
                    elementId={chartId}
                    handleChange={handleSelectChange}
                    value={dimension.value}
                  />
                ))}
            </HorizontalBarChartWrapper>
          </SuppressNegativeOrZeroContext.Provider>
        </ChartDimensionContext.Provider>
      );
    })
  );
}
