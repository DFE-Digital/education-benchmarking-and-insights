import { HistoricChart } from "src/composed/historic-chart-composed";
import { ExpenditureHistoryRow } from "src/services";
import { Loading } from "src/components/loading";

export const SpendingSectionAdministrativeSupplies: React.FC<{
  data: ExpenditureHistoryRow[];
}> = ({ data }) => {
  return (
    <>
      {data.length > 0 ? (
        <HistoricChart
          chartTitle="Administration supplies (non educational) costs"
          data={data}
          seriesConfig={{
            administrativeSuppliesNonEducationalCosts: {
              label: "Administration supplies (non educational) costs",
              visible: true,
            },
          }}
          valueField="administrativeSuppliesNonEducationalCosts"
        >
          <h3 className="govuk-heading-s">
            Administration supplies (non educational) costs
          </h3>
        </HistoricChart>
      ) : (
        <Loading />
      )}
    </>
  );
};
