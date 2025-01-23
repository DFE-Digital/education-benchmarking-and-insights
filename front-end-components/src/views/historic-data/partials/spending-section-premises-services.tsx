import { HistoricChart } from "src/composed/historic-chart-composed";
import { ExpenditureHistoryRow } from "src/services";
import { Loading } from "src/components/loading";

export const SpendingSectionPremisesServices: React.FC<{
  data: ExpenditureHistoryRow[];
}> = ({ data }) => {
  return (
    <>
      {data.length > 0 ? (
        <>
          <HistoricChart
            chartTitle="Total premises staff and services costs"
            data={data}
            seriesConfig={{
              totalPremisesStaffServiceCosts: {
                label: "Total premises staff and services costs",
                visible: true,
              },
            }}
            valueField="totalPremisesStaffServiceCosts"
          >
            <h3 className="govuk-heading-s">
              Total premises staff and services costs
            </h3>
          </HistoricChart>

          <HistoricChart
            chartTitle="Cleaning and caretaking costs"
            data={data}
            seriesConfig={{
              cleaningCaretakingCosts: {
                label: "Cleaning and caretaking costs",
                visible: true,
              },
            }}
            valueField="cleaningCaretakingCosts"
          >
            <h3 className="govuk-heading-s">Cleaning and caretaking costs</h3>
          </HistoricChart>

          <HistoricChart
            chartTitle="Maintenance of premises costs"
            data={data}
            seriesConfig={{
              maintenancePremisesCosts: {
                label: "Maintenance of premises costs",
                visible: true,
              },
            }}
            valueField="maintenancePremisesCosts"
          >
            <h3 className="govuk-heading-s">Maintenance of premises costs</h3>
          </HistoricChart>

          <HistoricChart
            chartTitle="Other occupation costs"
            data={data}
            seriesConfig={{
              otherOccupationCosts: {
                label: "Other occupation costs",
                visible: true,
              },
            }}
            valueField="otherOccupationCosts"
          >
            <h3 className="govuk-heading-s">Other occupation costs</h3>
          </HistoricChart>

          <HistoricChart
            chartTitle="Premises staff costs"
            data={data}
            seriesConfig={{
              premisesStaffCosts: {
                label: "Premises staff costs",
                visible: true,
              },
            }}
            valueField="premisesStaffCosts"
          >
            <h3 className="govuk-heading-s">Premises staff costs</h3>
          </HistoricChart>
        </>
      ) : (
        <Loading />
      )}
    </>
  );
};
