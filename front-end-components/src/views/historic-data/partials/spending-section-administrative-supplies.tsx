import { HistoricChart } from "src/composed/historic-chart-composed";
import { SchoolExpenditureHistory } from "src/services";
import { Loading } from "src/components/loading";

export const SpendingSectionAdministrativeSupplies: React.FC<{
  data: SchoolExpenditureHistory[];
}> = ({ data }) => {
  return (
    <>
      {data.length > 0 ? (
        <HistoricChart
          chartName="Administration supplies (non educational) costs"
          data={data}
          seriesConfig={{
            administrativeSuppliesCosts: {
              label: "Administration supplies (non educational) costs",
              visible: true,
            },
          }}
          valueField="administrativeSuppliesCosts"
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
