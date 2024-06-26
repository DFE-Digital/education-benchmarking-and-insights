import { format } from "date-fns";
import { BfrTableProps } from "./types";
import {
  fullValueFormatter,
  shortValueFormatter,
} from "src/components/charts/utils";
import { Loading } from "src/components/loading";

export const BfrTable = ({ data }: BfrTableProps) => {
  return (
    <div className="govuk-grid-column-full govuk-!-margin-top-5">
      <h2 className="govuk-heading-m">
        Year-end balances and forecasting variance
      </h2>
      <p className="govuk-body">
        The analysis in the table indicates forecasting accuracy. To assess the
        accuracy of your forecasting, the forecasted revenue reserve balances
        from the budget forecast return are compared with the actual closing
        balances from the accounts return for the same period.
      </p>
      <details className="govuk-details">
        <summary className="govuk-details__summary">
          <span className="govuk-details__summary-text">
            Help with variance status
          </span>
        </summary>
        <div className="govuk-details__text">
          <p className="govuk-body">
            We compare the most recent forecast for any given period. You'll see
            a variance status indicated whether you're forecasting is under or
            over.
          </p>
          <p className="govuk-body">
            The thresholds for the variance between AR and BFR are:
          </p>
          <ul className="govuk-list">
            <li>Below -5%: AR below forecast</li>
            <li>Between -5% and 5%: stable forecast</li>
            <li>Between 5% and 10%: AR above forecast</li>
            <li>Above 10%: AR significantly above forecast</li>
          </ul>
        </div>
      </details>
      {data && Array.isArray(data) ? (
        <table className="govuk-table">
          <thead className="govuk-table__head">
            <tr className="govuk-table__row">
              <th className="govuk-table__header">Period end date</th>
              <th className="govuk-table__header">Forecast reserves</th>
              <th className="govuk-table__header">Actual reserves</th>
              <th className="govuk-table__header">Difference</th>
              <th className="govuk-table__header">Variance %</th>
              <th className="govuk-table__header">Variance status</th>
            </tr>
          </thead>
          <tbody className="govuk-table__body">
            {data.map((item) => {
              let status = "";
              if (item.percentVariance !== undefined) {
                if (item.percentVariance < -5) {
                  status = "AR below forecast";
                }
                if (item.percentVariance >= -5 && item.percentVariance < 5) {
                  status = "Stable forecast";
                }
                if (item.percentVariance >= 5 && item.percentVariance < 10) {
                  status = "AR above forecast";
                }
                if (item.percentVariance >= 10) {
                  status = "AR significantly above forecast";
                }
              }
              return (
                <tr className="govuk-table__row">
                  <td className="govuk-table__cell">
                    {format(new Date(item.year, 7, 31), "d MMM yyyy")}
                  </td>
                  <td className="govuk-table__cell">
                    {item.forecast &&
                      fullValueFormatter(item.forecast, {
                        valueUnit: "currency",
                      })}
                  </td>
                  <td className="govuk-table__cell">
                    {item.actual &&
                      fullValueFormatter(item.actual, {
                        valueUnit: "currency",
                      })}
                  </td>
                  <td className="govuk-table__cell">
                    {item.variance &&
                      fullValueFormatter(item.variance, {
                        valueUnit: "currency",
                      })}
                  </td>
                  <td className="govuk-table__cell">
                    {item.percentVariance &&
                      shortValueFormatter(item.percentVariance, {
                        valueUnit: "%",
                      })}
                  </td>
                  <td className="govuk-table__cell">{status}</td>
                </tr>
              );
            })}
          </tbody>
        </table>
      ) : (
        <Loading />
      )}
    </div>
  );
};
