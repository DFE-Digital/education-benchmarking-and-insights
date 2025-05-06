import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  CompareYourCostsProps,
  OtherCostsData,
} from "src/views/compare-your-costs/partials/accordion-sections/types";
import { CostCategories, PoundsPerPupil } from "src/components";
import { PhaseContext, CustomDataContext } from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import { ExpenditureApi, OtherCostsDataExpenditure } from "src/services";
import { AccordionSection } from "src/composed/accordion-section";
import { useAbort } from "src/hooks/useAbort";

export const OtherCosts: React.FC<CompareYourCostsProps> = ({ type, id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [data, setData] = useState<OtherCostsDataExpenditure[] | null>();
  const { abort, signal } = useAbort();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<OtherCostsDataExpenditure>(
      type,
      id,
      dimension.value,
      "Other",
      phase,
      customDataId,
      [signal]
    );
  }, [type, id, dimension.value, phase, customDataId, signal]);

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

  const handleDimensionChange = (value: string) => {
    abort();

    const dimension =
      CostCategories.find((x) => x.value === value) ?? PoundsPerPupil;
    setDimension(dimension);
  };

  const totalOtherCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.totalOtherCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const otherInsurancePremiumsCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.otherInsurancePremiumsCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const directRevenueFinancingCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.directRevenueFinancingCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const groundsMaintenanceCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.groundsMaintenanceCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const indirectEmployeeExpensesBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.indirectEmployeeExpenses,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const interestChargesLoanBankBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.interestChargesLoanBank,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const privateFinanceInitiativeChargesBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.privateFinanceInitiativeCharges,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const rentRatesCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.rentRatesCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const specialFacilitiesCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.specialFacilitiesCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const staffDevelopmentTrainingCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.staffDevelopmentTrainingCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const staffRelatedInsuranceCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.staffRelatedInsuranceCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const supplyTeacherInsurableCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.supplyTeacherInsurableCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const communityFocusedSchoolStaffBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.communityFocusedSchoolStaff,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const communityFocusedSchoolCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.communityFocusedSchoolCosts,
            };
          }) ?? [],
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
        {
          data: communityFocusedSchoolStaffBarData,
          title: "Community focused school staff (maintained schools only)",
        },
        {
          data: communityFocusedSchoolCostsBarData,
          title: "Community focused school costs (maintained schools only)",
        },
      ]}
      dimension={dimension}
      handleDimensionChange={handleDimensionChange}
      hasNoData={data?.length === 0}
      index={9}
      showCopyImageButton
      title="Other costs"
    />
  );
};
