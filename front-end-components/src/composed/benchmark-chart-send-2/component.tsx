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

    data?.forEach((s) => {
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

    return {
      tableHeadings: ["Local authority", "Amount"],
      dataPoints,
    };
  }, [data, valueField]);

  return (
    <HorizontalBarChartWrapper
      chartTitle={chartTitle}
      data={mergedData}
      localAuthority
      showCopyImageButton
      valueUnit="currency"
    >
      <h3 className="govuk-heading-m">{chartTitle}</h3>
    </HorizontalBarChartWrapper>
  );
}
