import { useMemo } from "react";
import { BenchmarkChartSend2Props } from "src/composed/benchmark-chart-send-2";
import { LocalAuthoritySend2Benchmark } from "src/services";
import { LaChartData } from "src/components/charts/table-chart";
import { HorizontalBarChartWrapper } from "../horizontal-bar-chart-wrapper";
import { DataWarning } from "src/components/charts/data-warning";

export function BenchmarkChartSend2<
  TData extends LocalAuthoritySend2Benchmark,
>({ chartTitle, data, valueField }: BenchmarkChartSend2Props<TData>) {
  const mergedData = useMemo(() => {
    const dataPoints: LaChartData[] = [];

    if (data && Array.isArray(data)) {
      data.forEach((s) => {
        const value = s && (s[valueField] as number);

        dataPoints.push({
          laCode: s.code,
          laName: s.name,
          value:
            value === undefined || value === null || isNaN(value)
              ? undefined
              : value,
        });
      });
    }

    return {
      tableHeadings: ["Local authority", "Amount"],
      dataPoints,
    };
  }, [data, valueField]);

  const missingDataKeys = mergedData.dataPoints
    .filter((d) => !d.value)
    .map((d) => d.laCode);

  return (
    <HorizontalBarChartWrapper
      chartTitle={chartTitle}
      data={mergedData}
      xAxisLabel="Amount"
      localAuthority
      missingDataKeys={missingDataKeys}
      showCopyImageButton
      valueUnit="amount"
    >
      <h3 className="govuk-heading-s govuk-!-margin-bottom-0">{chartTitle}</h3>
      {missingDataKeys.length > 0 && (
        <DataWarning className="govuk-!-margin-top-3">
          {missingDataKeys.length > 1
            ? "Comparator local authorities have missing data"
            : "Comparator local authority has missing data"}
        </DataWarning>
      )}
    </HorizontalBarChartWrapper>
  );
}
