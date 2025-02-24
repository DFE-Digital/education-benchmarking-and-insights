import React, { useCallback, useEffect, useMemo, useState } from "react";
import { BalanceData } from "src/views/compare-your-trust/partials";
import { Dimension } from "src/components";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { BalanceApi, TrustBalance } from "src/services";
import { ChartDimensionContext } from "src/contexts";

export const Balance: React.FC<{
  id: string;
  dimension: Dimension;
}> = ({ id, dimension }) => {
  const [data, setData] = useState<TrustBalance[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await BalanceApi.trust(id, dimension.value, false);
  }, [id, dimension]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const inYearBalanceChartData: HorizontalBarChartWrapperData<BalanceData> =
    useMemo(() => {
      const tableHeadings = [
        "Trust name",
        `Total ${dimension.heading}`,
        `School ${dimension.heading}`,
        `Central ${dimension.heading}`,
      ];

      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.inYearBalance ?? 0,
                  schoolValue: trust.schoolInYearBalance ?? 0,
                  centralValue: trust.centralInYearBalance ?? 0,
                  type: "balance",
                };
              })
            : [],
        tableHeadings,
      };
    }, [dimension, data]);

  const revenueReserveChartData: HorizontalBarChartWrapperData<BalanceData> =
    useMemo(() => {
      const tableHeadings = ["Trust name", `Total ${dimension.heading}`];

      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.revenueReserve ?? 0,
                  schoolValue: undefined,
                  centralValue: undefined,
                  type: "balance",
                };
              })
            : [],
        tableHeadings,
      };
    }, [dimension, data]);

  return (
    <ChartDimensionContext.Provider value={dimension}>
      <HorizontalBarChartWrapper
        chartTitle="In-year balance"
        data={inYearBalanceChartData}
        linkToEstablishment
        showCopyImageButton
        tooltip
        trust
      >
        <h2 className="govuk-heading-m">In-year balance</h2>
      </HorizontalBarChartWrapper>
      <HorizontalBarChartWrapper
        chartTitle="Revenue reserve"
        data={revenueReserveChartData}
        linkToEstablishment
        showCopyImageButton
        tooltip
        trust
      >
        <h2 className="govuk-heading-m">Revenue reserve</h2>
      </HorizontalBarChartWrapper>
    </ChartDimensionContext.Provider>
  );
};
