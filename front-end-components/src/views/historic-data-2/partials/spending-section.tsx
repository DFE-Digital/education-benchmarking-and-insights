import React, { useCallback, useEffect, useState } from "react";
import {
  Actual,
  ChartDimensions,
  ChartMode,
  CostCategories,
  PoundsPerPupil,
} from "src/components";
import {
  ExpenditureHistoryItem,
  HistoryService,
  SchoolHistoryComparison,
} from "src/services";
import { ChartDimensionContext, useChartModeContext } from "src/contexts";
import { HistoricChart2 } from "src/composed/historic-chart-2-composed";
import { Loading } from "src/components/loading";
import { HistoricData2Props } from "../types";
import { CateringCostsHistoryChart } from "./catering-costs-history-chart";
import { spendingSections } from ".";
import classNames from "classnames";
import { DataWarning } from "src/components/charts/data-warning";

export const SpendingSection: React.FC<HistoricData2Props> = ({
  financeType,
  id,
  load,
  overallPhase,
  type,
  fetchTimeout,
}) => {
  const defaultDimension = Actual;
  const { chartMode, setChartMode } = useChartModeContext();
  const [dimension, setDimension] = useState(defaultDimension);
  const [data, setData] = useState<
    SchoolHistoryComparison<ExpenditureHistoryItem>
  >({});
  const [loadError, setLoadError] = useState<string>();
  const [cancelSignal, setCancelSignal] = useState<AbortController>(
    new AbortController()
  );

  const getData = useCallback(async () => {
    if (!load) {
      return {};
    }

    setLoadError(undefined);
    setData({});
    return await HistoryService.getExpenditureHistoryComparison(
      type,
      id,
      dimension.value,
      overallPhase,
      financeType,
      fetchTimeout
        ? [cancelSignal.signal, AbortSignal.timeout(fetchTimeout)]
        : [cancelSignal.signal]
    );
  }, [
    dimension.value,
    financeType,
    id,
    load,
    overallPhase,
    type,
    fetchTimeout,
    cancelSignal,
  ]);

  useEffect(() => {
    getData()
      .then((result) => {
        setData(result);
      })
      .catch((e: Error) => {
        setData({});

        if (e.name !== "AbortError") {
          setLoadError("Unable to load historical spending data");
        }
      });
  }, [getData]);

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    cancelSignal?.abort();

    const dimension =
      CostCategories.find((x) => x.value === event.target.value) ??
      defaultDimension;
    setDimension(dimension);

    setCancelSignal(new AbortController());
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
        <section>
          <HistoricChart2
            chartName="Total expenditure"
            data={data}
            valueField="totalExpenditure"
            perUnitDimension={PoundsPerPupil}
          >
            <h2 className="govuk-heading-m">Total expenditure</h2>
          </HistoricChart2>
        </section>
      ) : loadError ? (
        <DataWarning>{loadError}</DataWarning>
      ) : (
        <Loading />
      )}
      <div
        className={classNames("govuk-accordion", {
          "govuk-visually-hidden": !data.school?.length,
        })}
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
                .map((chart) => (
                  <section key={chart.field}>
                    {(chart.field as string) === "totalCateringCostsField" ? (
                      <CateringCostsHistoryChart
                        chartName={chart.name}
                        data={data}
                        perUnitDimension={chart.perUnitDimension}
                      />
                    ) : (
                      <HistoricChart2
                        chartName={chart.name}
                        data={data}
                        valueField={chart.field}
                        perUnitDimension={chart.perUnitDimension}
                      >
                        <h3 className="govuk-heading-s">{chart.name}</h3>
                      </HistoricChart2>
                    )}
                  </section>
                ))}
            </div>
          </div>
        ))}
      </div>
    </ChartDimensionContext.Provider>
  );
};
