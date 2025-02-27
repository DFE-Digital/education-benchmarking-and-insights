import React, { useCallback, useEffect, useState } from "react";
import { ChartMode } from "src/components";
import {
  LocalAuthoritySection251,
  LocalAuthoritySection251History,
} from "src/services";
import { useChartModeContext } from "src/contexts";
import { Loading } from "src/components/loading";
import { HistoricDataHighNeedsProps } from "../types";
import { section251Sections } from ".";
import classNames from "classnames";
import { DataWarning } from "src/components/charts/data-warning";
import { HighNeedsApi } from "src/services/high-needs-api";
import { HistoricChartSection251 } from "src/composed/historic-chart-section-251-composed";

export const Section251Section: React.FC<HistoricDataHighNeedsProps> = ({
  code,
  load,
  fetchTimeout,
}) => {
  const { chartMode, setChartMode } = useChartModeContext();
  const [data, setData] = useState<
    LocalAuthoritySection251History<LocalAuthoritySection251>[] | undefined
  >();
  const [loadError, setLoadError] = useState<string>();

  const getData = useCallback(async () => {
    if (!load) {
      return undefined;
    }

    setLoadError(undefined);
    setData(undefined);
    return await HighNeedsApi.history(
      code,
      fetchTimeout ? [AbortSignal.timeout(fetchTimeout)] : undefined
    );
  }, [code, load, fetchTimeout]);

  useEffect(() => {
    getData()
      .then((result) => {
        setData(result);
      })
      .catch((e: Error) => {
        setData([]);

        if (e.name !== "AbortError") {
          setLoadError("Unable to load historical section 251 data");
        }
      });
  }, [getData]);

  return (
    <>
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-two-thirds">&nbsp;</div>
        <div className="govuk-grid-column-one-third">
          <ChartMode
            chartMode={chartMode}
            handleChange={setChartMode}
            prefix="section-251"
          />
        </div>
      </div>
      <hr className="govuk-section-break govuk-section-break--l govuk-section-break--visible govuk-!-margin-top-0" />
      {loadError ? (
        <DataWarning>{loadError}</DataWarning>
      ) : (
        !data && <Loading />
      )}
      <div
        className={classNames("govuk-accordion", {
          "govuk-visually-hidden": !data?.length,
        })}
        data-module="govuk-accordion"
        id="accordion-expenditure"
      >
        {section251Sections.map((section, index) => (
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
              {section.charts.map((chart) => (
                <section key={chart.field}>
                  <HistoricChartSection251
                    chartTitle={chart.name}
                    data={data}
                    valueField={chart.field}
                  >
                    <h3 className="govuk-heading-s">{chart.name}</h3>
                  </HistoricChartSection251>
                </section>
              ))}
            </div>
          </div>
        ))}
      </div>
    </>
  );
};
