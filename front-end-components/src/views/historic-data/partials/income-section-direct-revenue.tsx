import { HistoricChart } from "src/composed/historic-chart-composed";
import { Income } from "src/services";
import { Loading } from "src/components/loading";
import React from "react";

export const IncomeSectionDirectRevenue: React.FC<{
  data: Income[];
}> = ({ data }) => {
  return (
    <>
      {data.length > 0 ? (
        <>
          <HistoricChart
            chartName="Direct revenue financing (capital reserves transfers)"
            data={data}
            seriesConfig={{
              directRevenueFinancing: {
                label: "Direct revenue financing",
                visible: true,
              },
            }}
            valueField="directRevenueFinancing"
          >
            <h3 className="govuk-heading-s">
              Direct revenue financing (capital reserves transfers)
            </h3>
          </HistoricChart>
          <details className="govuk-details">
            <summary className="govuk-details__summary">
              <span className="govuk-details__summary-text">
                More about direct revenue financing
              </span>
            </summary>
            <div className="govuk-details__text">
              <p>This includes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>
                  all amounts transferred to CI04 to be accumulated to fund
                  capital works (it may include receipts from insurance claims
                  for capital losses received into income under I11)
                </li>
                <li>
                  any amount transferred to a local authority reserve to part
                  fund a capital scheme delivered by the local authority
                </li>
                <li>
                  any repayment of principal on a capital loan from the local
                  authority
                </li>
              </ul>
              <p>It excludes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>funds specifically provided for capital purposes</li>
              </ul>
            </div>
          </details>
        </>
      ) : (
        <Loading />
      )}
    </>
  );
};
