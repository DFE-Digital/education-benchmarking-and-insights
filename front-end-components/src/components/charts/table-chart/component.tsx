import React, { useContext } from "react";
import { TableChartProps } from "src/components/charts/table-chart";
import { SelectedSchoolContext } from "src/contexts";

export const TableChart: React.FC<TableChartProps> = (props) => {
  const { tableHeadings, data } = props;
  const selectedSchool = useContext(SelectedSchoolContext);

  return (
    <table className="govuk-table">
      <thead className="govuk-table__head">
        <tr className="govuk-table__row">
          {tableHeadings.map((heading) => {
            return (
              <th scope="col" className="govuk-table__header">
                {heading}
              </th>
            );
          })}
        </tr>
      </thead>
      <tbody className="govuk-table__body">
        {data &&
          data.map((row) => {
            return (
              <tr className="govuk-table__row">
                <td className="govuk-table__cell">
                  {selectedSchool.urn == row.urn ? (
                    <strong>
                      <a
                        className="govuk-link govuk-link--no-visited-state"
                        href={`/school/${row.urn}`}
                      >
                        {row.school}
                      </a>
                    </strong>
                  ) : (
                    <a
                      className="govuk-link govuk-link--no-visited-state"
                      href={`/school/${row.urn}`}
                    >
                      {row.school}
                    </a>
                  )}
                </td>
                {row.additionalData &&
                  row.additionalData.map((data) => {
                    return (
                      <td className="govuk-table__cell">{String(data)}</td>
                    );
                  })}
                <td className="govuk-table__cell">{row.value.toFixed(2)}</td>
              </tr>
            );
          })}
      </tbody>
    </table>
  );
};
