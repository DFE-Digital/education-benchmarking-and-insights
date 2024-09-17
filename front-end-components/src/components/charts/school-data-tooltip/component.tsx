import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { SchoolTooltipProps } from "src/components/charts/school-data-tooltip";
import { SchoolChartData } from "../table-chart";
import { PartYearDataWarning } from "../part-year-data-warning";

export function SchoolDataTooltip<
  TValue extends ValueType,
  TName extends NameType,
>({ active, payload }: SchoolTooltipProps<TValue, TName>) {
  if (!active || !payload || !payload.length) {
    return null;
  }

  const { laName, periodCoveredByReturn, schoolName, schoolType, totalPupils } =
    payload[0].payload as SchoolChartData;
  return (
    <>
      {periodCoveredByReturn !== undefined && periodCoveredByReturn < 12 && (
        <div className="tooltip-part-year-warning">
          <PartYearDataWarning periodCoveredByReturn={periodCoveredByReturn} />
        </div>
      )}
      <table className="govuk-table govuk-table--small-text-until-tablet tooltip-table">
        <caption className="govuk-table__caption govuk-table__caption--s">
          {schoolName}
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
            <td className="govuk-table__cell">{laName}</td>
          </tr>
          <tr className="govuk-table__row">
            <th scope="row" className="govuk-table__header">
              School type
            </th>
            <td className="govuk-table__cell">{schoolType}</td>
          </tr>
          <tr className="govuk-table__row">
            <th scope="row" className="govuk-table__header">
              Number of pupils
            </th>
            <td className="govuk-table__cell">{String(totalPupils)}</td>
          </tr>
        </tbody>
      </table>
    </>
  );
}
