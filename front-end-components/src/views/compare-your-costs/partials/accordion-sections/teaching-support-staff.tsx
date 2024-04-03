import React, { useMemo, useState } from "react";
import {
  TeachingSupportStaffData,
  TeachingSupportStaffProps,
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

export const TeachingSupportStaff: React.FC<TeachingSupportStaffProps> = ({
  schools,
}) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
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
      CostCategories.find((x) => x.value === event.target.value) ??
      PoundsPerPupil;
    setDimension(dimension);
  };

  const totalTeachingBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension: dimension.value,
              value: school.totalTeachingSupportStaffCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const teachingStaffBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension: dimension.value,
              value: school.teachingStaffCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const supplyTeachingBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension: dimension.value,
              value: school.supplyTeachingStaffCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const educationalConsultancyBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension: dimension.value,
              value: school.educationalConsultancyCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const educationSupportStaffBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension: dimension.value,
              value: school.educationSupportStaffCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const agencySupplyBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension: dimension.value,
              value: school.agencySupplyTeachingStaffCosts,
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
            chartName="total teaching and support staff cost"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">
              Total teaching and teaching support staff costs
            </h3>
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="total-teaching-support-staff-cost"
              defaultValue={dimension.value}
            />
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={teachingStaffBarData}
            chartName="teaching staff costs"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">Teaching staff costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={supplyTeachingBarData}
            chartName="supply teaching staff costs"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">Supply teaching staff costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={educationalConsultancyBarData}
            chartName="educational consultancy costs"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">Educational consultancy costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={educationSupportStaffBarData}
            chartName="educational support staff costs"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">Educational support staff costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={agencySupplyBarData}
            chartName="agency supply teaching staff costs"
            valueUnit="currency"
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
