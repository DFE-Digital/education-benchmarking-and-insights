import React, { useState } from "react";
import { UtilitiesProps } from "src/views/compare-your-costs/partials/accordion-sections/types";
import {
  CalculatePremisesValue,
  DimensionHeading,
  PoundsPerMetreSq,
  PremisesCategories,
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
  ChartDimensions,
} from "src/components";
import { ChartDimensionContext } from "src/contexts";

export const Utilities: React.FC<UtilitiesProps> = ({ schools }) => {
  const [dimension, setDimension] = useState(PoundsPerMetreSq);
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

  const totalUtilitiesCostsBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculatePremisesValue({
          dimension: dimension,
          value: school.totalUtilitiesCosts,
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

  const energyBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculatePremisesValue({
          dimension: dimension,
          value: school.energyCosts,
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

  const waterSewerageBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculatePremisesValue({
          dimension: dimension,
          value: school.waterSewerageCosts,
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
              id="accordion-heading-6"
            >
              Utilities
            </span>
          </h2>
        </div>
        <div
          id="accordion-content-6"
          className="govuk-accordion__section-content"
          aria-labelledby="accordion-heading-6"
          role="region"
        >
          <HorizontalBarChartWrapper
            data={totalUtilitiesCostsBarData}
            chartName="total utilities costs"
          >
            <h3 className="govuk-heading-s">Total utilities costs</h3>
            <ChartDimensions
              dimensions={PremisesCategories}
              handleChange={handleSelectChange}
              elementId="total-utilities-costs"
              defaultValue={dimension}
            />
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={energyBarData}
            chartName="energy costs"
          >
            <h3 className="govuk-heading-s">Energy costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={waterSewerageBarData}
            chartName="water and sewerage costs"
          >
            <h3 className="govuk-heading-s">Water and sewerage costs</h3>
          </HorizontalBarChartWrapper>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
