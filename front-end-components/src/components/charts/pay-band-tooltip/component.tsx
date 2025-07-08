import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { PayBandDataTooltipProps } from "src/components/charts/pay-band-tooltip";
import { TrustChartData } from "../table-chart";
import { payBandFormatter, statValueFormatter } from "../utils";

export function PayBandDataTooltip<
  TValue extends ValueType,
  TName extends NameType,
>(props: PayBandDataTooltipProps<TValue, TName>) {
  const { active, payload } = props;

  if (!active || !payload || !payload.length) {
    return null;
  }

  const { trustName, totalValue } = payload[0].payload as TrustChartData;
  const { totalPupils } = payload[0].payload;
  const label = "Highest emolument band";
  return (
    totalValue && (
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
            <td className="govuk-table__cell">
              {payBandFormatter(totalValue)}
            </td>
          </tr>
          <tr className="govuk-table__row">
            <th scope="row" className="govuk-table__header">
              Pupil numbers
            </th>
            <td className="govuk-table__cell">
              {statValueFormatter(totalPupils)}
            </td>
          </tr>
        </tbody>
      </table>
    )
  );
}
