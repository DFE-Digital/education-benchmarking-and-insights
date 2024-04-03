import React, { useCallback, useEffect, useState } from "react";
import {
  Actual,
  ChartDimensions,
  ChartMode,
  ChartModeChart,
  CostCategories,
} from "src/components";
import { Expenditure, HistoryApi } from "src/services";
import { ChartDimensionContext, ChartModeContext } from "src/contexts";
import { LineChart } from "src/components/charts/line-chart";
import { shortValueFormatter } from "src/components/charts/utils.ts";
import { LineChartTooltip } from "src/components/charts/line-chart-tooltip";
import { ResolvedStat } from "src/components/charts/resolved-stat";
import { Loading } from "src/components/loading";
import { SpendingSectionTeachingCosts } from "src/views/historic-data/partials/spending-section-teaching-costs";
import { SpendingSectionNonEducationalStaffCosts } from "src/views/historic-data/partials/spending-section-non-educational-staff-costs";
import { SpendingSectionEducationalSupplies } from "src/views/historic-data/partials/spending-section-educational-supplies";
import { SpendingSectionEducationalIct } from "src/views/historic-data/partials/spending-section-educational-ict";
import { SpendingSectionPremisesServices } from "src/views/historic-data/partials/spending-section-premises-services";
import { SpendingSectionUtilities } from "src/views/historic-data/partials/spending-section-utilities.tsx";
import { SpendingSectionAdministrativeSupplies } from "src/views/historic-data/partials/spending-section-administrative-supplies";
import { SpendingSectionCateringServices } from "src/views/historic-data/partials/spending-section-catering-services";
import { SpendingSectionOther } from "src/views/historic-data/partials/spending-section-other";

export const SpendingSection: React.FC<{ type: string; id: string }> = ({
  type,
  id,
}) => {
  const defaultDimension = Actual;
  const [displayMode, setDisplayMode] = useState<string>(ChartModeChart);
  const [dimension, setDimension] = useState(defaultDimension);
  const [data, setData] = useState(new Array<Expenditure>());
  const getData = useCallback(async () => {
    setData(new Array<Expenditure>());
    return await HistoryApi.getExpenditure(type, id, dimension.value);
  }, [type, id, dimension]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const handleModeChange: React.ChangeEventHandler<HTMLInputElement> = (
    event
  ) => {
    setDisplayMode(event.target.value);
  };

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    const dimension =
      CostCategories.find((x) => x.value === event.target.value) ??
      defaultDimension;
    setDimension(dimension);
  };

  return (
    <ChartModeContext.Provider value={displayMode}>
      <ChartDimensionContext.Provider value={dimension}>
        <div className="govuk-grid-row">
          <div className="govuk-grid-column-two-thirds">
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="expenditure"
              defaultValue={dimension.value}
            />
          </div>
          <div className="govuk-grid-column-one-third">
            <ChartMode
              displayMode={displayMode}
              handleChange={handleModeChange}
              prefix="expenditure"
            />
          </div>
        </div>
        <hr className="govuk-section-break govuk-section-break--l govuk-section-break--visible govuk-!-margin-top-0" />
        {data.length > 0 ? (
          <>
            <h2 className="govuk-heading-m">Total spending and costs</h2>
            <div className="govuk-grid-row">
              <div className="govuk-grid-column-three-quarters">
                <div style={{ height: 200 }}>
                  <LineChart
                    chartName="Total spending and costs"
                    data={data}
                    grid
                    highlightActive
                    keyField="yearEnd"
                    margin={20}
                    seriesConfig={{
                      totalExpenditure: {
                        label: "Total expenditure",
                        visible: true,
                      },
                    }}
                    seriesLabel={dimension.label}
                    seriesLabelField="yearEnd"
                    valueFormatter={shortValueFormatter}
                    valueUnit={dimension.unit}
                    tooltip={(t) => (
                      <LineChartTooltip
                        {...t}
                        valueFormatter={(v) =>
                          shortValueFormatter(v, { valueUnit: dimension.unit })
                        }
                      />
                    )}
                  />
                </div>
              </div>
              <aside className="govuk-grid-column-one-quarter">
                <ResolvedStat
                  chartName="Most recent total spending and costs"
                  className="chart-stat-line-chart"
                  compactValue
                  data={data}
                  displayIndex={data.length - 1}
                  seriesLabelField="yearEnd"
                  valueField="totalExpenditure"
                  valueFormatter={shortValueFormatter}
                  valueUnit={dimension.unit}
                />
              </aside>
            </div>
          </>
        ) : (
          <Loading />
        )}
        <div
          className="govuk-accordion"
          data-module="govuk-accordion"
          id="accordion-expenditure"
        >
          <div className="govuk-accordion__section">
            <div className="govuk-accordion__section-header">
              <h2 className="govuk-accordion__section-heading">
                <span
                  className="govuk-accordion__section-button"
                  id="accordion-expenditure-heading-1"
                >
                  Teaching and teaching support costs
                </span>
              </h2>
            </div>
            <div
              id="accordion-expenditure-content-1"
              className="govuk-accordion__section-content"
            >
              <p className="govuk-body">
                <SpendingSectionTeachingCosts data={data} />
              </p>
            </div>
          </div>
          <div className="govuk-accordion__section">
            <div className="govuk-accordion__section-header">
              <h2 className="govuk-accordion__section-heading">
                <span
                  className="govuk-accordion__section-button"
                  id="accordion-expenditure-heading-2"
                >
                  Non-educational support staff
                </span>
              </h2>
            </div>
            <div
              id="accordion-expenditure-content-2"
              className="govuk-accordion__section-content"
            >
              <p className="govuk-body">
                <SpendingSectionNonEducationalStaffCosts data={data} />
              </p>
            </div>
          </div>
          <div className="govuk-accordion__section">
            <div className="govuk-accordion__section-header">
              <h2 className="govuk-accordion__section-heading">
                <span
                  className="govuk-accordion__section-button"
                  id="accordion-expenditure-heading-3"
                >
                  Educational supplies
                </span>
              </h2>
            </div>
            <div
              id="accordion-expenditure-content-3"
              className="govuk-accordion__section-content"
            >
              <p className="govuk-body">
                <SpendingSectionEducationalSupplies data={data} />
              </p>
            </div>
          </div>
          <div className="govuk-accordion__section">
            <div className="govuk-accordion__section-header">
              <h2 className="govuk-accordion__section-heading">
                <span
                  className="govuk-accordion__section-button"
                  id="accordion-expenditure-heading-4"
                >
                  Educational ICT
                </span>
              </h2>
            </div>
            <div
              id="accordion-expenditure-content-4"
              className="govuk-accordion__section-content"
            >
              <p className="govuk-body">
                <SpendingSectionEducationalIct data={data} />
              </p>
            </div>
          </div>
          <div className="govuk-accordion__section">
            <div className="govuk-accordion__section-header">
              <h2 className="govuk-accordion__section-heading">
                <span
                  className="govuk-accordion__section-button"
                  id="accordion-expenditure-heading-5"
                >
                  Premises and services
                </span>
              </h2>
            </div>
            <div
              id="accordion-expenditure-content-5"
              className="govuk-accordion__section-content"
            >
              <p className="govuk-body">
                <SpendingSectionPremisesServices data={data} />
              </p>
            </div>
          </div>
          <div className="govuk-accordion__section">
            <div className="govuk-accordion__section-header">
              <h2 className="govuk-accordion__section-heading">
                <span
                  className="govuk-accordion__section-button"
                  id="accordion-expenditure-heading-6"
                >
                  Utilities
                </span>
              </h2>
            </div>
            <div
              id="accordion-expenditure-content-6"
              className="govuk-accordion__section-content"
            >
              <p className="govuk-body">
                <SpendingSectionUtilities data={data} />
              </p>
            </div>
          </div>
          <div className="govuk-accordion__section">
            <div className="govuk-accordion__section-header">
              <h2 className="govuk-accordion__section-heading">
                <span
                  className="govuk-accordion__section-button"
                  id="accordion-expenditure-heading-7"
                >
                  Administrative supplies
                </span>
              </h2>
            </div>
            <div
              id="accordion-expenditure-content-7"
              className="govuk-accordion__section-content"
            >
              <p className="govuk-body">
                <SpendingSectionAdministrativeSupplies data={data} />
              </p>
            </div>
          </div>
          <div className="govuk-accordion__section">
            <div className="govuk-accordion__section-header">
              <h2 className="govuk-accordion__section-heading">
                <span
                  className="govuk-accordion__section-button"
                  id="accordion-expenditure-heading-8"
                >
                  Catering staff and services
                </span>
              </h2>
            </div>
            <div
              id="accordion-expenditure-content-8"
              className="govuk-accordion__section-content"
            >
              <p className="govuk-body">
                <SpendingSectionCateringServices data={data} />
              </p>
            </div>
          </div>
          <div className="govuk-accordion__section">
            <div className="govuk-accordion__section-header">
              <h2 className="govuk-accordion__section-heading">
                <span
                  className="govuk-accordion__section-button"
                  id="accordion-expenditure-heading-9"
                >
                  Other
                </span>
              </h2>
            </div>
            <div
              id="accordion-expenditure-content-9"
              className="govuk-accordion__section-content"
            >
              <p className="govuk-body">
                <SpendingSectionOther data={data} />
              </p>
            </div>
          </div>
        </div>
      </ChartDimensionContext.Provider>
    </ChartModeContext.Provider>
  );
};
