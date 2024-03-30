import React, { useMemo, useState } from "react";
import {
  CalculateWorkforceValue,
  ChartDimensions,
  DimensionHeading,
  HeadcountPerFTE,
  PercentageOfWorkforce,
  PupilsPerStaffRole,
  WorkforceCategories,
} from "src/components";
import { ChartDimensionContext } from "src/contexts";
import {
  HeadcountData,
  HeadcountProps,
} from "src/views/compare-your-workforce/partials";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";

export const Headcount: React.FC<HeadcountProps> = (props) => {
  const { schools } = props;
  const [dimension, setDimension] = useState(PupilsPerStaffRole.value);

  const chartData: HorizontalBarChartWrapperData<HeadcountData> =
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
            value: CalculateWorkforceValue({
              dimension,
              value: school.schoolWorkforceHeadcount,
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
        chartName="school workforce (headcount)"
      >
        <h2 className="govuk-heading-m">School workforce (Headcount)</h2>
        <ChartDimensions
          dimensions={WorkforceCategories.filter(
            (category) =>
              category !== HeadcountPerFTE && category !== PercentageOfWorkforce
          )}
          handleChange={handleSelectChange}
          elementId="headcount"
          defaultValue={dimension}
        />
      </HorizontalBarChartWrapper>
    </ChartDimensionContext.Provider>
  );
};
