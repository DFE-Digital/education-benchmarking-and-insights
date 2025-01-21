import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  AdministrativeSuppliesData,
  CompareYourCostsProps,
} from "src/views/compare-your-costs/partials/accordion-sections/types";
import { CostCategories, PoundsPerPupil } from "src/components";
import { PhaseContext, CustomDataContext } from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import {
  ExpenditureApi,
  AdministrativeSuppliesExpenditure,
} from "src/services";
import { AccordionSection } from "src/composed/accordion-section";

export const AdministrativeSupplies: React.FC<CompareYourCostsProps> = ({
  type,
  id,
}) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [data, setData] = useState<
    AdministrativeSuppliesExpenditure[] | null
  >();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<AdministrativeSuppliesExpenditure>(
      type,
      id,
      dimension.value,
      "AdministrationSupplies",
      phase,
      customDataId
    );
  }, [id, dimension, type, phase, customDataId]);

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
      const tableHeadings = [
        "School name",
        "Local Authority",
        "School type",
        "Number of pupils",
        dimension.heading,
      ];

      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.administrativeSuppliesNonEducationalCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, dimension]);

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
      title="Administrative supplies"
    />
  );
};
