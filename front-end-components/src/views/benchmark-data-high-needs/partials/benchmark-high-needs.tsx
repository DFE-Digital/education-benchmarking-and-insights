import {
  SelectedEstablishmentContext,
  useChartModeContext,
} from "src/contexts";
import { Section251Section } from ".";
import { BenchmarkDataHighNeedsAccordionProps } from "../types";
import { Send2Section } from "./send-2-section";
import { ChartMode } from "src/components";
import classNames from "classnames";
import { useCallback, useContext, useEffect, useState } from "react";
import { DataWarning } from "src/components/charts/data-warning";
import { Loading } from "src/components/loading";
import {
  LocalAuthoritySection251,
  LocalAuthoritySection251Benchmark,
  LocalAuthoritySend2Benchmark,
} from "src/services";
import { EducationHealthCarePlanApi } from "src/services/education-health-care-plans-api";
import { HighNeedsApi } from "src/services/high-needs-api";

export const BenchmarkHighNeeds: React.FC<
  BenchmarkDataHighNeedsAccordionProps
> = ({ count, editLink, fetchTimeout }) => {
  const selectedEstabishment = useContext(SelectedEstablishmentContext);
  const { chartMode, setChartMode } = useChartModeContext();
  const [section251LoadError, setSection251LoadError] = useState<string>();
  const [send2LoadError, setSend2LoadError] = useState<string>();
  const [send2Data, setSend2Data] = useState<
    LocalAuthoritySend2Benchmark[] | undefined
  >();
  const [section251Data, setSection251Data] = useState<
    LocalAuthoritySection251Benchmark<LocalAuthoritySection251>[] | undefined
  >();

  const getSection251Data = useCallback(async () => {
    setSection251LoadError(undefined);
    setSection251Data(undefined);
    return await HighNeedsApi.comparison(
      selectedEstabishment,
      fetchTimeout ? [AbortSignal.timeout(fetchTimeout)] : undefined
    );
  }, [fetchTimeout, selectedEstabishment]);

  const getSend2Data = useCallback(async () => {
    setSend2LoadError(undefined);
    setSend2Data(undefined);
    return await EducationHealthCarePlanApi.comparison(
      selectedEstabishment,
      fetchTimeout ? [AbortSignal.timeout(fetchTimeout)] : undefined
    );
  }, [fetchTimeout, selectedEstabishment]);

  useEffect(() => {
    getSend2Data()
      .then((result) => {
        setSend2Data(result);
      })
      .catch((e: Error) => {
        setSend2Data([]);

        if (e.name !== "AbortError") {
          setSend2LoadError("Unable to load benchmark send 2 data");
        }
      });
  }, [getSend2Data]);

  useEffect(() => {
    getSection251Data()
      .then((result) => {
        setSection251Data(result);
      })
      .catch((e: Error) => {
        setSection251Data([]);

        if (e.name !== "AbortError") {
          setSection251LoadError("Unable to load benchmark section 251 data");
        }
      });
  }, [getSection251Data]);

  return (
    <>
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-two-thirds">
          {count && (
            <p className="govuk-body govuk-!-font-weight-bold">
              Currently comparing against {count} local
              {count === 1 ? " authority" : " authorities"}
            </p>
          )}
          {editLink && (
            <a
              href={editLink}
              role="button"
              className="govuk-button govuk-button--secondary govuk-!-margin-bottom-9"
            >
              Change comparators
            </a>
          )}
        </div>
        <div className="govuk-grid-column-one-third">
          <ChartMode chartMode={chartMode} handleChange={setChartMode} />
        </div>
      </div>
      {section251LoadError || send2LoadError ? (
        <DataWarning>
          {section251LoadError}
          {send2LoadError}
        </DataWarning>
      ) : (
        !send2Data && !section251Data && <Loading />
      )}
      <div
        className={classNames({
          "govuk-visually-hidden":
            !section251Data?.length || !send2Data?.length,
        })}
      >
        <Section251Section data={section251Data} />
        <Send2Section data={send2Data} />
      </div>
    </>
  );
};
