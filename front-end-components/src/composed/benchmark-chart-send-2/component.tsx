import { useMemo } from "react";
import { BenchmarkChartSend2Props } from "src/composed/benchmark-chart-send-2";
import { LocalAuthoritySend2Benchmark } from "src/services";
import { LaChartData } from "src/components/charts/table-chart";
import { HorizontalBarChartWrapper } from "../horizontal-bar-chart-wrapper";

export function BenchmarkChartSend2<
  TData extends LocalAuthoritySend2Benchmark,
>({ chartTitle, data, valueField }: BenchmarkChartSend2Props<TData>) {
  const mergedData = useMemo(() => {
    const dataPoints: LaChartData[] = [];

    if (data && Array.isArray(data)) {
      data.forEach((s) => {
        const value = s && (s[valueField] as number);

        if (value === undefined || value === null || isNaN(value)) {
          return;
        }

        dataPoints.push({
          laCode: s.code,
          laName: s.name,
          value: value,
          totalPupils: s.totalPupils,
        });
      });
    }

    return {
      tableHeadings: ["Local authority", "Amount", "Number of pupils"],
      dataPoints,
    };
  }, [data, valueField]);

  return (
    <HorizontalBarChartWrapper
      chartTitle={chartTitle}
      data={mergedData}
      xAxisLabel="Amount"
      localAuthority
      showCopyImageButton
      valueUnit="amount"
    >
      <h3 className="govuk-heading-s govuk-!-margin-bottom-0">{chartTitle}</h3>
    </HorizontalBarChartWrapper>
  );
}
