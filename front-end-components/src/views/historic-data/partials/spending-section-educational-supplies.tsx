import { HistoricChart } from "src/composed/historic-chart-composed";
import { ExpenditureHistoryRow } from "src/services";
import { Loading } from "src/components/loading";

export const SpendingSectionEducationalSupplies: React.FC<{
  data: ExpenditureHistoryRow[];
}> = ({ data }) => {
  return (
    <>
      {data.length > 0 ? (
        <>
          <HistoricChart
            chartTitle="Total educational supplies costs"
            data={data}
            seriesConfig={{
              totalEducationalSuppliesCosts: {
                label: "Total educational supplies costs",
                visible: true,
              },
            }}
            valueField="totalEducationalSuppliesCosts"
          >
            <h3 className="govuk-heading-s">
              Total educational supplies costs
            </h3>
          </HistoricChart>

          <HistoricChart
            chartTitle="Examination fees costs"
            data={data}
            seriesConfig={{
              examinationFeesCosts: {
                label: "Examination fees costs",
                visible: true,
              },
            }}
            valueField="examinationFeesCosts"
          >
            <h3 className="govuk-heading-s">Examination fees costs</h3>
          </HistoricChart>

          <HistoricChart
            chartTitle="Learning resources (non ICT equipment) costs"
            data={data}
            seriesConfig={{
              learningResourcesNonIctCosts: {
                label: "Learning resources (non ICT equipment) costs",
                visible: true,
              },
            }}
            valueField="learningResourcesNonIctCosts"
          >
            <h3 className="govuk-heading-s">
              Learning resources (non ICT equipment) costs
            </h3>
          </HistoricChart>
        </>
      ) : (
        <Loading />
      )}
    </>
  );
};
