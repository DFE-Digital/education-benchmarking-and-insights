import { HistoricChart } from "src/composed/historic-chart-composed";
import { ExpenditureHistory } from "src/services";
import { Loading } from "src/components/loading";

export const SpendingSectionUtilities: React.FC<{
  data: ExpenditureHistory[];
}> = ({ data }) => {
  return (
    <>
      {data.length > 0 ? (
        <>
          <HistoricChart
            chartName="Total utilities costs"
            data={data}
            seriesConfig={{
              totalUtilitiesCosts: {
                label: "Total utilities costs",
                visible: true,
              },
            }}
            valueField="totalUtilitiesCosts"
          >
            <h3 className="govuk-heading-s">Total utilities costs</h3>
          </HistoricChart>

          <HistoricChart
            chartName="Energy costs"
            data={data}
            seriesConfig={{
              energyCosts: {
                label: "Energy costs",
                visible: true,
              },
            }}
            valueField="energyCosts"
          >
            <h3 className="govuk-heading-s">Energy costs</h3>
          </HistoricChart>

          <HistoricChart
            chartName="Water and sewerage costs"
            data={data}
            seriesConfig={{
              waterSewerageCosts: {
                label: "Water and sewerage costs",
                visible: true,
              },
            }}
            valueField="waterSewerageCosts"
          >
            <h3 className="govuk-heading-s">Water and sewerage costs</h3>
          </HistoricChart>
        </>
      ) : (
        <Loading />
      )}
    </>
  );
};
