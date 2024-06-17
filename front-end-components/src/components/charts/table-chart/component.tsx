import React, { useContext } from "react";
import {
  SchoolChartData,
  TableChartProps,
  TrustChartData,
} from "src/components/charts/table-chart";
import { SelectedEstablishmentContext } from "src/contexts";
import { fullValueFormatter } from "../utils";

export const TableChart: React.FC<
  TableChartProps<SchoolChartData | TrustChartData>
> = (props) => {
  const { tableHeadings, data, preventFocus, valueUnit } = props;
  const selectedSchool = useContext(SelectedEstablishmentContext);

  const renderSchoolAnchor = (row: SchoolChartData) => (
    <a
      className="govuk-link govuk-link--no-visited-state"
      href={`/school/${row.urn}`}
      tabIndex={preventFocus ? -1 : undefined}
    >
      {row.schoolName}
    </a>
  );

  const renderTrustAnchor = (row: TrustChartData) => (
    <a
      className="govuk-link govuk-link--no-visited-state"
      href={`/trust/${row.companyNumber}`}
      tabIndex={preventFocus ? -1 : undefined}
    >
      {row.trustName}
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
            const school = row as SchoolChartData;
            const trust = row as TrustChartData;
            const { laName, schoolType, totalPupils, urn, value } = school;
            const { totalValue, schoolValue, centralValue, companyNumber } =
              trust;
            const additionalData = school.urn
              ? {
                  laName,
                  schoolType,
                  totalPupils,
                }
              : {};

            return (
              <tr key={urn ?? companyNumber} className="govuk-table__row">
                {urn ? (
                  <td className="govuk-table__cell">
                    {selectedSchool == urn ? (
                      <strong>{renderSchoolAnchor(school)}</strong>
                    ) : (
                      renderSchoolAnchor(school)
                    )}
                  </td>
                ) : (
                  <td className="govuk-table__cell">
                    {selectedSchool == companyNumber ? (
                      <strong>{renderTrustAnchor(trust)}</strong>
                    ) : (
                      renderTrustAnchor(trust)
                    )}
                  </td>
                )}
                {additionalData &&
                  Object.values(additionalData).map((data, i) => {
                    return (
                      <td key={i} className="govuk-table__cell">
                        {String(data)}
                      </td>
                    );
                  })}
                {urn && (
                  <td className="govuk-table__cell">
                    {fullValueFormatter(value, {
                      valueUnit,
                    })}
                  </td>
                )}
                {companyNumber && (
                  <>
                    <td className="govuk-table__cell">
                      {fullValueFormatter(totalValue, {
                        valueUnit,
                      })}
                    </td>
                    <td className="govuk-table__cell">
                      {fullValueFormatter(schoolValue, {
                        valueUnit,
                      })}
                    </td>
                    <td className="govuk-table__cell">
                      {fullValueFormatter(centralValue, {
                        valueUnit,
                      })}
                    </td>
                  </>
                )}
              </tr>
            );
          })}
      </tbody>
    </table>
  );
};
