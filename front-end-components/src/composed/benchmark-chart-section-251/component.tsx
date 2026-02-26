import { useMemo } from "react";
import { BenchmarkChartSection251Props } from "src/composed/benchmark-chart-section-251";
import { LocalAuthoritySection251 } from "src/services";
import { HorizontalBarChartMultiSeries } from "../horizontal-bar-chart-multi-series";
import { ChartSeriesConfigItem } from "src/components";
import { shortValueFormatter } from "src/components/charts/utils";
import { LaChartData } from "src/components/charts/table-chart";

export function BenchmarkChartSection251<
  TData extends LocalAuthoritySection251,
>({
  chartTitle,
  data,
  valueField,
  sourceInfo,
}: BenchmarkChartSection251Props<TData>) {
  const mergedData = useMemo(() => {
    const dataPoints: LaChartData[] = [];

    if (data && Array.isArray(data)) {
      data.forEach((s) => {
        const outturnValue = s.outturn?.[valueField] as number | undefined;
        const budgetValue = s.budget?.[valueField] as number | undefined;

        // Skip if either is missing or invalid AB#297723
        if (
          outturnValue === undefined ||
          outturnValue === null ||
          isNaN(outturnValue) ||
          budgetValue === undefined ||
          budgetValue === null ||
          isNaN(budgetValue)
        ) {
          return;
        }

        dataPoints.push({
          laCode: s.code,
          laName: s.name,
          outturn: outturnValue,
          budget: budgetValue,
          totalPupils: s.totalPupils,
        });
      });
    }

    return {
      tableHeadings: [
        "Local authority",
        "Outturn",
        "Planned expenditure",
        "Number of pupils",
      ],
      dataPoints,
    };
  }, [data, valueField]);

  const seriesConfig: { [key: string]: ChartSeriesConfigItem } = {
    outturn: {
      label: "Outturn",
      visible: true,
      valueFormatter: (v) =>
        shortValueFormatter(v, {
          valueUnit: "currency",
        }),
    },
    budget: {
      label: "Planned expenditure",
      visible: true,
      valueFormatter: (v) =>
        shortValueFormatter(v, {
          valueUnit: "currency",
        }),
    },
  };

  return (
    <HorizontalBarChartMultiSeries
      chartTitle={chartTitle}
      data={mergedData}
      keyField="laCode"
      seriesConfig={seriesConfig}
      seriesLabelField="laName"
      showCopyImageButton
      valueUnit="currency"
      sourceInfo={sourceInfo}
    >
      <h3 className="govuk-heading-s govuk-!-margin-bottom-0">{chartTitle}</h3>
    </HorizontalBarChartMultiSeries>
  );
}
