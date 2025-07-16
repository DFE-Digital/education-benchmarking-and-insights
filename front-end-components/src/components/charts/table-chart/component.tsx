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
import { ErrorBanner } from "src/components/error-banner";
import "./styles.scss";
import { TableCellEstablishmentName } from "./partials";

export const TableChart: React.FC<
  TableChartProps<SchoolChartData | TrustChartData | LaChartData>
> = ({
  data,
  linkToEstablishment,
  localAuthority,
  preventFocus,
  tableHeadings,
  trust,
  valueUnit,
  valueFormatter,
}) => {
  const selectedEstablishment = useContext(SelectedEstablishmentContext);
  const { breakdown } = useCentralServicesBreakdownContext();
  const { suppressNegativeOrZero, message } = useContext(
    SuppressNegativeOrZeroContext
  );

  const resolvedValueFormatter = valueFormatter ?? fullValueFormatter;

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
              const { laName, schoolType, totalPupils, urn, value } = schoolRow;
              const { totalValue, schoolValue, centralValue, companyNumber } =
                trustRow;
              const { budget, laCode, population } = laRow;
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
                  <TableCellEstablishmentName
                    linkToEstablishment={linkToEstablishment}
                    localAuthority={localAuthority}
                    preventFocus={preventFocus}
                    row={row}
                    trust={trust}
                  />
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
                        {resolvedValueFormatter(totalValue, {
                          valueUnit,
                          forDisplay: true,
                        })}
                      </td>
                      {breakdown === BreakdownInclude &&
                        schoolValue !== undefined &&
                        centralValue !== undefined && (
                          <>
                            <td className="govuk-table__cell table-cell-value">
                              {resolvedValueFormatter(schoolValue, {
                                valueUnit,
                              })}
                            </td>
                            <td className="govuk-table__cell table-cell-value">
                              {resolvedValueFormatter(centralValue, {
                                valueUnit,
                              })}
                            </td>
                          </>
                        )}
                    </>
                  ) : localAuthority ? (
                    <>
                      <td className="govuk-table__cell table-cell-value">
                        {resolvedValueFormatter(value, {
                          valueUnit,
                        })}
                      </td>
                      {budget !== undefined && (
                        <td className="govuk-table__cell table-cell-value">
                          {resolvedValueFormatter(budget, {
                            valueUnit,
                          })}
                        </td>
                      )}
                      <td className="govuk-table__cell table-cell-value">
                        {resolvedValueFormatter(population, {
                          valueUnit: "amount",
                        })}
                      </td>
                    </>
                  ) : (
                    <td className="govuk-table__cell table-cell-value">
                      {resolvedValueFormatter(value, {
                        valueUnit,
                        forDisplay: true,
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
