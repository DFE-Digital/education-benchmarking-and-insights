import React, { useCallback, useEffect, useState } from "react";
import {
  Actual,
  ChartDimensions,
  ChartMode,
  CostCategories,
} from "src/components";
import {
  ExpenditureApi,
  SchoolExpenditureHistory,
  SchoolHistoryComparison,
} from "src/services";
import { ChartDimensionContext, useChartModeContext } from "src/contexts";
import { HistoricChart2 } from "src/composed/historic-chart-2-composed";
import { Loading } from "src/components/loading";
import { SpendingSectionPremisesServices } from "./spending-section-premises-services";

export const SpendingSection: React.FC<{ type: string; id: string }> = ({
  type,
  id,
}) => {
  const defaultDimension = Actual;
  const { chartMode, setChartMode } = useChartModeContext();
  const [dimension, setDimension] = useState(defaultDimension);
  const [data, setData] = useState<
    SchoolHistoryComparison<SchoolExpenditureHistory>
  >({});
  const getData = useCallback(async () => {
    setData({});
    return await ExpenditureApi.historyComparison(type, id, dimension.value);
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
    <ChartDimensionContext.Provider value={dimension}>
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-two-thirds">
          <ChartDimensions
            dimensions={CostCategories}
            handleChange={handleSelectChange}
            elementId="expenditure"
            value={dimension.value}
          />
        </div>
        <div className="govuk-grid-column-one-third">
          <ChartMode
            chartMode={chartMode}
            handleChange={setChartMode}
            prefix="expenditure"
          />
        </div>
      </div>
      <hr className="govuk-section-break govuk-section-break--l govuk-section-break--visible govuk-!-margin-top-0" />
      {data.school?.length ? (
        <HistoricChart2
          chartName="Total expenditure"
          data={data}
          valueField="totalExpenditure"
        >
          <h2 className="govuk-heading-m">Total expenditure</h2>
        </HistoricChart2>
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
                Premises staff and services
              </span>
            </h2>
          </div>
          <div
            id="accordion-expenditure-content-1"
            className="govuk-accordion__section-content"
          >
            <SpendingSectionPremisesServices data={data} />
          </div>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
