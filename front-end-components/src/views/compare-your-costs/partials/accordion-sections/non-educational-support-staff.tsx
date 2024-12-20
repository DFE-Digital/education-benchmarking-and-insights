import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  CompareYourCostsProps,
  NonEducationalSupportStaffData,
} from "src/views/compare-your-costs/partials/accordion-sections/types";
import {
  CostCategories,
  PoundsPerPupil,
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
import classNames from "classnames";
import { useHash } from "src/hooks/useHash";
import {
  ExpenditureApi,
  NonEducationalSupportStaffExpenditure,
} from "src/services";
import { ErrorBanner } from "src/components/error-banner";

export const NonEducationalSupportStaff: React.FC<CompareYourCostsProps> = ({
  type,
  id,
}) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [data, setData] = useState<
    NonEducationalSupportStaffExpenditure[] | null
  >();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<NonEducationalSupportStaffExpenditure>(
      type,
      id,
      dimension.value,
      "NonEducationalSupportStaff",
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
      CostCategories.find((x) => x.value === event.target.value) ??
      PoundsPerPupil;
    setDimension(dimension);
  };

  const administrativeClericalBarData: HorizontalBarChartWrapperData<NonEducationalSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.administrativeClericalStaffCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const totalNonEducationalBarData: HorizontalBarChartWrapperData<NonEducationalSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.totalNonEducationalSupportStaffCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const auditorsCostsBarData: HorizontalBarChartWrapperData<NonEducationalSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.auditorsCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const otherStaffCostsBarData: HorizontalBarChartWrapperData<NonEducationalSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.otherStaffCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const professionalServicesBarData: HorizontalBarChartWrapperData<NonEducationalSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.professionalServicesNonCurriculumCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const elementId = "non-educational-support-staff-and-services";
  const [hash] = useHash();

  const hasNoData = data?.length === 0;

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
              id="accordion-heading-2"
            >
              Non-educational support staff
            </span>
          </h2>
        </div>
        <div
          id="accordion-content-2"
          className="govuk-accordion__section-content"
          aria-labelledby="accordion-2"
          role="region"
        >
          {hasNoData ? (
            <ErrorBanner
              isRendered={hasNoData}
              message="There isn't enough information available to create a set of similar schools."
            />
          ) : (
            <>
              <HorizontalBarChartWrapper
                data={totalNonEducationalBarData}
                chartName="total non-educational support staff costs"
              >
                <h3 className="govuk-heading-s">
                  Total non-educational support staff costs
                </h3>
                <ChartDimensions
                  dimensions={CostCategories}
                  handleChange={handleSelectChange}
                  elementId="total-non-educational-support-staff-costs"
                  value={dimension.value}
                />
              </HorizontalBarChartWrapper>
              <HorizontalBarChartWrapper
                data={administrativeClericalBarData}
                chartName="administrative and clerical staff costs"
              >
                <h3 className="govuk-heading-s">
                  Administrative and clerical staff costs
                </h3>
              </HorizontalBarChartWrapper>
              <HorizontalBarChartWrapper
                data={auditorsCostsBarData}
                chartName="auditors costs"
              >
                <h3 className="govuk-heading-s">Auditors costs</h3>
              </HorizontalBarChartWrapper>
              <HorizontalBarChartWrapper
                data={otherStaffCostsBarData}
                chartName="other staff costs"
              >
                <h3 className="govuk-heading-s">Other staff costs</h3>
              </HorizontalBarChartWrapper>
              <HorizontalBarChartWrapper
                data={professionalServicesBarData}
                chartName="profession services (non-curriculum) costs"
              >
                <h3 className="govuk-heading-s">
                  Professional services (non-curriculum) costs
                </h3>
              </HorizontalBarChartWrapper>
            </>
          )}
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
