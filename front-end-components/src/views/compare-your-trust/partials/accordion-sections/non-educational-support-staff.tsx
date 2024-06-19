import React, { useCallback, useEffect, useMemo, useState } from "react";
import { NonEducationalSupportStaffData } from "src/views/compare-your-trust/partials/accordion-sections/types";
import {
  CostCategories,
  PoundsPerPupil,
  ChartDimensions,
} from "src/components";
import {
  ChartDimensionContext,
  useCentralServicesBreakdownContext,
} from "src/contexts";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import classNames from "classnames";
import { useHash } from "src/hooks/useHash";
import {
  ExpenditureApi,
  NonEducationalSupportStaffTrustExpenditure,
} from "src/services";
import {
  BreakdownExclude,
  BreakdownInclude,
} from "src/components/central-services-breakdown";

export const NonEducationalSupportStaff: React.FC<{
  id: string;
}> = ({ id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const { breakdown } = useCentralServicesBreakdownContext(true);
  const [data, setData] = useState<
    NonEducationalSupportStaffTrustExpenditure[] | null
  >();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.trust<NonEducationalSupportStaffTrustExpenditure>(
      id,
      dimension.value,
      "NonEducationalSupportStaff",
      breakdown === BreakdownExclude
    );
  }, [id, dimension, breakdown]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const tableHeadings = useMemo(() => {
    const headings = ["Trust name", `Total ${dimension.heading}`];
    if (breakdown === BreakdownInclude) {
      headings.push(
        `School ${dimension.heading}`,
        `Central ${dimension.heading}`
      );
    }
    return headings;
  }, [dimension, breakdown]);

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
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.administrativeClericalStaffCosts ?? 0,
                  schoolValue:
                    trust.schoolAdministrativeClericalStaffCosts ?? 0,
                  centralValue:
                    trust.centralAdministrativeClericalStaffCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const totalNonEducationalBarData: HorizontalBarChartWrapperData<NonEducationalSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.totalNonEducationalSupportStaffCosts ?? 0,
                  schoolValue:
                    trust.schoolTotalNonEducationalSupportStaffCosts ?? 0,
                  centralValue:
                    trust.centralTotalNonEducationalSupportStaffCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const auditorsCostsBarData: HorizontalBarChartWrapperData<NonEducationalSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.auditorsCosts ?? 0,
                  schoolValue: trust.schoolAuditorsCosts ?? 0,
                  centralValue: trust.centralAuditorsCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const otherStaffCostsBarData: HorizontalBarChartWrapperData<NonEducationalSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.otherStaffCosts ?? 0,
                  schoolValue: trust.schoolOtherStaffCosts ?? 0,
                  centralValue: trust.centralOtherStaffCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const professionalServicesBarData: HorizontalBarChartWrapperData<NonEducationalSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.professionalServicesNonCurriculumCosts ?? 0,
                  schoolValue:
                    trust.schoolProfessionalServicesNonCurriculumCosts ?? 0,
                  centralValue:
                    trust.centralProfessionalServicesNonCurriculumCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const elementId = "non-educational-support-staff";
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
          <HorizontalBarChartWrapper
            data={totalNonEducationalBarData}
            chartName="total non-educational support staff costs"
            trust
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
            trust
          >
            <h3 className="govuk-heading-s">
              Administrative and clerical staff costs
            </h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={auditorsCostsBarData}
            chartName="auditors costs"
            trust
          >
            <h3 className="govuk-heading-s">Auditors costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={otherStaffCostsBarData}
            chartName="other staff costs"
            trust
          >
            <h3 className="govuk-heading-s">Other staff costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={professionalServicesBarData}
            chartName="profession services (non-curriculum) costs"
            trust
          >
            <h3 className="govuk-heading-s">
              Professional services (non-curriculum) costs
            </h3>
          </HorizontalBarChartWrapper>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
