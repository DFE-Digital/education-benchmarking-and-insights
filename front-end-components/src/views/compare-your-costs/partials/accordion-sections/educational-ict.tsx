import React, { useMemo, useState } from "react";
import {
  EducationalIctData,
  EducationalIctProps,
} from "src/views/compare-your-costs/partials/accordion-sections/types";
import {
  CalculateCostValue,
  CostCategories,
  DimensionHeading,
  PoundsPerPupil,
  ChartDimensions,
} from "src/components";
import { ChartDimensionContext } from "src/contexts";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";

export const EducationalIct: React.FC<EducationalIctProps> = ({ schools }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    setDimension(event.target.value);
  };

  const learningResourcesBarData: HorizontalBarChartWrapperData<EducationalIctData> =
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
              value: school.learningResourcesIctCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools]);

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
            chartName="eductional learning resources costs"
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
