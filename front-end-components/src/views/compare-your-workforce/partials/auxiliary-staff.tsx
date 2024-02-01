import React, { useState } from "react";
import {
  ChartDimensions,
  DimensionHeading,
  HorizontalBarChartWrapperData,
  CalculateWorkforceValue,
  PupilsPerStaffRole,
  HorizontalBarChartWrapper,
  WorkforceCategories,
} from "src/components";
import { ChartDimensionContext } from "src/contexts";
import { AuxiliaryStaffProps } from "src/views/compare-your-workforce/partials";

export const AuxiliaryStaff: React.FC<AuxiliaryStaffProps> = (props) => {
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
          value: school.auxiliaryStaffFTE,
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
      <HorizontalBarChartWrapper
        data={chartData}
        chartName="auxiliary staff (full time equivalent)"
      >
        <h2 className="govuk-heading-m">
          Auxiliary staff (Full Time Equivalent)
        </h2>
        <ChartDimensions
          dimensions={WorkforceCategories}
          handleChange={handleSelectChange}
          elementId="auxiliary-staff"
          defaultValue={dimension}
        />
      </HorizontalBarChartWrapper>
    </ChartDimensionContext.Provider>
  );
};
