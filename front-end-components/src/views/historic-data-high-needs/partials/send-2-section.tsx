import React, { useCallback, useEffect, useState } from "react";
import { ChartMode } from "src/components";
import { LocalAuthoritySend2History } from "src/services";
import { useChartModeContext } from "src/contexts";
import { Loading } from "src/components/loading";
import { HistoricDataHighNeedsProps } from "../types";
import classNames from "classnames";
import { DataWarning } from "src/components/charts/data-warning";
import { EducationHealthCarePlanApi } from "src/services/education-health-care-plans-api";
import { HistoricChart } from "src/composed/historic-chart-composed";
import { send2LeadSection, send2AccordionSection } from ".";

export const Send2Section: React.FC<HistoricDataHighNeedsProps> = ({
  code,
  load,
  fetchTimeout,
}) => {
  const { chartMode, setChartMode } = useChartModeContext();
  const [data, setData] = useState<LocalAuthoritySend2History[] | undefined>();
  const [loadError, setLoadError] = useState<string>();

  const getData = useCallback(async () => {
    if (!load) {
      return undefined;
    }

    setLoadError(undefined);
    setData(undefined);
    return await EducationHealthCarePlanApi.history(
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
          setLoadError("Unable to load historical send 2 data");
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
            prefix="send-2"
          />
        </div>
      </div>
      {loadError ? (
        <DataWarning>{loadError}</DataWarning>
      ) : (
        !data && <Loading />
      )}
      {data &&
        data.length > 0 &&
        send2LeadSection.charts.map((chart) => (
          <section key={chart.field}>
            <HistoricChart
              chartTitle={chart.name}
              data={data}
              seriesConfig={{
                [chart.field]: {
                  visible: true,
                },
              }}
              valueField={chart.field}
              columnHeading="Amount"
            >
              <h2 className="govuk-heading-m">{chart.name}</h2>
            </HistoricChart>
          </section>
        ))}
      <div
        className={classNames("govuk-accordion", {
          "govuk-visually-hidden": !data?.length,
        })}
        data-module="govuk-accordion"
        id="accordion-send-2"
      >
        <div className="govuk-accordion__section">
          <div className="govuk-accordion__section-header">
            <h2 className="govuk-accordion__section-heading">
              <span
                className="govuk-accordion__section-button"
                id="accordion-send-2-heading"
              >
                {send2AccordionSection.heading}
              </span>
            </h2>
          </div>
          <div
            id={"accordion-send-2-content"}
            className="govuk-accordion__section-content"
          >
            {data &&
              data.length > 0 &&
              send2AccordionSection.charts.map((chart) => (
                <section key={chart.field}>
                  <HistoricChart
                    chartTitle={chart.name}
                    data={data}
                    seriesConfig={{
                      [chart.field]: {
                        label: chart.name,
                        visible: true,
                      },
                    }}
                    valueField={chart.field}
                    columnHeading="Amount"
                  >
                    <h3 className="govuk-heading-m">{chart.name}</h3>
                  </HistoricChart>
                </section>
              ))}
          </div>
        </div>
      </div>
    </>
  );
};
