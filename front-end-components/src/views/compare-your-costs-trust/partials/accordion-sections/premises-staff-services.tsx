import React, { useCallback, useEffect, useMemo, useState } from "react";
import { PremisesStaffServicesData } from "src/views/compare-your-costs-trust/partials/accordion-sections/types";
import {
  PoundsPerMetreSq,
  PremisesCategories,
  ChartDimensions,
} from "src/components";
import { ChartDimensionContext } from "src/contexts";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { useHash } from "src/hooks/useHash";
import classNames from "classnames";
import { TrustExpenditure, ExpenditureApi } from "src/services";

export const PremisesStaffServices: React.FC<{
  type: string;
  id: string;
}> = ({ type, id }) => {
  const [dimension, setDimension] = useState(PoundsPerMetreSq);
  const [data, setData] = useState<TrustExpenditure[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.trust(
      type,
      id,
      dimension.value,
      "PremisesStaffServices",
      true
    );
  }, [id, dimension, type]);

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
      PremisesCategories.find((x) => x.value === event.target.value) ??
      PoundsPerMetreSq;
    setDimension(dimension);
  };

  const totalPremisesStaffServiceCostsBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((trust) => {
            return {
              ...trust,
              totalValue: trust.totalPremisesStaffServiceCosts ?? 0,
              schoolValue: trust.schoolTotalPremisesStaffServiceCosts ?? 0,
              centralValue: trust.centralTotalPremisesStaffServiceCosts ?? 0,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const cleaningCaretakingBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((trust) => {
            return {
              ...trust,
              totalValue: trust.cleaningCaretakingCosts ?? 0,
              schoolValue: trust.schoolCleaningCaretakingCosts ?? 0,
              centralValue: trust.centralCleaningCaretakingCosts ?? 0,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const maintenanceBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((trust) => {
            return {
              ...trust,
              totalValue: trust.maintenancePremisesCosts ?? 0,
              schoolValue: trust.schoolMaintenancePremisesCosts ?? 0,
              centralValue: trust.centralMaintenancePremisesCosts ?? 0,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const otherOccupationBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((trust) => {
            return {
              ...trust,
              totalValue: trust.otherOccupationCosts ?? 0,
              schoolValue: trust.schoolOtherOccupationCosts ?? 0,
              centralValue: trust.centralOtherOccupationCosts ?? 0,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const premisesStaffBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((trust) => {
            return {
              ...trust,
              totalValue: trust.premisesStaffCosts ?? 0,
              schoolValue: trust.schoolPremisesStaffCosts ?? 0,
              centralValue: trust.centralPremisesStaffCosts ?? 0,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const elementId = "premises-and-services";
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
          >
            <h3 className="govuk-heading-s">Cleaning and caretaking costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={maintenanceBarData}
            chartName="maintenance of premises costs"
          >
            <h3 className="govuk-heading-s">Maintenance of premises costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={otherOccupationBarData}
            chartName="other occupation costs"
          >
            <h3 className="govuk-heading-s">Other occupation costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={premisesStaffBarData}
            chartName="premises staff costs"
          >
            <h3 className="govuk-heading-s">Premises staff costs</h3>
          </HorizontalBarChartWrapper>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
