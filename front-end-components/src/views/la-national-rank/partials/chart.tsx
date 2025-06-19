import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import { useChartModeContext } from "src/contexts";
import { HorizontalBarChartWrapper } from "src/composed/horizontal-bar-chart-wrapper";
import { ChartMode } from "src/components";
import { ErrorBanner } from "src/components/error-banner";
import { LaNationalRankChartProps } from "./types";
import { SelectedEstablishmentContext } from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import { NationalRankApi, LocalAuthorityRank } from "src/services";
import { LocalAuthorityRankData } from "./types";
import { Loading } from "src/components/loading";
import { DataWarning } from "src/components/charts/data-warning";

export const LaNationalRankChart: React.FC<LaNationalRankChartProps> = ({
  title,
  summary,
  prefix,
  valueLabel,
  rankingApiParam,
}) => {
  const { chartMode, setChartMode } = useChartModeContext();
  const selectedEstablishment = useContext(SelectedEstablishmentContext);
  const [data, setData] = useState<LocalAuthorityRank[] | null>();
  const [loadError, setLoadError] = useState<string>();

  const getData = useCallback(async () => {
    setData(null);
    setLoadError(undefined);
    const response = await NationalRankApi.get(rankingApiParam);

    if (!response.ranking) {
      setLoadError("Unable to load national ranking data");
    }
    return response;
  }, [rankingApiParam]);

  useEffect(() => {
    getData().then((result) => {
      setData(result.ranking || null);
    });
  }, [getData]);

  const chartData: HorizontalBarChartWrapperData<
    Omit<LocalAuthorityRankData, "rank">
  > = useMemo(() => {
    const tableHeadings = ["Local Authority", valueLabel];

    return {
      dataPoints:
        data?.map((rank) => {
          return {
            laCode: rank.code || "",
            laName: rank.name || "",
            value: rank.value || 0,
          };
        }) ?? [],
      tableHeadings,
    };
  }, [data, valueLabel]);

  const notInRanking = useMemo(() => {
    if (!data) {
      return false;
    }

    return data.find((d) => d.code == selectedEstablishment) == null;
  }, [data, selectedEstablishment]);

  return (
    <>
      {data?.length ? (
        <>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-full">
              <p className="govuk-body">{summary}</p>
            </div>
          </div>
          <div>
            <HorizontalBarChartWrapper
              chartTitle={title}
              data={chartData}
              localAuthority
              showCopyImageButton
              valueUnit="%"
              xAxisLabel={valueLabel}
            >
              <ChartMode
                chartMode={chartMode}
                handleChange={setChartMode}
                prefix={prefix}
              />
              {notInRanking && (
                <ErrorBanner
                  isRendered
                  message="There isn't enough information available to rank the current local authority."
                />
              )}
            </HorizontalBarChartWrapper>
          </div>
        </>
      ) : loadError ? (
        <DataWarning>{loadError}</DataWarning>
      ) : (
        <Loading />
      )}
    </>
  );
};
