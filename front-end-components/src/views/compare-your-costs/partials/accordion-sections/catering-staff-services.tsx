import React, { useMemo, useState } from "react";
import {
  CateringStaffServicesData,
  CateringStaffServicesProps,
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

export const CateringStaffServices: React.FC<CateringStaffServicesProps> = ({
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

  const netCateringBarData: HorizontalBarChartWrapperData<CateringStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension: dimension.value,
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
              dimension: dimension.value,
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
              dimension: dimension.value,
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
              dimension: dimension.value,
              value: school.incomeCatering,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const id = "catering-staff-and-services";
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
          >
            <h3 className="govuk-heading-s">Net catering costs</h3>
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="net-catering-costs"
              defaultValue={dimension.value}
            />
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={cateringStaffBarData}
            chartName="catering staff costs"
          >
            <h3 className="govuk-heading-s">Catering staff costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={cateringSuppliesBarData}
            chartName="catering supplies costs"
          >
            <h3 className="govuk-heading-s">Catering supplies costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={incomeCateringBarData}
            chartName="income from catering"
          >
            <h3 className="govuk-heading-s">Income from catering</h3>
          </HorizontalBarChartWrapper>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
