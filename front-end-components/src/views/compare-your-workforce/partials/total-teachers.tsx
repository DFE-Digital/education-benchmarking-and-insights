import React, { useState } from "react";
import {
  CalculateWorkforceValue,
  ChartDimensions,
  DimensionHeading,
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
  PupilsPerStaffRole,
  WorkforceCategories,
} from "src/components";
import { ChartDimensionContext } from "src/contexts";
import { TotalTeachersProps } from "src/views/compare-your-workforce/partials";

export const TotalTeachers: React.FC<TotalTeachersProps> = (props) => {
  const { schools } = props;
  const [dimension, setDimension] = useState(PupilsPerStaffRole);
  const tableHeadings = [
    "School name",
    "Local Authority",
    "School type",
    "Number of pupils",
    DimensionHeading(dimension),
  ];

  const chartData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateWorkforceValue({
          dimension: dimension,
          value: school.totalNumberOfTeachersFTE,
          ...school,
        }),
        additionalData: [
          school.localAuthority,
          school.schoolType,
          school.numberOfPupils,
        ],
      };
    }),
    tableHeadings: tableHeadings,
  };

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    setDimension(event.target.value);
  };

  return (
    <ChartDimensionContext.Provider value={dimension}>
      <HorizontalBarChartWrapper data={chartData} chartName="total-teachers">
        <h2 className="govuk-heading-m">
          Total number of teachers (Full Time Equivalent)
        </h2>
        <ChartDimensions
          dimensions={WorkforceCategories}
          handleChange={handleSelectChange}
          elementId="total-teachers"
          defaultValue={dimension}
        />
      </HorizontalBarChartWrapper>
    </ChartDimensionContext.Provider>
  );
};
