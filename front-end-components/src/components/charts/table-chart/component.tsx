import React, { useContext } from "react";
import {
  SchoolChartData,
  TableChartProps,
  TrustChartData,
} from "src/components/charts/table-chart";
import {
  SelectedEstablishmentContext,
  useCentralServicesBreakdownContext,
} from "src/contexts";
import { fullValueFormatter } from "../utils";
import { BreakdownInclude } from "src/components/central-services-breakdown";

export const TableChart: React.FC<
  TableChartProps<SchoolChartData | TrustChartData>
> = (props) => {
  const { tableHeadings, data, preventFocus, valueUnit, trust } = props;
  const selectedSchool = useContext(SelectedEstablishmentContext);
  const { breakdown } = useCentralServicesBreakdownContext();

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
            const schoolRow = row as SchoolChartData;
            const trustRow = row as TrustChartData;
            const { laName, schoolType, totalPupils, urn, value } = schoolRow;
            const { totalValue, schoolValue, centralValue, companyNumber } =
              trustRow;
            const additionalData = schoolRow.urn
              ? {
                  laName,
                  schoolType,
                  totalPupils,
                }
              : {};

            return (
              <tr key={urn ?? companyNumber} className="govuk-table__row">
                {trust ? (
                  <td className="govuk-table__cell">
                    {selectedSchool == companyNumber ? (
                      <strong>{renderTrustAnchor(trustRow)}</strong>
                    ) : (
                      renderTrustAnchor(trustRow)
                    )}
                  </td>
                ) : (
                  <td className="govuk-table__cell">
                    {selectedSchool == urn ? (
                      <strong>{renderSchoolAnchor(schoolRow)}</strong>
                    ) : (
                      renderSchoolAnchor(schoolRow)
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
                {trust ? (
                  <>
                    <td className="govuk-table__cell">
                      {fullValueFormatter(totalValue, {
                        valueUnit,
                      })}
                    </td>
                    {breakdown === BreakdownInclude && (
                      <>
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
                  </>
                ) : (
                  <td className="govuk-table__cell">
                    {fullValueFormatter(value, {
                      valueUnit,
                    })}
                  </td>
                )}
              </tr>
            );
          })}
      </tbody>
    </table>
  );
};
