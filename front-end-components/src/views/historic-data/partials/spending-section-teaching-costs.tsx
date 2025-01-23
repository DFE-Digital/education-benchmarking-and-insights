import { HistoricChart } from "src/composed/historic-chart-composed";
import { ExpenditureHistoryRow } from "src/services";
import { Loading } from "src/components/loading";

export const SpendingSectionTeachingCosts: React.FC<{
  data: ExpenditureHistoryRow[];
}> = ({ data }) => {
  return (
    <>
      {data.length > 0 ? (
        <>
          <HistoricChart
            chartTitle="Total teaching and teaching support staff costs"
            data={data}
            seriesConfig={{
              totalTeachingSupportStaffCosts: {
                label: "Total teaching and teaching support staff costs",
                visible: true,
              },
            }}
            valueField="totalTeachingSupportStaffCosts"
          >
            <h3 className="govuk-heading-s">
              Total teaching and teaching support staff costs
            </h3>
          </HistoricChart>

          <HistoricChart
            chartTitle="Teaching staff costs"
            data={data}
            seriesConfig={{
              teachingStaffCosts: {
                label: "Teaching staff costs",
                visible: true,
              },
            }}
            valueField="teachingStaffCosts"
          >
            <h3 className="govuk-heading-s">Teaching staff costs</h3>
          </HistoricChart>

          <HistoricChart
            chartTitle="Supply teaching staff"
            data={data}
            seriesConfig={{
              supplyTeachingStaffCosts: {
                label: "Supply teaching staff",
                visible: true,
              },
            }}
            valueField="supplyTeachingStaffCosts"
          >
            <h3 className="govuk-heading-s">Supply teaching staff</h3>
          </HistoricChart>

          <HistoricChart
            chartTitle="Educational consultancy"
            data={data}
            seriesConfig={{
              educationalConsultancyCosts: {
                label: "Educational consultancy",
                visible: true,
              },
            }}
            valueField="educationalConsultancyCosts"
          >
            <h3 className="govuk-heading-s">Educational consultancy</h3>
          </HistoricChart>

          <HistoricChart
            chartTitle="Education support staff"
            data={data}
            seriesConfig={{
              educationSupportStaffCosts: {
                label: "Education support staff",
                visible: true,
              },
            }}
            valueField="educationSupportStaffCosts"
          >
            <h3 className="govuk-heading-s">Education support staff</h3>
          </HistoricChart>

          <HistoricChart
            chartTitle="Agency supply teaching staff"
            data={data}
            seriesConfig={{
              agencySupplyTeachingStaffCosts: {
                label: "Agency supply teaching staff",
                visible: true,
              },
            }}
            valueField="agencySupplyTeachingStaffCosts"
          >
            <h3 className="govuk-heading-s">Agency supply teaching staff</h3>
          </HistoricChart>
        </>
      ) : (
        <Loading />
      )}
    </>
  );
};
