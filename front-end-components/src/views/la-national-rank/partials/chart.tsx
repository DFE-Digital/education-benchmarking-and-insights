import React, { useCallback, useEffect, useMemo, useState } from "react";
import { useChartModeContext } from "src/contexts";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { NationalRankApi, LocalAuthorityRank } from "src/services";
import { ChartMode } from "src/components";
import { ErrorBanner } from "src/components/error-banner";
import { LocalAuthorityRankData } from "./types";

export const LaNationalRankChart: React.FC = () => {
  const { chartMode, setChartMode } = useChartModeContext();
  const [data, setData] = useState<LocalAuthorityRank[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await NationalRankApi.get("asc");
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

  const title = "National ranking";

  return (
    <>
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-two-thirds">&nbsp;</div>
        <div className="govuk-grid-column-one-third">
          <ChartMode chartMode={chartMode} handleChange={setChartMode} />
        </div>
      </div>
      <div>
        {data?.length ? (
          <HorizontalBarChartWrapper
            chartTitle={title}
            data={chartData}
            localAuthority
            showCopyImageButton
            valueUnit="%"
            xAxisLabel={valueLabel}
          >
            <h2 className="govuk-heading-m">{title}</h2>
          </HorizontalBarChartWrapper>
        ) : (
          <ErrorBanner message="XXX" />
        )}
      </div>
    </>
  );
};
