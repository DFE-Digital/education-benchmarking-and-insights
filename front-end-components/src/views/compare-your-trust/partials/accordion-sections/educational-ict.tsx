import React, { useCallback, useEffect, useMemo, useState } from "react";
import { EducationalIctData } from "src/views/compare-your-trust/partials/accordion-sections/types";
import { CostCategories, PoundsPerPupil } from "src/components";
import { useCentralServicesBreakdownContext } from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import { ExpenditureApi, EducationalIctTrustExpenditure } from "src/services";
import {
  BreakdownExclude,
  BreakdownInclude,
} from "src/components/central-services-breakdown";
import { AccordionSection } from "src/composed/accordion-section";
import { useAbort } from "src/hooks/useAbort";

export const EducationalIct: React.FC<{
  id: string;
}> = ({ id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const { breakdown } = useCentralServicesBreakdownContext(true);
  const [data, setData] = useState<EducationalIctTrustExpenditure[] | null>();
  const { abort, signal } = useAbort();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.trust<EducationalIctTrustExpenditure>(
      id,
      dimension.value,
      "EducationalIct",
      breakdown === BreakdownExclude,
      [signal]
    );
  }, [id, dimension.value, breakdown, signal]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const handleDimensionChange = (value: string) => {
    abort();

    const dimension =
      CostCategories.find((x) => x.value === value) ?? PoundsPerPupil;
    setDimension(dimension);
  };

  const learningResourcesBarData: HorizontalBarChartWrapperData<EducationalIctData> =
    useMemo(() => {
      const tableHeadings = ["Trust name", `Total ${dimension.heading}`];
      if (breakdown === BreakdownInclude) {
        tableHeadings.push(
          `School ${dimension.heading}`,
          `Central ${dimension.heading}`
        );
      }

      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.learningResourcesIctCosts ?? 0,
                  schoolValue: trust.schoolLearningResourcesIctCosts ?? 0,
                  centralValue: trust.centralLearningResourcesIctCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [dimension, data, breakdown]);

  return (
    <AccordionSection
      charts={[
        {
          data: learningResourcesBarData,
          title: "Educational learning resources costs",
        },
      ]}
      dimension={dimension}
      handleDimensionChange={handleDimensionChange}
      hasNoData={data?.length === 0}
      index={4}
      showCopyImageButton
      title="Educational ICT"
      trust
    />
  );
};
