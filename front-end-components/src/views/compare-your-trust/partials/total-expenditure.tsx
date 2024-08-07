import React, { useCallback, useEffect, useMemo, useState } from "react";
import { TotalExpenditureData } from "src/views/compare-your-trust/partials";
import {
  ChartDimensionContext,
  useCentralServicesBreakdownContext,
} from "src/contexts";
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
import { ExpenditureApi, TotalExpenditureTrustExpenditure } from "src/services";
import {
  BreakdownExclude,
  BreakdownInclude,
} from "src/components/central-services-breakdown";

export const TotalExpenditure: React.FC<{
  id: string;
}> = ({ id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const { breakdown } = useCentralServicesBreakdownContext(true);
  const [data, setData] = useState<TotalExpenditureTrustExpenditure[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.trust<TotalExpenditureTrustExpenditure>(
      id,
      dimension.value,
      "TotalExpenditure",
      breakdown === BreakdownExclude
    );
  }, [id, dimension, breakdown]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const chartData: HorizontalBarChartWrapperData<TotalExpenditureData> =
    useMemo(() => {
      const tableHeadings = ["Trust name", `Total ${dimension.heading}`];
      if (breakdown === BreakdownInclude) {
        tableHeadings.push(
          `School ${dimension.heading}`,
          `Central ${dimension.heading}`
        );
      }

      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.totalExpenditure ?? 0,
                  schoolValue: trust.schoolTotalExpenditure ?? 0,
                  centralValue: trust.centralTotalExpenditure ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [dimension, data, breakdown]);

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
        data={chartData}
        chartName="total expenditure"
        trust
      >
        <h2 className="govuk-heading-m">Total expenditure</h2>
        <ChartDimensions
          dimensions={CostCategories.filter(function (category) {
            return category !== PercentageExpenditure;
          })}
          handleChange={handleSelectChange}
          elementId="total-expenditure"
          value={dimension.value}
        />
      </HorizontalBarChartWrapper>
    </ChartDimensionContext.Provider>
  );
};
