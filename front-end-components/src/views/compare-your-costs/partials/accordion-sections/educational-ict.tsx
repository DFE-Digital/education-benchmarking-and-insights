import React, { useState } from "react";
import { EducationalIctProps } from "src/views/compare-your-costs/partials/accordion-sections/types";
import {
  CalculateCostValue,
  CostCategories,
  DimensionHeading,
  PoundsPerPupil,
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
  ChartDimensions,
} from "src/components";
import { ChartDimensionContext } from "src/contexts";

export const EducationalIct: React.FC<EducationalIctProps> = ({ schools }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const tableHeadings = [
    "School name",
    "Local Authority",
    "School type",
    "Number of pupils",
    DimensionHeading(dimension),
  ];

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    setDimension(event.target.value);
  };

  const learningResourcesBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.learningResourcesIctCosts,
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

  return (
    <ChartDimensionContext.Provider value={dimension}>
      <div className="govuk-accordion__section">
        <div className="govuk-accordion__section-header">
          <h2 className="govuk-accordion__section-heading">
            <span
              className="govuk-accordion__section-button"
              id="accordion-heading-4"
            >
              Educational ICT
            </span>
          </h2>
        </div>
        <div
          id="accordion-content-4"
          className="govuk-accordion__section-content"
          aria-labelledby="accordion-heading-4"
          role="region"
        >
          <HorizontalBarChartWrapper
            data={learningResourcesBarData}
            chartName="eductional-learning-resources-costs"
          >
            <h3 className="govuk-heading-s">
              Educational learning resources costs
            </h3>
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="eductional-learning-resources-costs"
              defaultValue={dimension}
            />
          </HorizontalBarChartWrapper>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
