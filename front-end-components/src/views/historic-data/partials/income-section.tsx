import React, { useCallback, useEffect, useState } from "react";
import {
  Actual,
  ChartDimensions,
  ChartMode,
  ChartModeChart,
  CostCategories,
} from "src/components";
import { Income, IncomeApi } from "src/services";
import {
  ChartDimensionContext,
  ChartModeProvider,
  useChartModeContext,
} from "src/contexts";
import { Loading } from "src/components/loading";
import { IncomeSectionGrantFunding } from "src/views/historic-data/partials/income-section-grant-funding";
import { IncomeSectionSelfGenerated } from "src/views/historic-data/partials/income-section-self-generated";
import { IncomeSectionDirectRevenue } from "src/views/historic-data/partials/income-section-direct-revenue";
import { HistoricChart } from "src/composed/historic-chart-composed";

export const IncomeSection: React.FC<{ type: string; id: string }> = ({
  type,
  id,
}) => {
  const defaultDimension = Actual;
  const { chartMode, setChartMode } = useChartModeContext();
  const [dimension, setDimension] = useState(defaultDimension);
  const [data, setData] = useState(new Array<Income>());
  const getData = useCallback(async () => {
    setData(new Array<Income>());
    return await IncomeApi.history(type, id, dimension.value);
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
              elementId="income"
              defaultValue={dimension.value}
            />
          </div>
          <div className="govuk-grid-column-one-third">
            <ChartMode
              chartMode={chartMode}
              handleChange={setChartMode}
              prefix="income"
            />
          </div>
        </div>
        <hr className="govuk-section-break govuk-section-break--l govuk-section-break--visible govuk-!-margin-top-0" />
        {data.length > 0 ? (
          <HistoricChart
            chartName="Total income"
            data={data}
            seriesConfig={{
              totalIncome: {
                label: "Total income",
                visible: true,
              },
            }}
            valueField="totalIncome"
          >
            <h2 className="govuk-heading-m">Total income</h2>
          </HistoricChart>
        ) : (
          <Loading />
        )}
        <div
          className="govuk-accordion"
          data-module="govuk-accordion"
          id="accordion-income"
        >
          <div className="govuk-accordion__section">
            <div className="govuk-accordion__section-header">
              <h2 className="govuk-accordion__section-heading">
                <span
                  className="govuk-accordion__section-button"
                  id="accordion-income-heading-1"
                >
                  Grant funding
                </span>
              </h2>
            </div>
            <div
              id="accordion-income-content-1"
              className="govuk-accordion__section-content"
            >
              <p className="govuk-body">
                <IncomeSectionGrantFunding data={data} />
              </p>
            </div>
          </div>
          <div className="govuk-accordion__section">
            <div className="govuk-accordion__section-header">
              <h2 className="govuk-accordion__section-heading">
                <span
                  className="govuk-accordion__section-button"
                  id="accordion-income-heading-2"
                >
                  Self-generated
                </span>
              </h2>
            </div>
            <div
              id="accordion-income-content-2"
              className="govuk-accordion__section-content"
            >
              <p className="govuk-body">
                <IncomeSectionSelfGenerated data={data} />
              </p>
            </div>
          </div>
          <div className="govuk-accordion__section">
            <div className="govuk-accordion__section-header">
              <h2 className="govuk-accordion__section-heading">
                <span
                  className="govuk-accordion__section-button"
                  id="accordion-income-heading-3"
                >
                  Direct revenue financing
                </span>
              </h2>
            </div>
            <div
              id="accordion-income-content-3"
              className="govuk-accordion__section-content"
            >
              <p className="govuk-body">
                <IncomeSectionDirectRevenue data={data} />
              </p>
            </div>
          </div>
        </div>
      </ChartDimensionContext.Provider>
    </ChartModeProvider>
  );
};
