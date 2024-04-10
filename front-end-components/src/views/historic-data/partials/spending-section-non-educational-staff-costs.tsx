import { HistoricChart } from "src/composed/historic-chart-composed";
import { Expenditure } from "src/services";
import { Loading } from "src/components/loading";

export const SpendingSectionNonEducationalStaffCosts: React.FC<{
  data: Expenditure[];
}> = ({ data }) => {
  return (
    <>
      {data.length > 0 ? (
        <>
          <HistoricChart
            chartName="Total non-educational support staff costs"
            data={data}
            seriesConfig={{
              totalNonEducationalSupportStaffCosts: {
                label: "Total non-educational support staff costs",
                visible: true,
              },
            }}
            valueField="totalNonEducationalSupportStaffCosts"
          >
            <h3 className="govuk-heading-s">
              Total non-educational support staff costs
            </h3>
          </HistoricChart>

          <HistoricChart
            chartName="Administrative and clerical staff costs"
            data={data}
            seriesConfig={{
              administrativeClericalStaffCosts: {
                label: "Administrative and clerical staff costs",
                visible: true,
              },
            }}
            valueField="administrativeClericalStaffCosts"
          >
            <h3 className="govuk-heading-s">
              Administrative and clerical staff costs
            </h3>
          </HistoricChart>

          <HistoricChart
            chartName="Auditor costs"
            data={data}
            seriesConfig={{
              auditorsCosts: {
                label: "Auditor costs",
                visible: true,
              },
            }}
            valueField="auditorsCosts"
          >
            <h3 className="govuk-heading-s">Auditor costs</h3>
          </HistoricChart>

          <HistoricChart
            chartName="Other staff costs"
            data={data}
            seriesConfig={{
              otherStaffCosts: {
                label: "Other staff costs",
                visible: true,
              },
            }}
            valueField="otherStaffCosts"
          >
            <h3 className="govuk-heading-s">Other staff costs</h3>
          </HistoricChart>

          <HistoricChart
            chartName="Professional services (non-curriculum) cost"
            data={data}
            seriesConfig={{
              professionalServicesNonCurriculumCosts: {
                label: "Professional services (non-curriculum) cost",
                visible: true,
              },
            }}
            valueField="professionalServicesNonCurriculumCosts"
          >
            <h3 className="govuk-heading-s">
              Professional services (non-curriculum) cost
            </h3>
          </HistoricChart>
        </>
      ) : (
        <Loading />
      )}
    </>
  );
};
