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
      <h2 className="govuk-heading-m">Forecasting accuracy</h2>
      <p className="govuk-body">
        The below table shows forecasting accuracy. It compares forecasted
        revenue reserves from the budget forecast return (BFR), with the actual
        revenue reserves from the accounts return (AR) for the same period.
      </p>
      <h3 className="govuk-heading-s">Variance status</h3>
      <p className="govuk-body">
        We compare the most recent forecast for each period. The variance status
        shows whether your forecasting is under or over.
      </p>
      <details className="govuk-details">
        <summary className="govuk-details__summary">
          <span className="govuk-details__summary-text">
            Variance status thresholds
          </span>
        </summary>
        <div className="govuk-details__text">
          <table className="govuk-table">
            <thead className="govuk-table__head">
              <tr className="govuk-table__row">
                <th className="govuk-table__header">Variance status</th>
                <th className="govuk-table__header">Variance threshold</th>
              </tr>
            </thead>
            <tbody className="govuk-table__body">
              <tr className="govuk-table__row">
                <td className="govuk-table__cell">
                  AR significantly below forecast
                </td>
                <td className="govuk-table__cell">Below −10%</td>
              </tr>
              <tr className="govuk-table__row">
                <td className="govuk-table__cell">AR below forecast</td>
                <td className="govuk-table__cell">Between −10% and −5%</td>
              </tr>
              <tr className="govuk-table__row">
                <td className="govuk-table__cell">Stable forecast</td>
                <td className="govuk-table__cell">Between −5% and 5%</td>
              </tr>
              <tr className="govuk-table__row">
                <td className="govuk-table__cell">AR above forecast</td>
                <td className="govuk-table__cell">Between 5% and 10%</td>
              </tr>
              <tr className="govuk-table__row">
                <td className="govuk-table__cell">
                  AR significantly above forecast
                </td>
                <td className="govuk-table__cell">Above 10% </td>
              </tr>
            </tbody>
          </table>
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
              <th className="govuk-table__header">Variance percentage</th>
              <th className="govuk-table__header">Variance status</th>
            </tr>
          </thead>
          <tbody className="govuk-table__body">
            {data.map((item) => {
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
                  <td className="govuk-table__cell">{item.varianceStatus}</td>
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
