import { HistoricChart } from "src/composed/historic-chart-composed";
import { ExpenditureHistory } from "src/services";
import { Loading } from "src/components/loading";

export const SpendingSectionCateringServices: React.FC<{
  data: ExpenditureHistory[];
}> = ({ data }) => {
  return (
    <>
      {data.length > 0 ? (
        <>
          <HistoricChart
            chartName="Total gross catering costs"
            data={data}
            seriesConfig={{
              totalGrossCateringCosts: {
                label: "Total gross catering costs",
                visible: true,
              },
            }}
            valueField="totalGrossCateringCosts"
          >
            <h3 className="govuk-heading-s">Total gross catering costs</h3>
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
