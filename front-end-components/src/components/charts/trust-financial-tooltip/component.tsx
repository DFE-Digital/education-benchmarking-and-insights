import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { TrustFinancialTooltipProps } from "src/components/charts/trust-financial-tooltip";
import { TrustFinancial } from "src/services";
import { shortValueFormatter } from "../utils";

export function TrustFinancialTooltip<
  TValue extends ValueType,
  TName extends NameType,
>(props: TrustFinancialTooltipProps<TValue, TName>) {
  const { active, payload } = props;
  const format = (value: number) =>
    shortValueFormatter(value, { valueUnit: "currency" });

  if (active && payload && payload.length) {
    const trust = payload[0].payload as TrustFinancial;
    return (
      <table className="govuk-table govuk-table--small-text-until-tablet tooltip-table">
        <caption className="govuk-table__caption govuk-table__caption--s">
          {trust.name}
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
            <td className="govuk-table__cell">{format(trust.totalSpend)}</td>
          </tr>
          <tr className="govuk-table__row">
            <th scope="row" className="govuk-table__header">
              School spend
            </th>
            <td className="govuk-table__cell">{format(trust.schoolSpend)}</td>
          </tr>
          <tr className="govuk-table__row">
            <th scope="row" className="govuk-table__header">
              Central spend
            </th>
            <td className="govuk-table__cell">{format(trust.centralSpend)}</td>
          </tr>
        </tbody>
      </table>
    );
  }

  return null;
}
