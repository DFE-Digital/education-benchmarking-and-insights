import { HistoricChart2 } from "src/composed/historic-chart-2-composed";
import {
  SchoolHistoryBase,
  SchoolHistoryComparison,
  TotalCateringCostsField,
} from "src/services";
import { useState } from "react";
import { TotalCateringCostsType } from "src/components/total-catering-costs-type";

export function CateringCostsHistoryChart<T extends SchoolHistoryBase>({
  data,
  chartName,
}: {
  data: SchoolHistoryComparison<T>;
  chartName: string;
}) {
  const [totalCateringCostsField, setTotalCateringCostsField] =
    useState<TotalCateringCostsField>("totalGrossCateringCosts");

  return (
    <HistoricChart2
      chartName={chartName}
      data={data}
      valueField={totalCateringCostsField as keyof T}
    >
      <h3 className="govuk-heading-s">{chartName}</h3>
      <TotalCateringCostsType
        field={totalCateringCostsField}
        onChange={setTotalCateringCostsField}
      />
    </HistoricChart2>
  );
}
