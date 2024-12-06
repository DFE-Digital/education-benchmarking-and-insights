import { HistoricChart2 } from "src/composed/historic-chart-2-composed";
import {
  SchoolExpenditureHistory,
  SchoolHistoryComparison,
} from "src/services";
import { Loading } from "src/components/loading";

export const SpendingSectionPremisesServices: React.FC<{
  data: SchoolHistoryComparison<SchoolExpenditureHistory>;
}> = ({ data }) => {
  return (
    <>
      {data.school?.length ? (
        <>
          <HistoricChart2
            chartName="Total premises staff and services costs"
            data={data}
            valueField="totalPremisesStaffServiceCosts"
          >
            <h3 className="govuk-heading-s">
              Total premises staff and services costs
            </h3>
          </HistoricChart2>
        </>
      ) : (
        <Loading />
      )}
    </>
  );
};
