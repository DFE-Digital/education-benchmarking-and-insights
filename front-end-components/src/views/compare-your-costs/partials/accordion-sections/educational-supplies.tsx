import React, { useMemo, useState } from "react";
import {
  EducationalSuppliesData,
  EducationalSuppliesProps,
} from "src/views/compare-your-costs/partials/accordion-sections/types";
import {
  CalculateCostValue,
  CostCategories,
  PoundsPerPupil,
  ChartDimensions,
} from "src/components";
import { ChartDimensionContext } from "src/contexts";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { useHash } from "src/hooks/useHash";
import classNames from "classnames";

export const EducationalSupplies: React.FC<EducationalSuppliesProps> = ({
  schools,
}) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const tableHeadings = useMemo(
    () => [
      "School name",
      "Local Authority",
      "School type",
      "Number of pupils",
      dimension.heading,
    ],
    [dimension]
  );

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    const dimension =
      CostCategories.find((x) => x.value === event.target.value) ??
      PoundsPerPupil;
    setDimension(dimension);
  };

  const totalEducationalSuppliesBarData: HorizontalBarChartWrapperData<EducationalSuppliesData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension: dimension.value,
              value: school.totalEducationalSuppliesCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const examinationFeesBarData: HorizontalBarChartWrapperData<EducationalSuppliesData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension: dimension.value,
              value: school.examinationFeesCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const breakdownEducationalBarData: HorizontalBarChartWrapperData<EducationalSuppliesData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension: dimension.value,
              value: school.breakdownEducationalSuppliesCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const learningResourcesBarData: HorizontalBarChartWrapperData<EducationalSuppliesData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension: dimension.value,
              value: school.learningResourcesNonIctCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const id = "educational-supplies";
  const [hash] = useHash();

  return (
    <ChartDimensionContext.Provider value={dimension}>
      <div
        className={classNames("govuk-accordion__section", {
          "govuk-accordion__section--expanded": hash === `#${id}`,
        })}
        id={id}
      >
        <div className="govuk-accordion__section-header">
          <h2 className="govuk-accordion__section-heading">
            <span
              className="govuk-accordion__section-button"
              id="accordion-heading-3"
            >
              Educational supplies
            </span>
          </h2>
        </div>
        <div
          id="accordion-content-3"
          className="govuk-accordion__section-content"
          aria-labelledby="accordion-heading-3"
          role="region"
        >
          <HorizontalBarChartWrapper
            data={totalEducationalSuppliesBarData}
            chartName="total educational supplies costs"
          >
            <h3 className="govuk-heading-s">
              Total educational supplies costs
            </h3>
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="total-educational-supplies-costs"
              defaultValue={dimension.value}
            />
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={examinationFeesBarData}
            chartName="examination fees costs"
          >
            <h3 className="govuk-heading-s">Examination fees costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={breakdownEducationalBarData}
            chartName="breakdown of eductional supplies costs"
          >
            <h3 className="govuk-heading-s">
              Breakdown of educational supplies costs
            </h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={learningResourcesBarData}
            chartName="learning resource (not ICT equipment) costs"
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
