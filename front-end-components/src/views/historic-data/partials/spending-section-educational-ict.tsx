import { HistoricChart } from "src/composed/historic-chart-composed";
import { SchoolExpenditureHistory } from "src/services";
import { Loading } from "src/components/loading";

export const SpendingSectionEducationalIct: React.FC<{
  data: SchoolExpenditureHistory[];
}> = ({ data }) => {
  return (
    <>
      {data.length > 0 ? (
        <HistoricChart
          chartName="ICT learning resources costs"
          data={data}
          seriesConfig={{
            learningResourcesIctCosts: {
              label: "ICT learning resources costs",
              visible: true,
            },
          }}
          valueField="learningResourcesIctCosts"
        >
          <h3 className="govuk-heading-s">ICT learning resources costs</h3>
        </HistoricChart>
      ) : (
        <Loading />
      )}
    </>
  );
};
