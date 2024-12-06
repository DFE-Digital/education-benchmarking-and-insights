import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { HistoricTooltipProps } from "src/components/charts/historic-data-tooltip";
import { SchoolHistoryValue } from "src/composed/historic-chart-2-composed";
import { ChartDataSeries } from "../types";

export function HistoricDataTooltip<
  TData extends ChartDataSeries,
  TValue extends ValueType,
  TName extends NameType,
>({
  active,
  dimension,
  payload,
  valueFormatter,
  valueUnit,
}: HistoricTooltipProps<TData, TValue, TName>) {
  if (!active || !payload || !payload.length) {
    return null;
  }

  const { term, school, comparatorSetAverage, nationalAverage } = payload[0]
    .payload as SchoolHistoryValue;
  return (
    <>
      <table className="govuk-table govuk-table--small-text-until-tablet tooltip-table">
        <caption className="govuk-table__caption govuk-table__caption--s">
          {term}
        </caption>
        <thead className="govuk-table__head govuk-visually-hidden">
          <tr className="govuk-table__row">
            <th scope="col" className="govuk-table__header">
              Item
            </th>
            <th scope="col" className="govuk-table__header">
              Value
            </th>
          </tr>
        </thead>
        <tbody className="govuk-table__body">
          <tr className="govuk-table__row">
            <th scope="row" className="govuk-table__header">
              {dimension}
            </th>
            <td className="govuk-table__cell">
              {school === undefined
                ? ""
                : valueFormatter
                  ? valueFormatter(school, { valueUnit })
                  : String(school)}
            </td>
          </tr>
          <tr className="govuk-table__row">
            <th scope="row" className="govuk-table__header">
              Average across comparator set
            </th>
            <td className="govuk-table__cell">
              {comparatorSetAverage === undefined
                ? ""
                : valueFormatter
                  ? valueFormatter(comparatorSetAverage, { valueUnit })
                  : String(comparatorSetAverage)}
            </td>
          </tr>
          <tr className="govuk-table__row">
            <th scope="row" className="govuk-table__header">
              National average across phase type
            </th>
            <td className="govuk-table__cell">
              {nationalAverage === undefined
                ? ""
                : valueFormatter
                  ? valueFormatter(nationalAverage, { valueUnit })
                  : String(nationalAverage)}
            </td>
          </tr>
        </tbody>
      </table>
    </>
  );
}
