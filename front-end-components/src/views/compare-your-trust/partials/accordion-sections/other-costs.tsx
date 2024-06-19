import React, { useCallback, useEffect, useMemo, useState } from "react";
import { OtherCostsData } from "src/views/compare-your-trust/partials/accordion-sections/types";
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
import { useHash } from "src/hooks/useHash";
import classNames from "classnames";
import { ExpenditureApi, OtherCostsDataTrustExpenditure } from "src/services";
import {
  BreakdownExclude,
  BreakdownInclude,
} from "src/components/central-services-breakdown";

export const OtherCosts: React.FC<{
  id: string;
}> = ({ id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const { breakdown } = useCentralServicesBreakdownContext(true);
  const [data, setData] = useState<OtherCostsDataTrustExpenditure[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.trust<OtherCostsDataTrustExpenditure>(
      id,
      dimension.value,
      "Other",
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

  const totalOtherCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.totalOtherCosts ?? 0,
                  schoolValue: trust.schoolTotalOtherCosts ?? 0,
                  centralValue: trust.centralTotalOtherCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const otherInsurancePremiumsCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.otherInsurancePremiumsCosts ?? 0,
                  schoolValue: trust.schoolOtherInsurancePremiumsCosts ?? 0,
                  centralValue: trust.centralOtherInsurancePremiumsCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const directRevenueFinancingCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.directRevenueFinancingCosts ?? 0,
                  schoolValue: trust.schoolDirectRevenueFinancingCosts ?? 0,
                  centralValue: trust.centralDirectRevenueFinancingCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const groundsMaintenanceCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.groundsMaintenanceCosts ?? 0,
                  schoolValue: trust.schoolGroundsMaintenanceCosts ?? 0,
                  centralValue: trust.centralGroundsMaintenanceCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const indirectEmployeeExpensesBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.indirectEmployeeExpenses ?? 0,
                  schoolValue: trust.schoolIndirectEmployeeExpenses ?? 0,
                  centralValue: trust.centralIndirectEmployeeExpenses ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const interestChargesLoanBankBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.interestChargesLoanBank ?? 0,
                  schoolValue: trust.schoolInterestChargesLoanBank ?? 0,
                  centralValue: trust.centralInterestChargesLoanBank ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const privateFinanceInitiativeChargesBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.privateFinanceInitiativeCharges ?? 0,
                  schoolValue: trust.schoolPrivateFinanceInitiativeCharges ?? 0,
                  centralValue:
                    trust.centralPrivateFinanceInitiativeCharges ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const rentRatesCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.rentRatesCosts ?? 0,
                  schoolValue: trust.schoolRentRatesCosts ?? 0,
                  centralValue: trust.centralRentRatesCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const specialFacilitiesCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.specialFacilitiesCosts ?? 0,
                  schoolValue: trust.schoolSpecialFacilitiesCosts ?? 0,
                  centralValue: trust.centralSpecialFacilitiesCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const staffDevelopmentTrainingCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.staffDevelopmentTrainingCosts ?? 0,
                  schoolValue: trust.schoolStaffDevelopmentTrainingCosts ?? 0,
                  centralValue: trust.centralStaffDevelopmentTrainingCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const staffRelatedInsuranceCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.staffRelatedInsuranceCosts ?? 0,
                  schoolValue: trust.schoolStaffRelatedInsuranceCosts ?? 0,
                  centralValue: trust.centralStaffRelatedInsuranceCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const supplyTeacherInsurableCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.supplyTeacherInsurableCosts ?? 0,
                  schoolValue: trust.schoolSupplyTeacherInsurableCosts ?? 0,
                  centralValue: trust.centralSupplyTeacherInsurableCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const communityFocusedSchoolStaffBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.communityFocusedSchoolStaff ?? 0,
                  schoolValue: trust.schoolCommunityFocusedSchoolStaff ?? 0,
                  centralValue: trust.centralCommunityFocusedSchoolStaff ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const communityFocusedSchoolCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.communityFocusedSchoolCosts ?? 0,
                  schoolValue: trust.schoolCommunityFocusedSchoolCosts ?? 0,
                  centralValue: trust.centralCommunityFocusedSchoolCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const elementId = "other";
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
              id="accordion-heading-9"
            >
              Other costs
            </span>
          </h2>
        </div>
        <div
          id="accordion-content-9"
          className="govuk-accordion__section-content"
          aria-labelledby="accordion-heading-9"
          role="region"
        >
          <HorizontalBarChartWrapper
            data={totalOtherCostsBarData}
            chartName="total other costs"
            trust
          >
            <h3 className="govuk-heading-s">Total other costs</h3>
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="total-otehr-costs"
              value={dimension.value}
            />
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={otherInsurancePremiumsCostsBarData}
            chartName="other insurance premiums costs"
            trust
          >
            <h3 className="govuk-heading-s">Other insurance premiums costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={directRevenueFinancingCostsBarData}
            chartName="direct revenue financing costs"
            trust
          >
            <h3 className="govuk-heading-s">Direct revenue financing costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={groundsMaintenanceCostsBarData}
            chartName="ground maintenance costs"
            trust
          >
            <h3 className="govuk-heading-s">Ground maintenance costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={indirectEmployeeExpensesBarData}
            chartName="indirect employee expenses"
            trust
          >
            <h3 className="govuk-heading-s">Indirect employee expenses</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={interestChargesLoanBankBarData}
            chartName="interest charges for loan and bank"
            trust
          >
            <h3 className="govuk-heading-s">
              Interest charges for loan and bank
            </h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={privateFinanceInitiativeChargesBarData}
            chartName="PFI charges"
            trust
          >
            <h3 className="govuk-heading-s">PFI charges</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={rentRatesCostsBarData}
            chartName="rent and rates costs"
            trust
          >
            <h3 className="govuk-heading-s">Rent and rates costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={specialFacilitiesCostsBarData}
            chartName="special facilities costs"
            trust
          >
            <h3 className="govuk-heading-s">Special facilities costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={staffDevelopmentTrainingCostsBarData}
            chartName="staff development and training costs"
            trust
          >
            <h3 className="govuk-heading-s">
              Staff development and training costs
            </h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={staffRelatedInsuranceCostsBarData}
            chartName="staff-related insurance costs"
            trust
          >
            <h3 className="govuk-heading-s">Staff-related insurance costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={supplyTeacherInsurableCostsBarData}
            chartName="supply teacher insurance costs"
            trust
          >
            <h3 className="govuk-heading-s">Supply teacher insurance costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={communityFocusedSchoolStaffBarData}
            chartName="community focused school staff (maintained schools only)"
            trust
          >
            <h3 className="govuk-heading-s">
              Community focused school staff (maintained schools only)
            </h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={communityFocusedSchoolCostsBarData}
            chartName="community focused school costs (maintained schools only)"
            trust
          >
            <h3 className="govuk-heading-s">
              Community focused school costs (maintained schools only)
            </h3>
          </HorizontalBarChartWrapper>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
