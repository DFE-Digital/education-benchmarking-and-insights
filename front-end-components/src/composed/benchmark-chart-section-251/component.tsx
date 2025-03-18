import { useMemo } from "react";
import { BenchmarkChartSection251Props } from "src/composed/benchmark-chart-section-251";
import { LocalAuthoritySection251 } from "src/services";
import { HorizontalBarChartMultiSeries } from "../horizontal-bar-chart-multi-series";
import { ChartSeriesConfigItem } from "src/components";
import { shortValueFormatter } from "src/components/charts/utils";
import { LaChartData } from "src/components/charts/table-chart";
import { DataWarning } from "src/components/charts/data-warning";

export function BenchmarkChartSection251<
  TData extends LocalAuthoritySection251,
>({ chartTitle, data, valueField }: BenchmarkChartSection251Props<TData>) {
  const mergedData = useMemo(() => {
    const dataPoints: LaChartData[] = [];

    if (data && Array.isArray(data)) {
      data.forEach((s) => {
        const outturnValue = s.outturn && (s.outturn[valueField] as number);
        const budgetValue = s.budget && (s.budget[valueField] as number);

        dataPoints.push({
          laCode: s.code,
          laName: s.name,
          actual:
            outturnValue === undefined ||
            outturnValue === null ||
            isNaN(outturnValue)
              ? undefined
              : outturnValue,
          planned:
            budgetValue === undefined ||
            budgetValue === null ||
            isNaN(budgetValue)
              ? undefined
              : budgetValue,
        });
      });
    }

    return {
      tableHeadings: ["Local authority", "Actual", "Planned"],
      dataPoints,
    };
  }, [data, valueField]);

  const seriesConfig: { [key: string]: ChartSeriesConfigItem } = {
    actual: {
      label: "Actual",
      visible: true,
      valueFormatter: (v) =>
        shortValueFormatter(v, {
          valueUnit: "currency",
        }),
    },
    planned: {
      label: "Planned",
      visible: true,
      valueFormatter: (v) =>
        shortValueFormatter(v, {
          valueUnit: "currency",
        }),
    },
  };

  const missingDataKeys = mergedData.dataPoints
    .filter((d) => !d.actual && !d.planned)
    .map((d) => d.laCode);

  return (
    <HorizontalBarChartMultiSeries
      chartTitle={chartTitle}
      data={mergedData}
      keyField="laCode"
      missingDataKeys={missingDataKeys}
      seriesConfig={seriesConfig}
      seriesLabelField="laName"
      showCopyImageButton
      valueUnit="currency"
    >
      <h3 className="govuk-heading-s govuk-!-margin-bottom-0">{chartTitle}</h3>
      {missingDataKeys.length > 0 && (
        <DataWarning className="govuk-!-margin-top-3">
          {missingDataKeys.length > 1
            ? "Comparator local authorities have missing data"
            : "Comparator local authority has missing data"}
        </DataWarning>
      )}
    </HorizontalBarChartMultiSeries>
  );
}
