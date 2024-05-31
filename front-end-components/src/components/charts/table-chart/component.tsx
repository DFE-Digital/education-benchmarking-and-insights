import React, { useContext } from "react";
import {
  SchoolChartData,
  TableChartProps,
} from "src/components/charts/table-chart";
import { SelectedSchoolContext } from "src/contexts";
import { fullValueFormatter } from "../utils";

export const TableChart: React.FC<TableChartProps<SchoolChartData>> = (
  props
) => {
  const { tableHeadings, data, preventFocus, valueUnit } = props;
  const selectedSchool = useContext(SelectedSchoolContext);

  const renderSchoolAnchor = (row: SchoolChartData) => (
    <a
      className="govuk-link govuk-link--no-visited-state"
      href={`/school/${row.urn}`}
      tabIndex={preventFocus ? -1 : undefined}
    >
      {row.name}
    </a>
  );

  return (
    <table className="govuk-table">
      <thead className="govuk-table__head">
        <tr className="govuk-table__row">
          {tableHeadings.map((heading, i) => {
            return (
              <th key={i} scope="col" className="govuk-table__header">
                {heading}
              </th>
            );
          })}
        </tr>
      </thead>
      <tbody className="govuk-table__body">
        {data &&
          data.map((row) => {
            const { localAuthority, schoolType, numberOfPupils } = row;
            const additionalData = {
              localAuthority,
              schoolType,
              numberOfPupils,
            };

            return (
              <tr key={row.urn} className="govuk-table__row">
                <td className="govuk-table__cell">
                  {selectedSchool.urn == row.urn ? (
                    <strong>{renderSchoolAnchor(row)}</strong>
                  ) : (
                    renderSchoolAnchor(row)
                  )}
                </td>
                {additionalData &&
                  Object.values(additionalData).map((data, i) => {
                    return (
                      <td key={i} className="govuk-table__cell">
                        {String(data)}
                      </td>
                    );
                  })}
                <td className="govuk-table__cell">
                  {fullValueFormatter(row.value, { valueUnit })}
                </td>
              </tr>
            );
          })}
      </tbody>
    </table>
  );
};
