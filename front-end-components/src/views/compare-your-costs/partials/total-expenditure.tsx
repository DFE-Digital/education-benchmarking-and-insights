import React, { useMemo, useState } from "react";
import {
  TotalExpenditureData,
  TotalExpenditureProps,
} from "src/views/compare-your-costs/partials";
import { ChartDimensionContext } from "src/contexts";
import {
  CalculateCostValue,
  CostCategories,
  DimensionHeading,
  PoundsPerPupil,
  ChartDimensions,
  PercentageExpenditure,
} from "src/components";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";

export const TotalExpenditure: React.FC<TotalExpenditureProps> = ({
  schools,
}) => {
  const [dimension, setDimension] = useState(PoundsPerPupil.value);

  const chartData: HorizontalBarChartWrapperData<TotalExpenditureData> =
    useMemo(() => {
      const tableHeadings = [
        "School name",
        "Local Authority",
        "School type",
        "Number of pupils",
        DimensionHeading(dimension),
      ];

      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension,
              value: school.totalExpenditure,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools]);

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    setDimension(event.target.value);
  };

  return (
    <ChartDimensionContext.Provider value={dimension}>
      <HorizontalBarChartWrapper
        data={chartData}
        chartName="total expenditure"
        valueUnit="currency"
      >
        <h2 className="govuk-heading-m">Total Expenditure</h2>
        <ChartDimensions
          dimensions={CostCategories.filter(function (category) {
            return category !== PercentageExpenditure;
          })}
          handleChange={handleSelectChange}
          elementId="total-expenditure"
          defaultValue={dimension}
        />
      </HorizontalBarChartWrapper>
    </ChartDimensionContext.Provider>
  );
};
