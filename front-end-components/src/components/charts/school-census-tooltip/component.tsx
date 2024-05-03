import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { SchoolTooltipProps } from "src/components/charts/school-census-tooltip";
import { Census } from "src/services";

export function SchoolCensusTooltip<
  TValue extends ValueType,
  TName extends NameType,
>(props: SchoolTooltipProps<TValue, TName>) {
  const { active, payload } = props;
  if (active && payload && payload.length) {
    const census = payload[0].payload as Census;
    return (
      <table className="govuk-table govuk-table--small-text-until-tablet tooltip-table">
        <caption className="govuk-table__caption govuk-table__caption--s">
          {census.name}
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
              Local authority
            </th>
            <td className="govuk-table__cell">{census.localAuthority}</td>
          </tr>
          <tr className="govuk-table__row">
            <th scope="row" className="govuk-table__header">
              School type
            </th>
            <td className="govuk-table__cell">{census.schoolType}</td>
          </tr>
          <tr className="govuk-table__row">
            <th scope="row" className="govuk-table__header">
              Number of pupils
            </th>
            <td className="govuk-table__cell">
              {String(census.numberOfPupils)}
            </td>
          </tr>
        </tbody>
      </table>
    );
  }

  return null;
}
