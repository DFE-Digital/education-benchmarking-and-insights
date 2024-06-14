import { HistoricChart } from "src/composed/historic-chart-composed";
import { ExpenditureHistory } from "src/services";
import { Loading } from "src/components/loading";

export const SpendingSectionOther: React.FC<{
  data: ExpenditureHistory[];
}> = ({ data }) => {
  return (
    <>
      {data.length > 0 ? (
        <>
          <HistoricChart
            chartName="Total other costs"
            data={data}
            seriesConfig={{
              totalOtherCosts: {
                label: "Total other costs",
                visible: true,
              },
            }}
            valueField="totalOtherCosts"
          >
            <h3 className="govuk-heading-s">Total other costs</h3>
          </HistoricChart>

          <HistoricChart
            chartName="Direct revenue financing costs"
            data={data}
            seriesConfig={{
              directRevenueFinancingCosts: {
                label: "Direct revenue financing costs",
                visible: true,
              },
            }}
            valueField="directRevenueFinancingCosts"
          >
            <h3 className="govuk-heading-s">Direct revenue financing costs</h3>
          </HistoricChart>

          <HistoricChart
            chartName="Grounds maintenance costs"
            data={data}
            seriesConfig={{
              groundsMaintenanceCosts: {
                label: "Grounds maintenance costs",
                visible: true,
              },
            }}
            valueField="groundsMaintenanceCosts"
          >
            <h3 className="govuk-heading-s">Grounds maintenance costs</h3>
          </HistoricChart>

          <HistoricChart
            chartName="Indirect employee expenses costs"
            data={data}
            seriesConfig={{
              indirectEmployeeExpenses: {
                label: "Indirect employee expenses costs",
                visible: true,
              },
            }}
            valueField="indirectEmployeeExpenses"
          >
            <h3 className="govuk-heading-s">
              Indirect employee expenses costs
            </h3>
          </HistoricChart>

          <HistoricChart
            chartName="Interest changes for loan and bank costs"
            data={data}
            seriesConfig={{
              interestChargesLoanBank: {
                label: "Interest changes for loan and bank costs",
                visible: true,
              },
            }}
            valueField="interestChargesLoanBank"
          >
            <h3 className="govuk-heading-s">
              Interest changes for loan and bank costs
            </h3>
          </HistoricChart>

          <HistoricChart
            chartName="Other insurance premiums costs"
            data={data}
            seriesConfig={{
              otherInsurancePremiumsCosts: {
                label: "Other insurance premiums costs",
                visible: true,
              },
            }}
            valueField="otherInsurancePremiumsCosts"
          >
            <h3 className="govuk-heading-s">Other insurance premiums costs</h3>
          </HistoricChart>

          <HistoricChart
            chartName="PFI charges costs"
            data={data}
            seriesConfig={{
              privateFinanceInitiativeCharges: {
                label: "PFI charges costs",
                visible: true,
              },
            }}
            valueField="privateFinanceInitiativeCharges"
          >
            <h3 className="govuk-heading-s">PFI charges costs</h3>
          </HistoricChart>

          <HistoricChart
            chartName="Rents and rates costs"
            data={data}
            seriesConfig={{
              rentRatesCosts: {
                label: "Rents and rates costs",
                visible: true,
              },
            }}
            valueField="rentRatesCosts"
          >
            <h3 className="govuk-heading-s">Rents and rates costs</h3>
          </HistoricChart>

          <HistoricChart
            chartName="Special facilities costs"
            data={data}
            seriesConfig={{
              specialFacilitiesCosts: {
                label: "Special facilities costs",
                visible: true,
              },
            }}
            valueField="specialFacilitiesCosts"
          >
            <h3 className="govuk-heading-s">Special facilities costs</h3>
          </HistoricChart>

          <HistoricChart
            chartName="Staff development and training costs"
            data={data}
            seriesConfig={{
              staffDevelopmentTrainingCosts: {
                label: "Staff development and training costs",
                visible: true,
              },
            }}
            valueField="staffDevelopmentTrainingCosts"
          >
            <h3 className="govuk-heading-s">
              Staff development and training costs
            </h3>
          </HistoricChart>

          <HistoricChart
            chartName="Staff-related insurance costs"
            data={data}
            seriesConfig={{
              staffRelatedInsuranceCosts: {
                label: "Staff-related insurance costs",
                visible: true,
              },
            }}
            valueField="staffRelatedInsuranceCosts"
          >
            <h3 className="govuk-heading-s">Staff-related insurance costs</h3>
          </HistoricChart>

          <HistoricChart
            chartName="Supply teacher insurance costs"
            data={data}
            seriesConfig={{
              supplyTeacherInsurableCosts: {
                label: "Supply teacher insurance costs",
                visible: true,
              },
            }}
            valueField="supplyTeacherInsurableCosts"
          >
            <h3 className="govuk-heading-s">Supply teacher insurance costs</h3>
          </HistoricChart>

          <HistoricChart
            chartName="Community focused school staff (maintained schools only)"
            data={data}
            seriesConfig={{
              communityFocusedSchoolStaff: {
                label:
                  "Community focused school staff (maintained schools only)",
                visible: true,
              },
            }}
            valueField="communityFocusedSchoolStaff"
          >
            <h3 className="govuk-heading-s">
              Community focused school staff (maintained schools only)
            </h3>
          </HistoricChart>
        </>
      ) : (
        <Loading />
      )}
    </>
  );
};
