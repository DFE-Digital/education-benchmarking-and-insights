import React, { useCallback, useEffect, useMemo, useState } from "react";
import { OtherCostsData } from "src/views/compare-your-trust/partials/accordion-sections/types";
import { CostCategories, PoundsPerPupil } from "src/components";
import { useCentralServicesBreakdownContext } from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import { ExpenditureApi, OtherCostsDataTrustExpenditure } from "src/services";
import {
  BreakdownExclude,
  BreakdownInclude,
} from "src/components/central-services-breakdown";
import { AccordionSection } from "src/composed/accordion-section";

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

  const handleDimensionChange = (value: string) => {
    const dimension =
      CostCategories.find((x) => x.value === value) ?? PoundsPerPupil;
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

  return (
    <AccordionSection
      charts={[
        { data: totalOtherCostsBarData, title: "Total other costs" },
        {
          data: otherInsurancePremiumsCostsBarData,
          title: "Other insurance premiums costs",
        },
        {
          data: directRevenueFinancingCostsBarData,
          title: "Direct revenue financing costs",
        },
        {
          data: groundsMaintenanceCostsBarData,
          title: "Ground maintenance costs",
        },
        {
          data: indirectEmployeeExpensesBarData,
          title: "Indirect employee expenses",
        },
        {
          data: interestChargesLoanBankBarData,
          title: "Interest charges for loan and bank",
        },
        { data: privateFinanceInitiativeChargesBarData, title: "PFI charges" },
        { data: rentRatesCostsBarData, title: "Rent and rates costs" },
        {
          data: specialFacilitiesCostsBarData,
          title: "Special facilities costs",
        },
        {
          data: staffDevelopmentTrainingCostsBarData,
          title: "Staff development and training costs",
        },
        {
          data: staffRelatedInsuranceCostsBarData,
          title: "Staff-related insurance costs",
        },
        {
          data: supplyTeacherInsurableCostsBarData,
          title: "Supply teacher insurance costs",
        },
      ]}
      dimension={dimension}
      handleDimensionChange={handleDimensionChange}
      hasNoData={data?.length === 0}
      index={9}
      showCopyImageButton
      title="Other costs"
      trust
    />
  );
};
