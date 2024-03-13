import { VerticalBarChart } from "src/components/charts/vertical-bar-chart";
import { ComparisonChartSummaryComposedProps } from "./types";
import { ChartDataSeries } from "src/components";
import { useMemo } from "react";
import { chartSeriesComparer } from "src/components/charts/utils";

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

  return (
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
  );
}
