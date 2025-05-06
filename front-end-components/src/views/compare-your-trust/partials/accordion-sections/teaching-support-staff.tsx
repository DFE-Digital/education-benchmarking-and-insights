import React, { useCallback, useEffect, useMemo, useState } from "react";
import { TeachingSupportStaffData } from "src/views/compare-your-trust/partials/accordion-sections/types";
import { CostCategories, PoundsPerPupil } from "src/components";
import { useCentralServicesBreakdownContext } from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import {
  ExpenditureApi,
  TeachingSupportStaffTrustExpenditure,
} from "src/services";
import {
  BreakdownExclude,
  BreakdownInclude,
} from "src/components/central-services-breakdown";
import { AccordionSection } from "src/composed/accordion-section";
import { useAbort } from "src/hooks/useAbort";

export const TeachingSupportStaff: React.FC<{ id: string }> = ({ id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const { breakdown } = useCentralServicesBreakdownContext(true);
  const [data, setData] = useState<
    TeachingSupportStaffTrustExpenditure[] | null
  >();
  const { abort, signal } = useAbort();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.trust<TeachingSupportStaffTrustExpenditure>(
      id,
      dimension.value,
      "TeachingTeachingSupportStaff",
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

  const totalTeachingBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.totalTeachingSupportStaffCosts ?? 0,
                  schoolValue: trust.schoolTotalTeachingSupportStaffCosts ?? 0,
                  centralValue:
                    trust.centralTotalTeachingSupportStaffCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const teachingStaffBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.teachingStaffCosts ?? 0,
                  schoolValue: trust.schoolTeachingStaffCosts ?? 0,
                  centralValue: trust.centralTeachingStaffCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const supplyTeachingBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.supplyTeachingStaffCosts ?? 0,
                  schoolValue: trust.schoolSupplyTeachingStaffCosts ?? 0,
                  centralValue: trust.centralSupplyTeachingStaffCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const educationalConsultancyBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.educationalConsultancyCosts ?? 0,
                  schoolValue: trust.schoolEducationalConsultancyCosts ?? 0,
                  centralValue: trust.centralEducationalConsultancyCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const educationSupportStaffBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.educationSupportStaffCosts ?? 0,
                  schoolValue: trust.schoolEducationSupportStaffCosts ?? 0,
                  centralValue: trust.centralEducationSupportStaffCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const agencySupplyBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.agencySupplyTeachingStaffCosts ?? 0,
                  schoolValue: trust.schoolAgencySupplyTeachingStaffCosts ?? 0,
                  centralValue:
                    trust.centralAgencySupplyTeachingStaffCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  return (
    <AccordionSection
      charts={[
        {
          data: totalTeachingBarData,
          title: "Total teaching and teaching support staff costs",
        },
        {
          data: teachingStaffBarData,
          title: "Teaching staff costs",
        },
        {
          data: supplyTeachingBarData,
          title: "Supply teaching staff costs",
        },
        {
          data: educationalConsultancyBarData,
          title: "Educational consultancy costs",
        },
        {
          data: educationSupportStaffBarData,
          title: "Educational support staff costs",
        },
        {
          data: agencySupplyBarData,
          title: "Agency supply teaching staff costs",
        },
      ]}
      dimension={dimension}
      handleDimensionChange={handleDimensionChange}
      hasNoData={data?.length === 0}
      index={1}
      showCopyImageButton
      title="Teaching and teaching support staff"
      trust
    />
  );
};
