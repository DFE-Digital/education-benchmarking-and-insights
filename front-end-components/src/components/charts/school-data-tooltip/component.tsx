import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { SchoolTooltipProps } from "src/components/charts/school-data-tooltip";
import { SchoolChartData } from "../table-chart";
import { useMemo } from "react";

export function SchoolDataTooltip<
  TValue extends ValueType,
  TName extends NameType,
>({ active, payload, specialItemFlags }: SchoolTooltipProps<TValue, TName>) {
  const key = useMemo(() => {
    if (!payload || !payload.length) {
      return null;
    }

    return (payload[0].payload as SchoolChartData).urn;
  }, [payload]);

  const partYear = useMemo(() => {
    return (
      key && specialItemFlags && specialItemFlags(key).includes("partYear")
    );
  }, [key, specialItemFlags]);

  if (!active || !payload || !payload.length) {
    return null;
  }

  const { laName, schoolName, schoolType, totalPupils } = payload[0]
    .payload as SchoolChartData;
  return (
    <>
      {partYear && (
        <div className="tooltip-part-year-warning">
          <div className="govuk-warning-text govuk-!-margin-0">
            <span className="govuk-warning-text__icon" aria-hidden="true">
              !
            </span>
            <strong className="govuk-warning-text__text">
              <span className="govuk-visually-hidden">Warning</span>
              This school doesn't have a complete set of financial data for this
              period.
            </strong>
          </div>
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
