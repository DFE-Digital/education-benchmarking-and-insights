import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  SelectedEstablishmentContext,
  useChartModeContext,
} from "src/contexts";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { NationalRankApi, LocalAuthorityRank } from "src/services";
import { ChartMode } from "src/components";
import { ErrorBanner } from "src/components/error-banner";
import { LocalAuthorityRankData, LaNationalRankChartProps } from "./types";

export const LaNationalRankChart: React.FC<LaNationalRankChartProps> = ({
  title,
}) => {
  const selectedEstablishment = useContext(SelectedEstablishmentContext);
  const { chartMode, setChartMode } = useChartModeContext();
  const [data, setData] = useState<LocalAuthorityRank[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await NationalRankApi.get("SpendAsPercentageOfBudget");
  }, []);

  useEffect(() => {
    getData().then((result) => {
      setData(result.ranking || null);
    });
  }, [getData]);

  const valueLabel = "Spend as percentage of budget";
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
  }, [data]);

  const notInRanking = useMemo(() => {
    if (!data) {
      return false;
    }

    return data.find((d) => d.code == selectedEstablishment) == null;
  }, [data, selectedEstablishment]);

  return (
    <>
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-two-thirds">&nbsp;</div>
        <div className="govuk-grid-column-one-third">
          <ChartMode chartMode={chartMode} handleChange={setChartMode} />
        </div>
      </div>
      <div>
        {data?.length && (
          <HorizontalBarChartWrapper
            chartTitle={title}
            data={chartData}
            localAuthority
            showCopyImageButton
            valueUnit="%"
            xAxisLabel={valueLabel}
          >
            <h2 className="govuk-heading-m">{title}</h2>
            {notInRanking && (
              <ErrorBanner
                isRendered
                message="There isn't enough information available to rank the current local authority."
              />
            )}
          </HorizontalBarChartWrapper>
        )}
      </div>
    </>
  );
};
