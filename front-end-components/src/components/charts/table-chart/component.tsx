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
import { PartYearDataWarning } from "../part-year-data-warning/component";
import classNames from "classnames";

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
            const {
              laName,
              periodCoveredByReturn,
              schoolType,
              totalPupils,
              urn,
              value,
            } = schoolRow;
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
                  <td
                    className={classNames("govuk-table__cell", {
                      "table-cell-warning":
                        periodCoveredByReturn !== undefined &&
                        periodCoveredByReturn < 12,
                    })}
                  >
                    {selectedSchool == urn ? (
                      <strong>{renderSchoolAnchor(schoolRow)}</strong>
                    ) : (
                      renderSchoolAnchor(schoolRow)
                    )}
                    {periodCoveredByReturn !== undefined &&
                      periodCoveredByReturn < 12 && (
                        <PartYearDataWarning
                          periodCoveredByReturn={periodCoveredByReturn}
                        />
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
                    <td className="govuk-table__cell table-cell-value">
                      {fullValueFormatter(totalValue, {
                        valueUnit,
                      })}
                    </td>
                    {breakdown === BreakdownInclude &&
                      schoolValue !== undefined &&
                      centralValue !== undefined && (
                        <>
                          <td className="govuk-table__cell table-cell-value">
                            {fullValueFormatter(schoolValue, {
                              valueUnit,
                            })}
                          </td>
                          <td className="govuk-table__cell table-cell-value">
                            {fullValueFormatter(centralValue, {
                              valueUnit,
                            })}
                          </td>
                        </>
                      )}
                  </>
                ) : (
                  <td className="govuk-table__cell table-cell-value">
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
