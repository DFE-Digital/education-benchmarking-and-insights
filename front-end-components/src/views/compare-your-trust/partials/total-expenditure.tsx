import React, { useCallback, useEffect, useMemo, useState } from "react";
import { TotalExpenditureData } from "src/views/compare-your-trust/partials";
import { ChartDimensionContext } from "src/contexts";
import {
  CostCategories,
  PoundsPerPupil,
  ChartDimensions,
  PercentageExpenditure,
} from "src/components";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { TrustExpenditure, ExpenditureApi } from "src/services";

export const TotalExpenditure: React.FC<{
  id: string;
}> = ({ id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const [data, setData] = useState<TrustExpenditure[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.trust(
      id,
      dimension.value,
      "TotalExpenditure",
      true
    );
  }, [id, dimension]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const chartData: HorizontalBarChartWrapperData<TotalExpenditureData> =
    useMemo(() => {
      const tableHeadings = [
        "Trust name",
        `Total ${dimension.heading}`,
        `School ${dimension.heading}`,
        `Central ${dimension.heading}`,
      ];

      return {
        dataPoints:
          data?.map((trust) => {
            return {
              ...trust,
              totalValue: trust.totalExpenditure ?? 0,
              schoolValue: trust.schoolTotalExpenditure ?? 0,
              centralValue: trust.centralTotalExpenditure ?? 0,
            };
          }) ?? [],
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
      <HorizontalBarChartWrapper data={chartData} chartName="total expenditure">
        <h2 className="govuk-heading-m">Total Expenditure</h2>
        <ChartDimensions
          dimensions={CostCategories.filter(function (category) {
            return category !== PercentageExpenditure;
          })}
          handleChange={handleSelectChange}
          elementId="total-expenditure"
          defaultValue={dimension.value}
        />
      </HorizontalBarChartWrapper>
    </ChartDimensionContext.Provider>
  );
};
