import { useMemo, useContext } from "react";
import { BenchmarkChartSend2Props } from "src/composed/benchmark-chart-send-2";
import { LocalAuthoritySend2Benchmark } from "src/services";
import { LaChartData } from "src/components/charts/table-chart";
import { HorizontalBarChartWrapper } from "../horizontal-bar-chart-wrapper";
import { SelectedEstablishmentContext } from "src/contexts";

export function BenchmarkChartSend2<
  TData extends LocalAuthoritySend2Benchmark,
>({ chartTitle, data, valueField }: BenchmarkChartSend2Props<TData>) {
  const selectedEstablishment = useContext(SelectedEstablishmentContext);

  const mergedData = useMemo(() => {
    const dataPoints: LaChartData[] = [];

    if (data && Array.isArray(data)) {
      data.forEach((s) => {
        const value = s && (s[valueField] as number);

        const valueInvalid =
          value === undefined || value === null || isNaN(value);
        const isSelectedEstablishment = selectedEstablishment === s.code;

        if (valueInvalid && !isSelectedEstablishment) {
          return;
        }

        dataPoints.push({
          laCode: s.code,
          laName: s.name,
          value: valueInvalid ? undefined : value,
          totalPupils: s.totalPupils,
        });
      });
    }

    return {
      tableHeadings: ["Local authority", "Amount", "Number of pupils"],
      dataPoints,
    };
  }, [data, valueField, selectedEstablishment]);

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
