import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { TrustDataTooltipProps } from "src/components/charts/trust-data-tooltip";
import { shortValueFormatter } from "../utils";
import { TrustChartData } from "../table-chart";

export function TrustDataTooltip<
  TValue extends ValueType,
  TName extends NameType,
>(props: TrustDataTooltipProps<TValue, TName>) {
  const { active, payload, valueUnit } = props;
  const format = (value?: number) =>
    value === undefined ? "" : shortValueFormatter(value, { valueUnit });

  if (active && payload && payload.length) {
    const trust = payload[0].payload as TrustChartData;
    return (
      <table className="govuk-table govuk-table--small-text-until-tablet tooltip-table">
        <caption className="govuk-table__caption govuk-table__caption--s">
          {trust.trustName}
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
              Total spend
            </th>
            <td className="govuk-table__cell">{format(trust.totalValue)}</td>
          </tr>
          <tr className="govuk-table__row">
            <th scope="row" className="govuk-table__header">
              School spend
            </th>
            <td className="govuk-table__cell">{format(trust.schoolValue)}</td>
          </tr>
          <tr className="govuk-table__row">
            <th scope="row" className="govuk-table__header">
              Central spend
            </th>
            <td className="govuk-table__cell">{format(trust.centralValue)}</td>
          </tr>
        </tbody>
      </table>
    );
  }

  return null;
}
