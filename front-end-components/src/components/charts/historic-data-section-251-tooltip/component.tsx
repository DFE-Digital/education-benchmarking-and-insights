import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { HistoricDataSection251TooltipProps } from "src/components/charts/historic-data-section-251-tooltip";
import { Section251HistoryValue } from "src/composed/historic-chart-section-251-composed";
import { ChartDataSeries } from "../types";

export function HistoricDataSection251Tooltip<
  TData extends ChartDataSeries,
  TValue extends ValueType,
  TName extends NameType,
>({
  active,
  payload,
  valueFormatter,
  valueUnit,
}: HistoricDataSection251TooltipProps<TData, TValue, TName>) {
  if (!active || !payload || !payload.length) {
    return null;
  }

  const { term, outturn, budget } = payload[0]
    .payload as Section251HistoryValue;
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
              Outturn
            </th>
            <td className="govuk-table__cell">
              {outturn === undefined
                ? ""
                : valueFormatter
                  ? valueFormatter(outturn, { valueUnit })
                  : String(outturn)}
            </td>
          </tr>
          <tr className="govuk-table__row">
            <th scope="row" className="govuk-table__header">
              Planned expenditure
            </th>
            <td className="govuk-table__cell">
              {budget === undefined
                ? ""
                : valueFormatter
                  ? valueFormatter(budget, { valueUnit })
                  : String(budget)}
            </td>
          </tr>
        </tbody>
      </table>
    </>
  );
}
