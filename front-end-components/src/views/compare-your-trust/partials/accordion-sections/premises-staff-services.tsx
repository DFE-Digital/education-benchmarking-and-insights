import React, { useCallback, useEffect, useMemo, useState } from "react";
import { PremisesStaffServicesData } from "src/views/compare-your-trust/partials/accordion-sections/types";
import { PoundsPerMetreSq, PremisesCategories } from "src/components";
import { useCentralServicesBreakdownContext } from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import {
  ExpenditureApi,
  PremisesStaffServicesTrustExpenditure,
} from "src/services";
import {
  BreakdownExclude,
  BreakdownInclude,
} from "src/components/central-services-breakdown";
import { AccordionSection } from "src/composed/accordion-section";

export const PremisesStaffServices: React.FC<{
  id: string;
}> = ({ id }) => {
  const [dimension, setDimension] = useState(PoundsPerMetreSq);
  const { breakdown } = useCentralServicesBreakdownContext(true);
  const [data, setData] = useState<
    PremisesStaffServicesTrustExpenditure[] | null
  >();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.trust<PremisesStaffServicesTrustExpenditure>(
      id,
      dimension.value,
      "PremisesStaffServices",
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
      PremisesCategories.find((x) => x.value === value) ?? PoundsPerMetreSq;
    setDimension(dimension);
  };

  const totalPremisesStaffServiceCostsBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.totalPremisesStaffServiceCosts ?? 0,
                  schoolValue: trust.schoolTotalPremisesStaffServiceCosts ?? 0,
                  centralValue:
                    trust.centralTotalPremisesStaffServiceCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const cleaningCaretakingBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.cleaningCaretakingCosts ?? 0,
                  schoolValue: trust.schoolCleaningCaretakingCosts ?? 0,
                  centralValue: trust.centralCleaningCaretakingCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const maintenanceBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.maintenancePremisesCosts ?? 0,
                  schoolValue: trust.schoolMaintenancePremisesCosts ?? 0,
                  centralValue: trust.centralMaintenancePremisesCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const otherOccupationBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.otherOccupationCosts ?? 0,
                  schoolValue: trust.schoolOtherOccupationCosts ?? 0,
                  centralValue: trust.centralOtherOccupationCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const premisesStaffBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.premisesStaffCosts ?? 0,
                  schoolValue: trust.schoolPremisesStaffCosts ?? 0,
                  centralValue: trust.centralPremisesStaffCosts ?? 0,
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
          data: totalPremisesStaffServiceCostsBarData,
          title: "Total premises staff and service costs",
        },
        {
          data: cleaningCaretakingBarData,
          title: "Cleaning and caretaking costs",
        },
        { data: maintenanceBarData, title: "Maintenance of premises costs" },
        { data: otherOccupationBarData, title: "Other occupation costs" },
        { data: premisesStaffBarData, title: "Premises staff costs" },
      ]}
      dimension={dimension}
      handleDimensionChange={handleDimensionChange}
      hasNoData={data?.length === 0}
      index={5}
      title="Premises staff and services"
      trust
    />
  );
};
