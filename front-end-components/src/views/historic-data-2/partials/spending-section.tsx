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
import { HistoricData2Props } from "../types";
import { CateringCostsHistoryChart } from "./catering-costs-history-chart";
import { spendingSections } from "./spending-sections";

export const SpendingSection: React.FC<HistoricData2Props> = ({
  type,
  id,
  overallPhase,
  financeType,
}) => {
  const defaultDimension = Actual;
  const { chartMode, setChartMode } = useChartModeContext();
  const [dimension, setDimension] = useState(defaultDimension);
  const [data, setData] = useState<
    SchoolHistoryComparison<SchoolExpenditureHistory>
  >({});
  const getData = useCallback(async () => {
    setData({});
    return await ExpenditureApi.historyComparison(
      type,
      id,
      dimension.value,
      overallPhase,
      financeType
    );
  }, [type, id, dimension, overallPhase, financeType]);

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
        {spendingSections.map((section, index) => (
          <div className="govuk-accordion__section" key={index}>
            <div className="govuk-accordion__section-header">
              <h2 className="govuk-accordion__section-heading">
                <span
                  className="govuk-accordion__section-button"
                  id={`accordion-expenditure-heading-${index + 1}`}
                >
                  {section.heading}
                </span>
              </h2>
            </div>
            <div
              id={`accordion-expenditure-content-${index + 1}`}
              className="govuk-accordion__section-content"
            >
              {section.charts
                .filter((c) => c.type === undefined || c.type === type)
                .map((chart) => {
                  return data.school?.length ? (
                    chart.field === "totalCateringCostsField" ? (
                      <CateringCostsHistoryChart
                        chartName={chart.name}
                        data={data}
                        key={chart.field}
                      />
                    ) : (
                      <HistoricChart2
                        chartName={chart.name}
                        data={data}
                        valueField={chart.field}
                        key={chart.field}
                      >
                        <h3 className="govuk-heading-s">{chart.name}</h3>
                      </HistoricChart2>
                    )
                  ) : (
                    <Loading key={chart.field} />
                  );
                })}
            </div>
          </div>
        ))}
      </div>
    </ChartDimensionContext.Provider>
  );
};
