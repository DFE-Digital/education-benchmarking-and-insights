import React, { useMemo, useState } from "react";
import {
  AdministrativeSuppliesData,
  AdministrativeSuppliesProps,
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

export const AdministrativeSupplies: React.FC<AdministrativeSuppliesProps> = ({
  schools,
}) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    setDimension(event.target.value);
  };

  const administrativeSuppliesBarData: HorizontalBarChartWrapperData<AdministrativeSuppliesData> =
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
              value: school.administrativeSuppliesCosts,
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
              id="accordion-heading-7"
            >
              Administrative supplies
            </span>
          </h2>
        </div>
        <div
          id="accordion-content-7"
          className="govuk-accordion__section-content"
          aria-labelledby="accordion-heading-7"
          role="region"
        >
          <HorizontalBarChartWrapper
            data={administrativeSuppliesBarData}
            chartName="administrative supplies (non-eductional)"
          >
            <h3 className="govuk-heading-s">
              Administrative supplies (Non-educational)
            </h3>
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="administrative-supplies-non-eductional"
              defaultValue={dimension}
            />
          </HorizontalBarChartWrapper>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
