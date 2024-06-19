import React, { useCallback, useEffect, useState } from "react";
import {
  Actual,
  ChartDimensions,
  ChartMode,
  ChartModeChart,
  CostCategories,
} from "src/components";
import {
  ChartDimensionContext,
  ChartModeProvider,
  useChartModeContext,
} from "src/contexts";
import { SchoolBalanceHistory, BalanceApi } from "src/services";
import { HistoricChart } from "src/composed/historic-chart-composed";
import { Loading } from "src/components/loading";

export const BalanceSection: React.FC<{ type: string; id: string }> = ({
  type,
  id,
}) => {
  const defaultDimension = Actual;
  const { chartMode, setChartMode } = useChartModeContext();
  const [dimension, setDimension] = useState(defaultDimension);
  const [data, setData] = useState(new Array<SchoolBalanceHistory>());
  const getData = useCallback(async () => {
    setData(new Array<SchoolBalanceHistory>());
    return await BalanceApi.history(type, id, dimension.value);
  }, [type, id, dimension]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    const dimension =
      CostCategories.find((x) => x.value === event.target.value) ??
      defaultDimension;
    setDimension(dimension);
  };

  return (
    <ChartModeProvider initialValue={ChartModeChart}>
      <ChartDimensionContext.Provider value={dimension}>
        <div className="govuk-grid-row">
          <div className="govuk-grid-column-two-thirds">
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="balance"
              defaultValue={dimension.value}
            />
          </div>
          <div className="govuk-grid-column-one-third">
            <ChartMode
              chartMode={chartMode}
              handleChange={setChartMode}
              prefix="balance"
            />
          </div>
        </div>
        <hr className="govuk-section-break govuk-section-break--l govuk-section-break--visible govuk-!-margin-top-0" />
        {data.length > 0 ? (
          <>
            <HistoricChart
              chartName="In-year balance"
              data={data}
              seriesConfig={{
                inYearBalance: {
                  label: "In-year balance",
                  visible: true,
                },
              }}
              valueField="inYearBalance"
            >
              <h2 className="govuk-heading-m">In-year balance</h2>
            </HistoricChart>

            <HistoricChart
              chartName="Revenue reserve"
              data={data}
              seriesConfig={{
                revenueReserve: {
                  label: "Revenue reserve",
                  visible: true,
                },
              }}
              valueField="revenueReserve"
            >
              <h2 className="govuk-heading-m">Revenue reserve</h2>
            </HistoricChart>

            <details className="govuk-details">
              <summary className="govuk-details__summary">
                <span className="govuk-details__summary-text">
                  More about revenue reserve
                </span>
              </summary>
              <div className="govuk-details__text">
                <p>
                  Local authority maintained schools and single academy trusts
                  <br />
                  Reserves are legally associated with one school and appear in
                  that school’s graphs.
                </p>
                <p>
                  Local authority maintained schools
                  <br />
                  Reserves include committed and uncommitted revenue balance.
                  They also include the community-focused extended school
                  revenue balance.
                </p>

                <p>
                  Single academy trusts
                  <br />
                  This is calculated by:
                  <ul className="govuk-list govuk-list--bullet">
                    <li>
                      carrying forward the closing balance (restricted and
                      unrestricted funds) from the previous year
                    </li>
                    <li>
                      adding total income in the current year (revenue, funds
                      inherited on conversion/transfer and contributions from
                      academies to trust)
                    </li>
                    <li>subtracting total expenditure in the current year</li>
                  </ul>
                </p>
                <p>
                  Multi-academy trust
                  <br />
                  The trust is the legal entity and all revenue reserves legally
                  belong to it.
                </p>
                <p>
                  Single academies in multi-academy trusts (MATs)
                  <br />
                  We estimated a value per academy by diving up and sharing out
                  the trust’s reserves on a pro-rata basis. For this we used the
                  full-time equivalent (FTE) number of pupils in each academy in
                  that MAT.
                </p>
              </div>
            </details>
          </>
        ) : (
          <Loading />
        )}
      </ChartDimensionContext.Provider>
    </ChartModeProvider>
  );
};
