import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  CompareYourCostsProps,
  EducationalIctData,
} from "src/views/compare-your-costs/partials/accordion-sections/types";
import { CostCategories, PoundsPerPupil } from "src/components";
import { PhaseContext, CustomDataContext } from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import { ExpenditureApi, EducationalIctExpenditure } from "src/services";
import { AccordionSection } from "src/composed/accordion-section";

export const EducationalIct: React.FC<CompareYourCostsProps> = ({
  type,
  id,
}) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [data, setData] = useState<EducationalIctExpenditure[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<EducationalIctExpenditure>(
      type,
      id,
      dimension.value,
      "EducationalIct",
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

  const learningResourcesBarData: HorizontalBarChartWrapperData<EducationalIctData> =
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
              value: school.learningResourcesIctCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [dimension, data]);

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
      title="Educational ICT"
    />
  );
};
