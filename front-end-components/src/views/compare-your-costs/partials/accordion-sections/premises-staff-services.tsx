import React, { useMemo, useState } from "react";
import {
  PremisesStaffServicesData,
  PremisesStaffServicesProps,
} from "src/views/compare-your-costs/partials/accordion-sections/types";
import {
  CalculatePremisesValue,
  PoundsPerMetreSq,
  PremisesCategories,
  ChartDimensions,
} from "src/components";
import { ChartDimensionContext } from "src/contexts";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";

export const PremisesStaffServices: React.FC<PremisesStaffServicesProps> = ({
  schools,
}) => {
  const [dimension, setDimension] = useState(PoundsPerMetreSq);
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
      PremisesCategories.find((x) => x.value === event.target.value) ??
      PoundsPerMetreSq;
    setDimension(dimension);
  };

  const totalPremisesStaffServiceCostsBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculatePremisesValue({
              dimension: dimension.value,
              value: school.totalPremisesStaffServiceCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const cleaningCaretakingBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculatePremisesValue({
              dimension: dimension.value,
              value: school.cleaningCaretakingCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const maintenanceBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculatePremisesValue({
              dimension: dimension.value,
              value: school.maintenancePremisesCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const otherOccupationBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculatePremisesValue({
              dimension: dimension.value,
              value: school.otherOccupationCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const premisesStaffBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculatePremisesValue({
              dimension: dimension.value,
              value: school.premisesStaffCosts,
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
              id="accordion-heading-5"
            >
              Premises staff and services
            </span>
          </h2>
        </div>
        <div
          id="accordion-content-5"
          className="govuk-accordion__section-content"
          aria-labelledby="accordion-heading-5"
          role="region"
        >
          <HorizontalBarChartWrapper
            data={totalPremisesStaffServiceCostsBarData}
            chartName="total premises staff and service costs"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">
              Total premises staff and service costs
            </h3>
            <ChartDimensions
              dimensions={PremisesCategories}
              handleChange={handleSelectChange}
              elementId="total-premises-staff-service-costs"
              defaultValue={dimension.value}
            />
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={cleaningCaretakingBarData}
            chartName="cleaning and caretaking costs"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">Cleaning and caretaking costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={maintenanceBarData}
            chartName="maintenance of premises costs"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">Maintenance of premises costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={otherOccupationBarData}
            chartName="other occupation costs"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">Other occupation costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={premisesStaffBarData}
            chartName="premises staff costs"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">Premises staff costs</h3>
          </HorizontalBarChartWrapper>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
