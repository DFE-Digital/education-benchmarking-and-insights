import React, { useMemo, useState } from "react";
import {
  UtilitiesData,
  UtilitiesProps,
} from "src/views/compare-your-costs/partials/accordion-sections/types";
import {
  CalculatePremisesValue,
  DimensionHeading,
  PoundsPerMetreSq,
  PremisesCategories,
  ChartDimensions,
} from "src/components";
import { ChartDimensionContext } from "src/contexts";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";

export const Utilities: React.FC<UtilitiesProps> = ({ schools }) => {
  const [dimension, setDimension] = useState(PoundsPerMetreSq);
  const tableHeadings = useMemo(
    () => [
      "School name",
      "Local Authority",
      "School type",
      "Number of pupils",
      DimensionHeading(dimension.value),
    ],
    [dimension]
  );

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    const dimension =
      PremisesCategories.find((x) => x.value === event.target.value) ??
      PoundsPerMetreSq;
    setDimension(dimension);
  };

  const totalUtilitiesCostsBarData: HorizontalBarChartWrapperData<UtilitiesData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculatePremisesValue({
              dimension: dimension.value,
              value: school.totalUtilitiesCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const energyBarData: HorizontalBarChartWrapperData<UtilitiesData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculatePremisesValue({
              dimension: dimension.value,
              value: school.energyCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const waterSewerageBarData: HorizontalBarChartWrapperData<UtilitiesData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculatePremisesValue({
              dimension: dimension.value,
              value: school.waterSewerageCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

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
              defaultValue={dimension.value}
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
