import React, { useCallback, useEffect, useState } from "react";
import {
  CensusCategories,
  ChartDimensions,
  ChartMode,
  PupilsPerStaffRole,
} from "src/components";
import {
  CensusHistoryItem,
  HistoryService,
  SchoolHistoryComparison,
} from "src/services";
import { ChartDimensionContext, useChartModeContext } from "src/contexts";
import { HistoricChart2 } from "src/composed/historic-chart-2-composed";
import { Loading } from "src/components/loading";
import { HistoricData2Props } from "../types";
import { censusCharts } from ".";
import { DataWarning } from "src/components/charts/data-warning";
import { useAbort } from "src/hooks/useAbort";

export const CensusSection: React.FC<HistoricData2Props> = ({
  financeType,
  id,
  load,
  overallPhase,
  type,
  fetchTimeout,
}) => {
  const defaultDimension = PupilsPerStaffRole;
  const { chartMode, setChartMode } = useChartModeContext();
  const [dimension, setDimension] = useState(defaultDimension);
  const [data, setData] = useState<SchoolHistoryComparison<CensusHistoryItem>>(
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
    return await HistoryService.getCensusHistoryComparison(
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
          setLoadError("Unable to load historical pupil and workforce data");
        }
      });
  }, [getData]);

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    abort();

    const dimension =
      CensusCategories.find((x) => x.value === event.target.value) ??
      defaultDimension;
    setDimension(dimension);
  };

  return (
    <ChartDimensionContext.Provider value={dimension}>
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-two-thirds">
          <ChartDimensions
            dimensions={CensusCategories}
            handleChange={handleSelectChange}
            elementId="census"
            value={dimension.value}
          />
        </div>
        <div className="govuk-grid-column-one-third">
          <ChartMode
            chartMode={chartMode}
            handleChange={setChartMode}
            prefix="census"
          />
        </div>
      </div>
      <hr className="govuk-section-break govuk-section-break--l govuk-section-break--visible govuk-!-margin-top-0" />
      {data.school?.length ? (
        censusCharts
          .filter((c) => c.type === undefined || c.type === type)
          .map((chart) => {
            return (
              <section key={chart.field}>
                <HistoricChart2
                  chartTitle={chart.name}
                  data={data}
                  valueField={chart.field}
                  key={chart.field}
                  perUnitDimension={chart.perUnitDimension}
                  valueUnit={chart.valueUnit}
                  axisLabel={chart.axisLabel}
                  columnHeading={chart.columnHeading}
                >
                  <h2 className="govuk-heading-s">{chart.name}</h2>
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
            );
          })
      ) : loadError ? (
        <DataWarning>{loadError}</DataWarning>
      ) : (
        <Loading />
      )}
    </ChartDimensionContext.Provider>
  );
};
