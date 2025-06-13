import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { PayBandDataTooltipProps } from "src/components/charts/pay-band-tooltip";
import { TrustChartData } from "../table-chart";

export function PayBandDataTooltip<
  TValue extends ValueType,
  TName extends NameType,
>(props: PayBandDataTooltipProps<TValue, TName>) {
  const { active, payload, valueFormatter } = props;

  if (!active || !payload || !payload.length) {
    return null;
  }

  const { trustName, totalValue } = payload[0].payload as TrustChartData;
  const label = "Highest emolument band";
  return (
    <table className="govuk-table govuk-table--small-text-until-tablet tooltip-table">
      <caption className="govuk-table__caption govuk-table__caption--s">
        {trustName}
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
            {label}
          </th>
          {/* TODO: deal with null properly */}
          <td className="govuk-table__cell">
            {valueFormatter(totalValue ?? 0)}
          </td>
        </tr>
      </tbody>
    </table>
  );
}
