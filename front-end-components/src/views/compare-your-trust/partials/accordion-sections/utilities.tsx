import React, { useCallback, useEffect, useMemo, useState } from "react";
import { UtilitiesData } from "src/views/compare-your-trust/partials/accordion-sections/types";
import { PoundsPerMetreSq, PremisesCategories } from "src/components";
import { useCentralServicesBreakdownContext } from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import { ExpenditureApi, UtilitiesTrustExpenditure } from "src/services";
import {
  BreakdownExclude,
  BreakdownInclude,
} from "src/components/central-services-breakdown";
import { AccordionSection } from "src/composed/accordion-section";

export const Utilities: React.FC<{
  id: string;
}> = ({ id }) => {
  const [dimension, setDimension] = useState(PoundsPerMetreSq);
  const { breakdown } = useCentralServicesBreakdownContext(true);
  const [data, setData] = useState<UtilitiesTrustExpenditure[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.trust<UtilitiesTrustExpenditure>(
      id,
      dimension.value,
      "Utilities",
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

  const totalUtilitiesCostsBarData: HorizontalBarChartWrapperData<UtilitiesData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.totalUtilitiesCosts ?? 0,
                  schoolValue: trust.schoolTotalUtilitiesCosts ?? 0,
                  centralValue: trust.centralTotalUtilitiesCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const energyBarData: HorizontalBarChartWrapperData<UtilitiesData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.energyCosts ?? 0,
                  schoolValue: trust.schoolEnergyCosts ?? 0,
                  centralValue: trust.centralEnergyCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const waterSewerageBarData: HorizontalBarChartWrapperData<UtilitiesData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.waterSewerageCosts ?? 0,
                  schoolValue: trust.schoolWaterSewerageCosts ?? 0,
                  centralValue: trust.centralWaterSewerageCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  return (
    <AccordionSection
      charts={[
        { data: totalUtilitiesCostsBarData, title: "Total utilities costs" },
        {
          data: energyBarData,
          title: "Energy costs",
        },
        {
          data: waterSewerageBarData,
          title: "Water and sewerage costs",
        },
      ]}
      dimension={dimension}
      handleDimensionChange={handleDimensionChange}
      hasNoData={data?.length === 0}
      index={6}
      title="Utilities"
      trust
    />
  );
};
