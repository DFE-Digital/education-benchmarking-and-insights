import React, { useState } from "react";
import { NonEducationalSupportStaffProps } from "src/views/compare-your-costs/partials/accordion-sections/types";
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

export const NonEducationalSupportStaff: React.FC<
  NonEducationalSupportStaffProps
> = ({ schools }) => {
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

  const administrativeClericalBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.administrativeClericalStaffCosts,
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

  const totalNonEducationalBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.totalNonEducationalSupportStaffCosts,
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

  const auditorsCostsBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.auditorsCosts,
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

  const otherStaffCostsBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.otherStaffCosts,
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

  const professionalServicesBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.professionalServicesNonCurriculumCosts,
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
              id="accordion-heading-2"
            >
              Non-educational support staff
            </span>
          </h2>
        </div>
        <div
          id="accordion-content-2"
          className="govuk-accordion__section-content"
          aria-labelledby="accordion-2"
          role="region"
        >
          <HorizontalBarChartWrapper
            data={totalNonEducationalBarData}
            chartName="total-non-educational-support-staff-costs"
          >
            <h3 className="govuk-heading-s">
              Total non-educational support staff costs
            </h3>
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="total-non-educational-support-staff-costs"
              defaultValue={dimension}
            />
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={administrativeClericalBarData}
            chartName="administrative-clerical-staff-costs"
          >
            <h3 className="govuk-heading-s">
              Administrative and clerical staff costs
            </h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={auditorsCostsBarData}
            chartName="Auditors costs"
          >
            <h3 className="govuk-heading-s">Auditors costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={otherStaffCostsBarData}
            chartName="Other staff costs"
          >
            <h3 className="govuk-heading-s">Other staff costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={professionalServicesBarData}
            chartName="profession-services-non-curriculum-costs"
          >
            <h3 className="govuk-heading-s">
              Professional services (non-curriculum) costs
            </h3>
          </HorizontalBarChartWrapper>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
