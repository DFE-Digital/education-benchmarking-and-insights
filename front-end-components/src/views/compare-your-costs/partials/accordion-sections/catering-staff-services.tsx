import React, { useState } from "react";
import { CateringStaffServicesProps } from "src/views/compare-your-costs/partials/accordion-sections/types";
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

export const CateringStaffServices: React.FC<CateringStaffServicesProps> = ({
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

  const netCateringBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.netCateringCosts,
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

  const cateringStaffBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.cateringStaffCosts,
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

  const cateringSuppliesBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.cateringSuppliesCosts,
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

  const incomeCateringBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.incomeCatering,
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
              id="accordion-heading-catering-staff-services"
            >
              Catering staff and services
            </span>
          </h2>
        </div>
        <div
          id="accordion-content-catering-staff-services"
          className="govuk-accordion__section-content"
          aria-labelledby="accordion-heading-catering-staff-services"
          role="region"
        >
          <HorizontalBarChartWrapper
            data={netCateringBarData}
            chartId="net-catering-costs"
          >
            <h3 className="govuk-heading-s">Net catering costs</h3>
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="net-catering-costs"
              defaultValue={dimension}
            />
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={cateringStaffBarData}
            chartId="catering-staff-costs"
          >
            <h3 className="govuk-heading-s">Catering staff costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={cateringSuppliesBarData}
            chartId="catering-supplies-costs"
          >
            <h3 className="govuk-heading-s">Catering supplies costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={incomeCateringBarData}
            chartId="income-from-catering"
          >
            <h3 className="govuk-heading-s">Income from catering</h3>
          </HorizontalBarChartWrapper>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
