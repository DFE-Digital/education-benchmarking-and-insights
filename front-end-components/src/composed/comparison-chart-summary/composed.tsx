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
import stats from "stats-lite";

export function ComparisonChartSummary<TData extends ChartDataSeries>(
  props: ComparisonChartSummaryComposedProps<TData>
) {
  const {
    averageType,
    data,
    highlightedItemKey,
    keyField,
    sortDirection,
    valueField,
    suffix,
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

  const average = useMemo(() => {
    const values = data.map((d) => d[valueField] as number);
    switch (averageType) {
      case "median":
        return stats.median(values);
      case "mode":
        return stats.mode(values);
      default:
        return stats.mean(values);
    }
  }, [averageType, data, valueField]);

  const averageDiff =
    (data[highlightedItemIndex][valueField] as number) - average;

  const averageDiffPercent = (averageDiff / average) * 100;

  return (
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
          includeNegativeValues
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
          value={average}
          valueFormatter={statValueFormatter}
          valueUnit="currency"
        />
        {!isNaN(averageDiff) && (
          <Stat
            chartName="This school spends"
            label="This school spends"
            suffix={
              <span>
                <strong>{averageDiff < 0 ? "less" : "more"}</strong> {suffix}
              </span>
            }
            value={Math.abs(averageDiff)}
            valueFormatter={statValueFormatter}
            valueSuffix={
              isNaN(averageDiffPercent) || !isFinite(averageDiffPercent)
                ? undefined
                : `(${Math.abs(averageDiffPercent).toFixed(1)}%)`
            }
            valueUnit="currency"
          />
        )}
      </div>
    </div>
  );
}
