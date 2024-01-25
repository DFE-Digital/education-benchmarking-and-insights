import React, { useState } from "react";
import { EducationalSuppliesProps } from "src/views/compare-your-costs/partials/accordion-sections/types";
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

export const EducationalSupplies: React.FC<EducationalSuppliesProps> = ({
  schools,
}) => {
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

  const totalEducationalSuppliesBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.totalEducationalSuppliesCosts,
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

  const examinationFeesBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.examinationFeesCosts,
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

  const breakdownEducationalBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.breakdownEducationalSuppliesCosts,
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

  const learningResourcesBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.learningResourcesNonIctCosts,
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
              id="accordion-heading-educational-supplies"
            >
              Educational supplies
            </span>
          </h2>
        </div>
        <div
          id="accordion-content-educational-supplies"
          className="govuk-accordion__section-content"
          aria-labelledby="accordion-heading-educational-supplies"
          role="region"
        >
          <HorizontalBarChartWrapper
            data={totalEducationalSuppliesBarData}
            chartId="total-educational-supplies-costs"
          >
            <h3 className="govuk-heading-s">
              Total educational supplies costs
            </h3>
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="total-educational-supplies-costs"
              defaultValue={dimension}
            />
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={examinationFeesBarData}
            chartId="examination-fees-costs"
          >
            <h3 className="govuk-heading-s">Examination fees costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={breakdownEducationalBarData}
            chartId="breakdown-eductional-supplies-costs"
          >
            <h3 className="govuk-heading-s">
              Breakdown of educational supplies costs
            </h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={learningResourcesBarData}
            chartId="learning-resource-not-ict-costs"
          >
            <h3 className="govuk-heading-s">
              Learning resources (not ICT equipment) costs
            </h3>
          </HorizontalBarChartWrapper>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
