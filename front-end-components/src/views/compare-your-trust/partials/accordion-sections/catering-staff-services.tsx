import React, { useCallback, useEffect, useMemo, useState } from "react";
import { CateringStaffServicesData } from "src/views/compare-your-trust/partials/accordion-sections/types";
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
  CateringStaffServicesTrustExpenditure,
} from "src/services";

export const CateringStaffServices: React.FC<{
  id: string;
}> = ({ id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const [data, setData] = useState<
    CateringStaffServicesTrustExpenditure[] | null
  >();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.trust<CateringStaffServicesTrustExpenditure>(
      id,
      dimension.value,
      "CateringStaffServices",
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

  const totalCateringBarData: HorizontalBarChartWrapperData<CateringStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((trust) => {
            return {
              ...trust,
              totalValue: trust.totalGrossCateringCosts ?? 0,
              schoolValue: trust.schoolTotalGrossCateringCosts ?? 0,
              centralValue: trust.centralTotalGrossCateringCosts ?? 0,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const cateringStaffBarData: HorizontalBarChartWrapperData<CateringStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((trust) => {
            return {
              ...trust,
              totalValue: trust.cateringStaffCosts ?? 0,
              schoolValue: trust.schoolCateringStaffCosts ?? 0,
              centralValue: trust.centralCateringStaffCosts ?? 0,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const cateringSuppliesBarData: HorizontalBarChartWrapperData<CateringStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((trust) => {
            return {
              ...trust,
              totalValue: trust.cateringSuppliesCosts ?? 0,
              schoolValue: trust.schoolCateringSuppliesCosts ?? 0,
              centralValue: trust.centralCateringSuppliesCosts ?? 0,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const elementId = "catering-staff-and-services";
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
            data={totalCateringBarData}
            chartName="total catering costs"
          >
            <h3 className="govuk-heading-s">Total catering costs</h3>
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="total-catering-costs"
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
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
