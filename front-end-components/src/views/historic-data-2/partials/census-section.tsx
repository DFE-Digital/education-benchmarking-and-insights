import React, { useCallback, useEffect, useState } from "react";
import {
  CensusCategories,
  ChartDimensions,
  ChartMode,
  PupilsPerStaffRole,
} from "src/components";
import {
  CensusApi,
  CensusHistory,
  SchoolHistoryComparison,
} from "src/services";
import { ChartDimensionContext, useChartModeContext } from "src/contexts";
import {
  HistoricChart2,
  HistoricChart2Props,
} from "src/composed/historic-chart-2-composed";
import { Loading } from "src/components/loading";
import { HistoricData2Props } from "../types";
import { censusCharts } from ".";

export const CensusSection: React.FC<HistoricData2Props> = ({
  financeType,
  id,
  load,
  overallPhase,
  type,
}) => {
  const defaultDimension = PupilsPerStaffRole;
  const { chartMode, setChartMode } = useChartModeContext();
  const [dimension, setDimension] = useState(defaultDimension);
  const [data, setData] = useState<SchoolHistoryComparison<CensusHistory>>({});
  const getData = useCallback(async () => {
    if (!load) {
      return {};
    }

    setData({});
    return await CensusApi.historyComparison(
      id,
      dimension.value,
      overallPhase,
      financeType
    );
  }, [dimension.value, financeType, id, load, overallPhase]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
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
            const chartProps: Partial<HistoricChart2Props<CensusHistory>> = {};
            if (chart.field === "totalPupils") {
              chartProps.valueUnit = "amount";
              chartProps.axisLabel = "total";
              chartProps.columnHeading = "Total";
            }

            return (
              <section key={chart.field}>
                <HistoricChart2
                  chartName={chart.name}
                  data={data}
                  valueField={chart.field}
                  key={chart.field}
                  perUnitDimension={chart.perUnitDimension}
                  {...chartProps}
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
      ) : (
        <Loading />
      )}
    </ChartDimensionContext.Provider>
  );
};
