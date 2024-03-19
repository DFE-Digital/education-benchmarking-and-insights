import { VerticalBarChart } from "src/components/charts/vertical-bar-chart";
import { ComparisonChartSummaryComposedProps } from "./types";
import { ChartDataSeries } from "src/components";
import { useMemo } from "react";
import { chartSeriesComparer } from "src/components/charts/utils";
import { Stat } from "src/components/charts/stat";
import { ResolvedStat } from "src/components/charts/resolved-stat";

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

  const mean =
    data.reduce((prev, curr) => (prev += curr[valueField] as number), 0) /
    data.length;

  const meanDiff = (data[highlightedItemIndex][valueField] as number) - mean;

  const meanDiffPercent = (meanDiff / mean) * 100;

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
          valueUnit="currency"
        />
        <Stat
          chartName="Similar schools spend"
          label="Similar schools spend"
          suffix={suffix && `${suffix}, on average`}
          value={mean}
          valueUnit="currency"
        />
        <Stat
          chartName="This school spends"
          label="This school spends"
          suffix={
            <span>
              <strong>{meanDiff < 0 ? "less" : "more"}</strong> {suffix}
            </span>
          }
          value={Math.abs(meanDiff)}
          valueSuffix={`(${Math.abs(meanDiffPercent).toFixed(1)}%)`}
          valueUnit="currency"
        />
      </div>
    </div>
  );
}
