import { HistoricChart } from "src/composed/historic-chart-composed";
import {
  SchoolExpenditureHistory,
  TotalCateringCostsField,
} from "src/services";
import { Loading } from "src/components/loading";
import { useState } from "react";
import { TotalCateringCostsType } from "src/components/total-catering-costs-type";

export const SpendingSectionCateringServices: React.FC<{
  data: SchoolExpenditureHistory[];
}> = ({ data }) => {
  const [totalCateringCostsField, setTotalCateringCostsField] =
    useState<TotalCateringCostsField>("totalGrossCateringCosts");

  return (
    <>
      {data.length > 0 ? (
        <>
          <HistoricChart
            chartName="Total catering costs"
            data={data}
            seriesConfig={{
              totalGrossCateringCosts: {
                label: "Total gross catering costs",
                visible: totalCateringCostsField == "totalGrossCateringCosts",
              },
              totalNetCateringCosts: {
                label: "Total net catering costs",
                visible: totalCateringCostsField == "totalNetCateringCosts",
              },
            }}
            valueField={totalCateringCostsField}
          >
            <h3 className="govuk-heading-s">Total catering costs</h3>
            <TotalCateringCostsType
              field={totalCateringCostsField}
              onChange={setTotalCateringCostsField}
            />
          </HistoricChart>

          <HistoricChart
            chartName="Catering staff costs"
            data={data}
            seriesConfig={{
              cateringStaffCosts: {
                label: "Catering staff costs",
                visible: true,
              },
            }}
            valueField="cateringStaffCosts"
          >
            <h3 className="govuk-heading-s">Catering staff costs</h3>
          </HistoricChart>

          <HistoricChart
            chartName="Catering supplies costs"
            data={data}
            seriesConfig={{
              cateringSuppliesCosts: {
                label: "Catering supplies costs",
                visible: true,
              },
            }}
            valueField="cateringSuppliesCosts"
          >
            <h3 className="govuk-heading-s">Catering supplies costs</h3>
          </HistoricChart>
        </>
      ) : (
        <Loading />
      )}
    </>
  );
};
