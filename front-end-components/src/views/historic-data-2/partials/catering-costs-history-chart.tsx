import {
  HistoricChart2,
  HistoricChart2Props,
} from "src/composed/historic-chart-2-composed";
import { HistoryBase, TotalCateringCostsField } from "src/services";
import { useState } from "react";
import { TotalCateringCostsType } from "src/components/total-catering-costs-type";

export function CateringCostsHistoryChart<T extends HistoryBase>(
  props: Pick<
    HistoricChart2Props<T>,
    "data" | "chartTitle" | "perUnitDimension"
  >
) {
  const [totalCateringCostsField, setTotalCateringCostsField] =
    useState<TotalCateringCostsField>("totalGrossCateringCosts");

  return (
    <HistoricChart2 valueField={totalCateringCostsField as keyof T} {...props}>
      <h3 className="govuk-heading-s">{props.chartTitle}</h3>
      <TotalCateringCostsType
        field={totalCateringCostsField}
        onChange={setTotalCateringCostsField}
      />
    </HistoricChart2>
  );
}
