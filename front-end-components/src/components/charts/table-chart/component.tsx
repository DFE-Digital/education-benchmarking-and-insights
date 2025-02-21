import React, { useContext, useMemo } from "react";
import {
  LaChartData,
  SchoolChartData,
  TableChartProps,
  TrustChartData,
} from "src/components/charts/table-chart";
import {
  SelectedEstablishmentContext,
  useCentralServicesBreakdownContext,
  SuppressNegativeOrZeroContext,
} from "src/contexts";
import { fullValueFormatter } from "../utils";
import { BreakdownInclude } from "src/components/central-services-breakdown";
import { PartYearDataWarning } from "../part-year-data-warning/component";
import { ErrorBanner } from "src/components/error-banner";
import classNames from "classnames";
import "./styles.scss";

export const TableChart: React.FC<
  TableChartProps<SchoolChartData | TrustChartData | LaChartData>
> = ({
  data,
  localAuthority,
  preventFocus,
  tableHeadings,
  trust,
  valueUnit,
}) => {
  const selectedEstablishment = useContext(SelectedEstablishmentContext);
  const { breakdown } = useCentralServicesBreakdownContext();
  const { suppressNegativeOrZero, message } = useContext(
    SuppressNegativeOrZeroContext
  );

  // todo: move 'name' cell to separate component
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

  const renderLaAnchor = (row: LaChartData) => (
    <a
      className="govuk-link govuk-link--no-visited-state"
      href={`/local-authority/${row.laCode}`}
      tabIndex={preventFocus ? -1 : undefined}
    >
      {row.laName}
    </a>
  );

  const dataPointKey = (trust ? "totalValue" : "value") as keyof (
    | SchoolChartData
    | TrustChartData
    | LaChartData
  );

  const keyField = (
    localAuthority ? "laCode" : trust ? "companyNumber" : "urn"
  ) as keyof (SchoolChartData | TrustChartData | LaChartData);

  const filteredData = useMemo(() => {
    return data?.filter((d) =>
      suppressNegativeOrZero
        ? (d[dataPointKey] as number) > 0 ||
          d[keyField] === selectedEstablishment
        : true
    );
  }, [
    data,
    suppressNegativeOrZero,
    dataPointKey,
    keyField,
    selectedEstablishment,
  ]);

  return (
    <>
      {filteredData && data && (
        <ErrorBanner
          isRendered={
            suppressNegativeOrZero && filteredData.length < data.length
          }
          message={message}
        />
      )}
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
          {filteredData &&
            filteredData.map((row) => {
              const schoolRow = row as SchoolChartData;
              const trustRow = row as TrustChartData;
              const laRow = row as LaChartData;
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
              const { laCode } = laRow;
              const additionalData = schoolRow.urn
                ? {
                    laName,
                    schoolType,
                    totalPupils,
                  }
                : {};

              return (
                <tr
                  key={urn ?? companyNumber ?? laCode}
                  className="govuk-table__row"
                >
                  {localAuthority ? (
                    <td className="govuk-table__cell">
                      {selectedEstablishment == laCode ? (
                        <strong>{renderLaAnchor(laRow)}</strong>
                      ) : (
                        renderLaAnchor(laRow)
                      )}
                    </td>
                  ) : trust ? (
                    <td className="govuk-table__cell">
                      {selectedEstablishment == companyNumber ? (
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
                      {selectedEstablishment == urn ? (
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
                    Object.values(additionalData).map((filteredData, i) => {
                      return (
                        <td key={i} className="govuk-table__cell">
                          {String(filteredData)}
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
    </>
  );
};
