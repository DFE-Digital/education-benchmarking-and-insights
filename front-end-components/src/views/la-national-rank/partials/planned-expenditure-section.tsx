import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import { SelectedEstablishmentContext } from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import { NationalRankApi, LocalAuthorityRank } from "src/services";
import { LocalAuthorityRankData } from "./types";
import { LaNationalRankChart } from ".";
import { Loading } from "src/components/loading";
import { DataWarning } from "src/components/charts/data-warning";

export const PlannedExpenditureSection: React.FC = () => {
  const selectedEstablishment = useContext(SelectedEstablishmentContext);
  const [data, setData] = useState<LocalAuthorityRank[] | null>();
  const [loadError, setLoadError] = useState<string>();

  const title = "planned-expenditure";
  const summary = "Outturn figures are pre-recoupment";
  const valueLabel = "Spend as percentage of planned expenditure";

  const getData = useCallback(async () => {
    setData(null);
    setLoadError(undefined);
    const response = await NationalRankApi.get("SpendAsPercentageOfBudget");

    if (!response.ranking) {
      setLoadError("Unable to load national ranking data");
    }
    return response;
  }, []);

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
        <LaNationalRankChart
          chartData={chartData}
          title={title}
          summary={summary}
          prefix={title}
          valueLabel={valueLabel}
          notInRanking={notInRanking}
        />
      ) : loadError ? (
        <DataWarning>{loadError}</DataWarning>
      ) : (
        <Loading />
      )}
    </>
  );
};
