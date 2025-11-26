import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  CompareYourCosts2Props,
  EducationalIctData,
} from "src/views/compare-your-costs-2/partials/accordion-sections/types";
import { CostCategories, PoundsPerPupil } from "src/components";
import {
  PhaseContext,
  CustomDataContext,
  useProgressIndicatorsContext,
} from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import { ExpenditureApi, EducationalIctExpenditure } from "src/services";
import { AccordionSection } from "src/composed/accordion-section";
import { useAbort } from "src/hooks/useAbort";

export const EducationalIct: React.FC<CompareYourCosts2Props> = ({
  type,
  id,
}) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [expenditureData, setExpenditureData] = useState<
    EducationalIctExpenditure[] | null
  >();
  const [data, setData] = useState<EducationalIctData[] | null>();
  const { abort, signal } = useAbort();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<EducationalIctExpenditure>(
      type,
      id,
      dimension.value,
      "EducationalIct",
      phase,
      customDataId,
      [signal]
    );
  }, [type, id, dimension.value, phase, customDataId, signal]);
  const { progressIndicators, renderChartLegend } =
    useProgressIndicatorsContext();

  useEffect(() => {
    getData().then((result) => {
      setExpenditureData(result);
    });
  }, [getData]);

  useEffect(() => {
    const merged = expenditureData
      ? expenditureData.reduce<EducationalIctData[]>(
          (acc: EducationalIctData[], curr: EducationalIctExpenditure) => {
            acc.push({
              ...curr,
              progressBanding: progressIndicators[curr.urn],
            });
            return acc;
          },
          []
        )
      : null;

    setData(merged);
  }, [expenditureData, progressIndicators]);

  const handleDimensionChange = (value: string) => {
    abort();

    const dimension =
      CostCategories.find((x) => x.value === value) ?? PoundsPerPupil;
    setDimension(dimension);
  };

  const tableHeadings = useMemo(() => {
    const headings = [
      "School name",
      "Local Authority",
      "School type",
      "Number of pupils",
      dimension.heading,
    ];

    if (Object.keys(progressIndicators).length > 0) {
      headings.push("Progress 8 banding");
    }

    return headings;
  }, [dimension, progressIndicators]);

  const learningResourcesBarData: HorizontalBarChartWrapperData<EducationalIctData> =
    useMemo(() => {
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
    }, [data, tableHeadings]);

  return (
    <AccordionSection
      charts={[
        {
          data: learningResourcesBarData,
          title: "Educational learning resources costs",
        },
      ]}
      costCodesUnderTitle
      dimension={dimension}
      handleDimensionChange={handleDimensionChange}
      hasNoData={data?.length === 0}
      index={4}
      legend
      legendContent={renderChartLegend}
      legendHorizontalAlign="center"
      legendVerticalAlign="bottom"
      progressIndicators={progressIndicators}
      showCopyImageButton
      title="Educational ICT"
      warningTag
    />
  );
};
