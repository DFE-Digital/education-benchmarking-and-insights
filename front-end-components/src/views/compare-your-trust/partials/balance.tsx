import React, { useCallback, useEffect, useMemo, useState } from "react";
import { BalanceData } from "src/views/compare-your-trust/partials";
import { ChartDimensionContext } from "src/contexts";
import {
  CostCategories,
  PoundsPerPupil,
  ChartDimensions,
} from "src/components";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { BalanceApi, TrustBalance } from "src/services";

export const Balance: React.FC<{
  id: string;
}> = ({ id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const [data, setData] = useState<TrustBalance[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await BalanceApi.trust(id, dimension.value, true);
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
                  totalValue: trust.revenueReserve ?? 0,
                  schoolValue: trust.schoolRevenueReserve ?? 0,
                  centralValue: trust.centralRevenueReserve ?? 0,
                  type: "balance",
                };
              })
            : [],
        tableHeadings,
      };
    }, [dimension, data]);

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    const dimension =
      CostCategories.find((x) => x.value === event.target.value) ??
      PoundsPerPupil;
    setDimension(dimension);
  };

  return (
    <ChartDimensionContext.Provider value={dimension}>
      <HorizontalBarChartWrapper
        data={inYearBalanceChartData}
        chartName="in year balance"
        trust
      >
        <h2 className="govuk-heading-m">In-year balance</h2>
        <ChartDimensions
          dimensions={CostCategories}
          handleChange={handleSelectChange}
          elementId="in-year-balance"
          value={dimension.value}
        />
      </HorizontalBarChartWrapper>
      <HorizontalBarChartWrapper
        data={revenueReserveChartData}
        chartName="revenue reserve"
        trust
      >
        <h2 className="govuk-heading-m">Revenue reserve</h2>
        <ChartDimensions
          dimensions={CostCategories}
          handleChange={handleSelectChange}
          elementId="in-year-balance"
          value={dimension.value}
        />
      </HorizontalBarChartWrapper>
    </ChartDimensionContext.Provider>
  );
};
