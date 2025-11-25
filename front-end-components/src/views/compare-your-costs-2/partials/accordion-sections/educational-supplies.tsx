import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  CompareYourCosts2Props,
  EducationalSuppliesData,
} from "src/views/compare-your-costs-2/partials/accordion-sections/types";
import { CostCategories, PoundsPerPupil } from "src/components";
import {
  CustomDataContext,
  PhaseContext,
  useProgressIndicatorsContext,
} from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import { ExpenditureApi, EducationalSuppliesExpenditure } from "src/services";
import { AccordionSection } from "src/composed/accordion-section";
import { useAbort } from "src/hooks/useAbort";

export const EducationalSupplies: React.FC<CompareYourCosts2Props> = ({
  type,
  id,
}) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [expenditureData, setExpenditureData] = useState<
    EducationalSuppliesExpenditure[] | null
  >();
  const [data, setData] = useState<EducationalSuppliesData[] | null>();
  const { abort, signal } = useAbort();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<EducationalSuppliesExpenditure>(
      type,
      id,
      dimension.value,
      "EducationalSupplies",
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
      ? expenditureData.reduce<EducationalSuppliesData[]>(
          (
            acc: EducationalSuppliesData[],
            curr: EducationalSuppliesExpenditure
          ) => {
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

  const handleDimensionChange = (value: string) => {
    abort();

    const dimension =
      CostCategories.find((x) => x.value === value) ?? PoundsPerPupil;
    setDimension(dimension);
  };

  const totalEducationalSuppliesBarData: HorizontalBarChartWrapperData<EducationalSuppliesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.totalEducationalSuppliesCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const examinationFeesBarData: HorizontalBarChartWrapperData<EducationalSuppliesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.examinationFeesCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const learningResourcesBarData: HorizontalBarChartWrapperData<EducationalSuppliesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.learningResourcesNonIctCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  return (
    <AccordionSection
      charts={[
        {
          data: totalEducationalSuppliesBarData,
          title: "Total educational supplies costs",
        },
        {
          data: examinationFeesBarData,
          title: "Examination fees costs",
        },
        {
          data: learningResourcesBarData,
          title: "Learning resources (not ICT equipment) costs",
        },
      ]}
      dimension={dimension}
      handleDimensionChange={handleDimensionChange}
      hasNoData={data?.length === 0}
      index={3}
      legend
      legendContent={renderChartLegend}
      legendHorizontalAlign="center"
      legendVerticalAlign="bottom"
      progressIndicators={progressIndicators}
      showCopyImageButton
      title="Educational supplies"
      warningTag
    />
  );
};
