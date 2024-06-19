import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { TrustDataTooltipProps } from "src/components/charts/trust-data-tooltip";
import { shortValueFormatter } from "../utils";
import { TrustChartData } from "../table-chart";
import { useCentralServicesBreakdownContext } from "src/contexts";
import { BreakdownInclude } from "src/components/central-services-breakdown";

export function TrustDataTooltip<
  TValue extends ValueType,
  TName extends NameType,
>(props: TrustDataTooltipProps<TValue, TName>) {
  const { active, payload, valueUnit } = props;
  const format = (value?: number) =>
    value === undefined ? "" : shortValueFormatter(value, { valueUnit });
  const { breakdown } = useCentralServicesBreakdownContext(true);

  if (active && payload && payload.length) {
    const { trustName, totalValue, schoolValue, centralValue, type } =
      payload[0].payload as TrustChartData;
    const label = type === "balance" ? type : "spend";
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
          {breakdown === BreakdownInclude ? (
            <>
              <tr className="govuk-table__row">
                <th scope="row" className="govuk-table__header">
                  Overall {label}
                </th>
                <td className="govuk-table__cell">{format(totalValue)}</td>
              </tr>
              <tr className="govuk-table__row">
                <th scope="row" className="govuk-table__header">
                  School {label}
                </th>
                <td className="govuk-table__cell">{format(schoolValue)}</td>
              </tr>
              <tr className="govuk-table__row">
                <th scope="row" className="govuk-table__header">
                  Central {label}
                </th>
                <td className="govuk-table__cell">{format(centralValue)}</td>
              </tr>
            </>
          ) : (
            <tr className="govuk-table__row">
              <th scope="row" className="govuk-table__header">
                Total {label}
              </th>
              <td className="govuk-table__cell">{format(totalValue)}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  return null;
}
