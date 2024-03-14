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
      <div className="govuk-grid-row composed-chart">
        <div className="govuk-grid-column-full">
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
      </div>
      <div className="govuk-grid-row chart-stat-summary">
        <div className="govuk-grid-column-one-third">
          <ResolvedStat
            chartName="This school spends"
            className="chart-stat-highlight"
            data={data}
            displayIndex={highlightedItemIndex}
            seriesLabel="This school spends"
            seriesLabelField="urn"
            suffix="per pupil"
            valueField={valueField}
            valueUnit="currency"
          />
        </div>
        <div className="govuk-grid-column-one-third">
          <Stat
            chartName="Similar schools spend"
            label="Similar schools spend"
            suffix="per pupil, on average"
            value={mean}
            valueUnit="currency"
          />
        </div>
        <div className="govuk-grid-column-one-third">
          <Stat
            chartName="This school spends"
            label="This school spends"
            suffix={
              <span>
                <strong>{meanDiff < 0 ? "less" : "more"}</strong> per pupil
              </span>
            }
            value={meanDiff}
            valueSuffix={`(${meanDiffPercent.toFixed(1)}%)`}
            valueUnit="currency"
          />
        </div>
      </div>
    </div>
  );
}
