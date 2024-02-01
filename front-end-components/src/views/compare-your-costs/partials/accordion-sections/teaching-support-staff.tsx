import React, { useState } from "react";
import { TeachingSupportStaffProps } from "src/views/compare-your-costs/partials/accordion-sections/types";
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

export const TeachingSupportStaff: React.FC<TeachingSupportStaffProps> = ({
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

  const totalTeachingBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.totalTeachingSupportStaffCosts,
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

  const teachingStaffBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.teachingStaffCosts,
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

  const supplyTeachingBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.supplyTeachingStaffCosts,
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

  const educationalConsultancyBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.educationalConsultancyCosts,
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

  const educationSupportStaffBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.educationSupportStaffCosts,
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

  const agencySupplyBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.agencySupplyTeachingStaffCosts,
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
              id="accordion-heading-1"
            >
              Teaching and teaching support staff
            </span>
          </h2>
        </div>
        <div
          id="accordion-content-1"
          className="govuk-accordion__section-content"
          aria-labelledby="accordion-heading-1"
          role="region"
        >
          <HorizontalBarChartWrapper
            data={totalTeachingBarData}
            chartId="total-teaching-support-staff-cost"
          >
            <h3 className="govuk-heading-s">
              Total teaching and teaching support staff costs
            </h3>
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="total-teaching-support-staff-cost"
              defaultValue={dimension}
            />
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={teachingStaffBarData}
            chartId="teaching-staff-costs"
          >
            <h3 className="govuk-heading-s">Teaching staff costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={supplyTeachingBarData}
            chartId="supply-teaching-staff-costs"
          >
            <h3 className="govuk-heading-s">Supply teaching staff costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={educationalConsultancyBarData}
            chartId="educational-consultancy-costs"
          >
            <h3 className="govuk-heading-s">Educational consultancy costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={educationSupportStaffBarData}
            chartId="education-support-stff-costs"
          >
            <h3 className="govuk-heading-s">Educational support staff costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={agencySupplyBarData}
            chartId="agency-supply-teaching-staff-costs"
          >
            <h3 className="govuk-heading-s">
              Agency supply teaching staff costs
            </h3>
          </HorizontalBarChartWrapper>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
