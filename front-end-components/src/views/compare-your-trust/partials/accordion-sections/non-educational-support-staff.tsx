import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import { NonEducationalSupportStaffData } from "src/views/compare-your-trust/partials/accordion-sections/types";
import { CostCategories, PoundsPerPupil } from "src/components";
import {
  useCentralServicesBreakdownContext,
  ShowHighExecutivePayContext,
} from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import {
  ExpenditureApi,
  NonEducationalSupportStaffTrustExpenditure,
} from "src/services";
import {
  BreakdownExclude,
  BreakdownInclude,
} from "src/components/central-services-breakdown";
import { AccordionSection } from "src/composed/accordion-section";
import { useAbort } from "src/hooks/useAbort";
import { payBandFormatter } from "src/components/charts/utils";

export const NonEducationalSupportStaff: React.FC<{
  id: string;
}> = ({ id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const { breakdown } = useCentralServicesBreakdownContext(true);
  const showHighExecutivePay = useContext(ShowHighExecutivePayContext);
  const [data, setData] = useState<
    NonEducationalSupportStaffTrustExpenditure[] | null
  >();
  const { abort, signal } = useAbort();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.trust<NonEducationalSupportStaffTrustExpenditure>(
      id,
      dimension.value,
      "NonEducationalSupportStaff",
      breakdown === BreakdownExclude,
      [signal]
    );
  }, [id, dimension.value, breakdown, signal]);

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
    abort();

    const dimension =
      CostCategories.find((x) => x.value === value) ?? PoundsPerPupil;
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

  const highestSalaryBandBarData: HorizontalBarChartWrapperData<NonEducationalSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.highestSalaryEmolumentBandValue,
                  schoolValue: undefined,
                  centralValue: undefined,
                };
              })
            : [],
        tableHeadings: ["Trust name", "Highest emolument band"],
      };
    }, [data]);

  const charts = [
    {
      data: totalNonEducationalBarData,
      title: "Total non-educational support staff costs",
    },
    {
      data: administrativeClericalBarData,
      title: "Administrative and clerical staff costs",
    },
    {
      data: auditorsCostsBarData,
      title: "Auditors costs",
    },
    {
      data: otherStaffCostsBarData,
      title: "Other staff costs",
    },
    {
      data: professionalServicesBarData,
      title: "Professional services (non-curriculum) costs",
    },
  ];

  const highExecutivePayChart = {
    data: highestSalaryBandBarData,
    title: "High executive pay",
    override: {
      valueUnit: "amount",
      valueLabel: "Highest emolument band",
      valueFormatter: payBandFormatter,
      suppressNegativeOrZero: true,
      suppressNegativeOrZeroMessage:
        "Only displaying trusts with pay band data.",
    },
  };

  if (showHighExecutivePay) {
    charts.push(highExecutivePayChart);
  }

  return (
    <AccordionSection
      charts={charts}
      dimension={dimension}
      handleDimensionChange={handleDimensionChange}
      hasNoData={data?.length === 0}
      index={2}
      showCopyImageButton
      title="Non-educational support staff and services"
      trust
    />
  );
};
