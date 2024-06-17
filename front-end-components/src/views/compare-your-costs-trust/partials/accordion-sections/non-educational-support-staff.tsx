import React, { useCallback, useEffect, useMemo, useState } from "react";
import { NonEducationalSupportStaffData } from "src/views/compare-your-costs-trust/partials/accordion-sections/types";
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
import classNames from "classnames";
import { useHash } from "src/hooks/useHash";
import { TrustExpenditure, ExpenditureApi } from "src/services";

export const NonEducationalSupportStaff: React.FC<{
  type: string;
  id: string;
}> = ({ type, id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const [data, setData] = useState<TrustExpenditure[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.trust(
      type,
      id,
      dimension.value,
      "NonEducationalSupportStaff",
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
      CostCategories.find((x) => x.value === event.target.value) ??
      PoundsPerPupil;
    setDimension(dimension);
  };

  const administrativeClericalBarData: HorizontalBarChartWrapperData<NonEducationalSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((trust) => {
            return {
              ...trust,
              totalValue: trust.administrativeClericalStaffCosts ?? 0,
              schoolValue: trust.schoolAdministrativeClericalStaffCosts ?? 0,
              centralValue: trust.centralAdministrativeClericalStaffCosts ?? 0,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const totalNonEducationalBarData: HorizontalBarChartWrapperData<NonEducationalSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((trust) => {
            return {
              ...trust,
              totalValue: trust.totalNonEducationalSupportStaffCosts ?? 0,
              schoolValue:
                trust.schoolTotalNonEducationalSupportStaffCosts ?? 0,
              centralValue:
                trust.centralTotalNonEducationalSupportStaffCosts ?? 0,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const auditorsCostsBarData: HorizontalBarChartWrapperData<NonEducationalSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((trust) => {
            return {
              ...trust,
              totalValue: trust.auditorsCosts ?? 0,
              schoolValue: trust.schoolAuditorsCosts ?? 0,
              centralValue: trust.centralAuditorsCosts ?? 0,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const otherStaffCostsBarData: HorizontalBarChartWrapperData<NonEducationalSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((trust) => {
            return {
              ...trust,
              totalValue: trust.otherStaffCosts ?? 0,
              schoolValue: trust.schoolOtherStaffCosts ?? 0,
              centralValue: trust.centralOtherStaffCosts ?? 0,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const professionalServicesBarData: HorizontalBarChartWrapperData<NonEducationalSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((trust) => {
            return {
              ...trust,
              totalValue: trust.professionalServicesNonCurriculumCosts ?? 0,
              schoolValue:
                trust.schoolProfessionalServicesNonCurriculumCosts ?? 0,
              centralValue:
                trust.centralProfessionalServicesNonCurriculumCosts ?? 0,
            };
          }) ?? [],
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
          >
            <h3 className="govuk-heading-s">
              Total non-educational support staff costs
            </h3>
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="total-non-educational-support-staff-costs"
              defaultValue={dimension.value}
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
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
