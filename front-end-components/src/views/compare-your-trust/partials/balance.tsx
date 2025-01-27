import React, { useCallback, useEffect, useMemo, useState } from "react";
import { BalanceData } from "src/views/compare-your-trust/partials";
import { CostCategories, PoundsPerPupil } from "src/components";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import { BalanceApi, TrustBalance } from "src/services";
import { DimensionedChart } from "src/composed/dimensioned-chart";

export const Balance: React.FC<{
  id: string;
}> = ({ id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
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

  const handleDimensionChange = (value: string) => {
    const dimension =
      CostCategories.find((x) => x.value === value) ?? PoundsPerPupil;
    setDimension(dimension);
  };

  return (
    <DimensionedChart
      charts={[
        { data: inYearBalanceChartData, title: "In-year balance" },
        {
          data: revenueReserveChartData,
          title: "Revenue reserve",
          selector: true,
        },
      ]}
      dimension={dimension}
      dimensions={CostCategories}
      handleDimensionChange={handleDimensionChange}
      showCopyImageButton
      topLevel
      trust
    />
  );
};
