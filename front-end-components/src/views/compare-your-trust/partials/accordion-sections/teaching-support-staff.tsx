import React, { useCallback, useEffect, useMemo, useState } from "react";
import { TeachingSupportStaffData } from "src/views/compare-your-trust/partials/accordion-sections/types";
import {
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
import {
  ExpenditureApi,
  TeachingSupportStaffTrustExpenditure,
} from "src/services";

export const TeachingSupportStaff: React.FC<{ id: string }> = ({ id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const [data, setData] = useState<
    TeachingSupportStaffTrustExpenditure[] | null
  >();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.trust<TeachingSupportStaffTrustExpenditure>(
      id,
      dimension.value,
      "TeachingTeachingSupportStaff",
      true
    );
  }, [id, dimension]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const tableHeadings = useMemo(
    () => [
      "Trust name",
      `Total ${dimension.heading}`,
      `School ${dimension.heading}`,
      `Central ${dimension.heading}`,
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
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.totalTeachingSupportStaffCosts ?? 0,
                  schoolValue: trust.schoolTotalTeachingSupportStaffCosts ?? 0,
                  centralValue:
                    trust.centralTotalTeachingSupportStaffCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const teachingStaffBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.teachingStaffCosts ?? 0,
                  schoolValue: trust.schoolTeachingStaffCosts ?? 0,
                  centralValue: trust.centralTeachingStaffCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const supplyTeachingBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.supplyTeachingStaffCosts ?? 0,
                  schoolValue: trust.schoolSupplyTeachingStaffCosts ?? 0,
                  centralValue: trust.centralSupplyTeachingStaffCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const educationalConsultancyBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.educationalConsultancyCosts ?? 0,
                  schoolValue: trust.schoolEducationalConsultancyCosts ?? 0,
                  centralValue: trust.centralEducationalConsultancyCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const educationSupportStaffBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.educationSupportStaffCosts ?? 0,
                  schoolValue: trust.schoolEducationSupportStaffCosts ?? 0,
                  centralValue: trust.centralEducationSupportStaffCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const agencySupplyBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.agencySupplyTeachingStaffCosts ?? 0,
                  schoolValue: trust.schoolAgencySupplyTeachingStaffCosts ?? 0,
                  centralValue:
                    trust.centralAgencySupplyTeachingStaffCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const elementId = "teaching-and-teaching-support-staff";
  const [hash] = useHash();

  return (
    <ChartDimensionContext.Provider value={dimension}>
      <div
        className={classNames("govuk-accordion__section", {
          "govuk-accordion__section--expanded": hash === `#${elementId}`,
        })}
        id={elementId}
      >
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
          >
            <h3 className="govuk-heading-s">Teaching staff costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={supplyTeachingBarData}
            chartName="supply teaching staff costs"
          >
            <h3 className="govuk-heading-s">Supply teaching staff costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={educationalConsultancyBarData}
            chartName="educational consultancy costs"
          >
            <h3 className="govuk-heading-s">Educational consultancy costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={educationSupportStaffBarData}
            chartName="educational support staff costs"
          >
            <h3 className="govuk-heading-s">Educational support staff costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={agencySupplyBarData}
            chartName="agency supply teaching staff costs"
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
