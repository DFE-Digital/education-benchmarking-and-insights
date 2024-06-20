import { VerticalBarChart } from "src/components/charts/vertical-bar-chart";
import { ComparisonChartSummaryComposedProps } from "./types";
import { ChartDataSeries } from "src/components";
import { useMemo } from "react";
import {
  chartSeriesComparer,
  statValueFormatter,
} from "src/components/charts/utils";
import { Stat } from "src/components/charts/stat";
import { ResolvedStat } from "src/components/charts/resolved-stat";
import { WarningBanner } from "src/components/warning-banner";

export function ComparisonChartSummary<TData extends ChartDataSeries>(
  props: ComparisonChartSummaryComposedProps<TData>
) {
  const {
    data,
    highlightedItemKey,
    keyField,
    sortDirection,
    valueField,
    suffix,
    chartStats,
    hasIncompleteData,
    ...rest
  } = props;

  const sortedData = useMemo(() => {
    return data.sort((a, b) =>
      chartSeriesComparer(a, b, {
        dataPoint: valueField,
        direction: sortDirection,
      })
    );
  }, [data, sortDirection, valueField]);

  const seriesConfig: Partial<
    Record<
      keyof TData,
      {
        visible: boolean;
      }
    >
  > = {};
  seriesConfig[valueField] = {
    visible: true,
  };

  const highlightedItemIndex = data.findIndex(
    (d) => d[keyField] === highlightedItemKey
  );

  return (
    <>
      <WarningBanner
        isRendered={hasIncompleteData}
        message="Some schools are missing data for this financial year"
      />
      <div className="composed-chart-wrapper">
        <div className="composed-chart">
          <VerticalBarChart
            barCategoryGap={3}
            data={sortedData}
            hideXAxis
            hideYAxis
            highlightedItemKeys={
              highlightedItemKey ? [highlightedItemKey] : undefined
            }
            keyField={keyField}
            seriesConfig={seriesConfig}
            seriesLabelField={keyField}
            {...rest}
          />
        </div>
        <div className="chart-stat-summary">
          <ResolvedStat
            chartName="This school spends"
            className="chart-stat-highlight"
            data={data}
            displayIndex={highlightedItemIndex}
            seriesLabel="This school spends"
            seriesLabelField="urn"
            suffix={suffix}
            valueField={valueField}
            valueFormatter={statValueFormatter}
            valueUnit="currency"
          />
          <Stat
            chartName="Similar schools spend"
            label="Similar schools spend"
            suffix={suffix && `${suffix}, on average`}
            value={chartStats.average}
            valueFormatter={statValueFormatter}
            valueUnit="currency"
          />
          {!isNaN(chartStats.difference) && (
            <Stat
              chartName="This school spends"
              label="This school spends"
              suffix={
                <span>
                  <strong>{chartStats.difference < 0 ? "less" : "more"}</strong>{" "}
                  {suffix}
                </span>
              }
              value={Math.abs(chartStats.difference)}
              valueFormatter={statValueFormatter}
              valueSuffix={
                isNaN(chartStats.percentDifference) ||
                !isFinite(chartStats.percentDifference)
                  ? undefined
                  : `(${Math.abs(chartStats.percentDifference).toFixed(1)}%)`
              }
              valueUnit="currency"
            />
          )}
        </div>
      </div>
    </>
  );
}
