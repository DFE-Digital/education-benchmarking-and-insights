import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { TrustBalanceTooltipProps } from "src/components/charts/trust-balance-tooltip";
import { TrustBalance } from "src/services";
import { shortValueFormatter } from "../utils";

export function TrustBalanceTooltip<
  TValue extends ValueType,
  TName extends NameType,
>(props: TrustBalanceTooltipProps<TValue, TName>) {
  const { active, payload } = props;
  const format = (value: number) =>
    shortValueFormatter(value, { valueUnit: "currency" });

  if (active && payload && payload.length) {
    const trust = payload[0].payload as TrustBalance;
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
            <td className="govuk-table__cell">
              {format(trust.totalExpenditure)}
            </td>
          </tr>
          <tr className="govuk-table__row">
            <th scope="row" className="govuk-table__header">
              School spend
            </th>
            <td className="govuk-table__cell">
              {format(trust.schoolExpenditure)}
            </td>
          </tr>
          <tr className="govuk-table__row">
            <th scope="row" className="govuk-table__header">
              Central spend
            </th>
            <td className="govuk-table__cell">
              {format(trust.centralExpenditure)}
            </td>
          </tr>
        </tbody>
      </table>
    );
  }

  return null;
}
