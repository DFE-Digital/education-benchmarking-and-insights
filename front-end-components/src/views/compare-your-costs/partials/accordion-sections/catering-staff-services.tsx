import React, { useMemo, useState } from "react";
import {
  CateringStaffServicesData,
  CateringStaffServicesProps,
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

export const CateringStaffServices: React.FC<CateringStaffServicesProps> = ({
  schools,
}) => {
  const [dimension, setDimension] = useState(PoundsPerPupil.value);
  const tableHeadings = useMemo(
    () => [
      "School name",
      "Local Authority",
      "School type",
      "Number of pupils",
      DimensionHeading(dimension),
    ],
    [dimension]
  );

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    setDimension(event.target.value);
  };

  const netCateringBarData: HorizontalBarChartWrapperData<CateringStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension,
              value: school.netCateringCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const cateringStaffBarData: HorizontalBarChartWrapperData<CateringStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension,
              value: school.cateringStaffCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const cateringSuppliesBarData: HorizontalBarChartWrapperData<CateringStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension,
              value: school.cateringSuppliesCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const incomeCateringBarData: HorizontalBarChartWrapperData<CateringStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension,
              value: school.incomeCatering,
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
              id="accordion-heading-8"
            >
              Catering staff and services
            </span>
          </h2>
        </div>
        <div
          id="accordion-content-8"
          className="govuk-accordion__section-content"
          aria-labelledby="accordion-heading-8"
          role="region"
        >
          <HorizontalBarChartWrapper
            data={netCateringBarData}
            chartName="net catering costs"
            valueUnit="currency"
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
            chartName="catering staff costs"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">Catering staff costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={cateringSuppliesBarData}
            chartName="catering supplies costs"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">Catering supplies costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={incomeCateringBarData}
            chartName="income from catering"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">Income from catering</h3>
          </HorizontalBarChartWrapper>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
