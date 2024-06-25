import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import { PremisesStaffServicesData } from "src/views/compare-your-costs/partials/accordion-sections/types";
import {
  PoundsPerMetreSq,
  PremisesCategories,
  ChartDimensions,
} from "src/components";
import {
  ChartDimensionContext,
  PhaseContext,
  CustomDataContext,
} from "src/contexts";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { useHash } from "src/hooks/useHash";
import classNames from "classnames";
import { ExpenditureApi, PremisesStaffServicesExpenditure } from "src/services";

export const PremisesStaffServices: React.FC<{
  type: string;
  id: string;
}> = ({ type, id }) => {
  const [dimension, setDimension] = useState(PoundsPerMetreSq);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [data, setData] = useState<PremisesStaffServicesExpenditure[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<PremisesStaffServicesExpenditure>(
      type,
      id,
      dimension.value,
      "PremisesStaffServices",
      phase,
      customDataId
    );
  }, [id, dimension, type, phase, customDataId]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

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
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.totalPremisesStaffServiceCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const cleaningCaretakingBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.cleaningCaretakingCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const maintenanceBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.maintenancePremisesCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const otherOccupationBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.otherOccupationCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const premisesStaffBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.premisesStaffCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const elementId = "premises-staff-and-services";
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
              value={dimension.value}
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
