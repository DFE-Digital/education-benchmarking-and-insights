import React, { useCallback, useEffect, useMemo, useState } from "react";
import { AdministrativeSuppliesData } from "src/views/compare-your-trust/partials/accordion-sections/types";
import { CostCategories, PoundsPerPupil } from "src/components";
import { useCentralServicesBreakdownContext } from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import {
  ExpenditureApi,
  AdministrativeSuppliesTrustExpenditure,
} from "src/services";
import {
  BreakdownExclude,
  BreakdownInclude,
} from "src/components/central-services-breakdown";
import { AccordionSection } from "src/composed/accordion-section";

export const AdministrativeSupplies: React.FC<{
  id: string;
}> = ({ id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const { breakdown } = useCentralServicesBreakdownContext(true);
  const [data, setData] = useState<
    AdministrativeSuppliesTrustExpenditure[] | null
  >();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.trust<AdministrativeSuppliesTrustExpenditure>(
      id,
      dimension.value,
      "AdministrationSupplies",
      breakdown === BreakdownExclude
    );
  }, [id, dimension, breakdown]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const handleDimensionChange = (value: string) => {
    const dimension =
      CostCategories.find((x) => x.value === value) ?? PoundsPerPupil;
    setDimension(dimension);
  };

  const administrativeSuppliesBarData: HorizontalBarChartWrapperData<AdministrativeSuppliesData> =
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
                  totalValue:
                    trust.administrativeSuppliesNonEducationalCosts ?? 0,
                  schoolValue:
                    trust.schoolAdministrativeSuppliesNonEducationalCosts ?? 0,
                  centralValue:
                    trust.centralAdministrativeSuppliesNonEducationalCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, dimension, breakdown]);
  return (
    <AccordionSection
      charts={[
        {
          data: administrativeSuppliesBarData,
          title: "Administrative supplies (Non-educational)",
        },
      ]}
      dimension={dimension}
      handleDimensionChange={handleDimensionChange}
      hasNoData={data?.length === 0}
      index={7}
      showCopyImageButton
      title="Administrative supplies"
      trust
    />
  );
};
