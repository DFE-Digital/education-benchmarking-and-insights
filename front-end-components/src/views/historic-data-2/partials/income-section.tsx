import React, { useCallback, useEffect, useState } from "react";
import {
  Actual,
  ChartDimensions,
  ChartMode,
  CostCategories,
  PoundsPerPupil,
} from "src/components";
import {
  IncomeHistoryItem,
  HistoryService,
  SchoolHistoryComparison,
} from "src/services";
import { ChartDimensionContext, useChartModeContext } from "src/contexts";
import { HistoricChart2 } from "src/composed/historic-chart-2-composed";
import { Loading } from "src/components/loading";
import { HistoricData2Props } from "../types";
import { incomeSections } from ".";
import classNames from "classnames";
import { DataWarning } from "src/components/charts/data-warning";
import { useAbort } from "src/hooks/useAbort";

export const IncomeSection: React.FC<HistoricData2Props> = ({
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
  const [data, setData] = useState<SchoolHistoryComparison<IncomeHistoryItem>>(
    {}
  );
  const [loadError, setLoadError] = useState<string>();
  const { abort, signal } = useAbort();

  const getData = useCallback(async () => {
    if (!load) {
      return {};
    }

    setLoadError(undefined);
    setData({});
    return await HistoryService.getIncomeHistoryComparison(
      type,
      id,
      dimension.value,
      overallPhase,
      financeType,
      fetchTimeout ? [signal, AbortSignal.timeout(fetchTimeout)] : [signal]
    );
  }, [
    dimension.value,
    financeType,
    id,
    load,
    overallPhase,
    type,
    fetchTimeout,
    signal,
  ]);

  useEffect(() => {
    getData()
      .then((result) => {
        setData(result);
      })
      .catch((e: Error) => {
        setData({});

        if (e.name !== "AbortError") {
          setLoadError("Unable to load historical income data");
        }
      });
  }, [getData]);

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    abort();

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
            prefix="income"
          />
        </div>
      </div>
      <hr className="govuk-section-break govuk-section-break--l govuk-section-break--visible govuk-!-margin-top-0" />
      {data.school?.length ? (
        <section>
          <HistoricChart2
            chartTitle="Total income"
            data={data}
            valueField="totalIncome"
            perUnitDimension={PoundsPerPupil}
          >
            <h2 className="govuk-heading-m">Total income</h2>
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
        id="accordion-income"
      >
        {incomeSections.map((section, index) => (
          <div className="govuk-accordion__section" key={index}>
            <div className="govuk-accordion__section-header">
              <h2 className="govuk-accordion__section-heading">
                <span
                  className="govuk-accordion__section-button"
                  id={`accordion-income-heading-${index + 1}`}
                >
                  {section.heading}
                </span>
              </h2>
            </div>
            <div
              id={`accordion-income-content-${index + 1}`}
              className="govuk-accordion__section-content"
            >
              {section.charts
                .filter((c) => c.type === undefined || c.type === type)
                .map((chart) => (
                  <section key={chart.field}>
                    <HistoricChart2
                      chartTitle={chart.name}
                      data={data}
                      valueField={chart.field}
                      perUnitDimension={chart.perUnitDimension}
                    >
                      <h3 className="govuk-heading-s">{chart.name}</h3>
                    </HistoricChart2>
                    {chart.details && (
                      <details className="govuk-details">
                        <summary className="govuk-details__summary">
                          <span className="govuk-details__summary-text">
                            {chart.details.label}
                          </span>
                        </summary>
                        <div className="govuk-details__text">
                          {chart.details.content}
                        </div>
                      </details>
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
