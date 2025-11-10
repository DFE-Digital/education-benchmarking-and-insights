import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  CompareYourCosts2Props,
  UtilitiesData,
} from "src/views/compare-your-costs-2/partials/accordion-sections/types";
import { PoundsPerMetreSq, PremisesCategories } from "src/components";
import {
  PhaseContext,
  CustomDataContext,
  useProgressIndicatorsContext,
} from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import { ExpenditureApi, UtilitiesExpenditure } from "src/services";
import { AccordionSection } from "src/composed/accordion-section";
import { useAbort } from "src/hooks/useAbort";

export const Utilities: React.FC<CompareYourCosts2Props> = ({ type, id }) => {
  const [dimension, setDimension] = useState(PoundsPerMetreSq);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [expenditureData, setExpenditureData] = useState<
    UtilitiesExpenditure[] | null
  >();
  const [data, setData] = useState<UtilitiesData[] | null>();
  const { abort, signal } = useAbort();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<UtilitiesExpenditure>(
      type,
      id,
      dimension.value,
      "Utilities",
      phase,
      customDataId,
      [signal]
    );
  }, [type, id, dimension.value, phase, customDataId, signal]);
  const { progressIndicators } = useProgressIndicatorsContext();

  useEffect(() => {
    getData().then((result) => {
      setExpenditureData(result);
    });
  }, [getData]);

  useEffect(() => {
    const merged = expenditureData
      ? expenditureData.reduce<UtilitiesData[]>(
          (acc: UtilitiesData[], curr: UtilitiesExpenditure) => {
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

  const tableHeadings = useMemo(
    () => [
      "School name",
      "Local Authority",
      "School type",
      "Number of pupils",
      dimension.heading,
    ],
    [dimension]
  );

  const handleDimensionChange = (value: string) => {
    abort();

    const dimension =
      PremisesCategories.find((x) => x.value === value) ?? PoundsPerMetreSq;
    setDimension(dimension);
  };

  const totalUtilitiesCostsBarData: HorizontalBarChartWrapperData<UtilitiesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.totalUtilitiesCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const energyBarData: HorizontalBarChartWrapperData<UtilitiesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.energyCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const waterSewerageBarData: HorizontalBarChartWrapperData<UtilitiesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.waterSewerageCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  return (
    <AccordionSection
      charts={[
        {
          data: totalUtilitiesCostsBarData,
          title: "Total utilities costs",
          dimensions: PremisesCategories,
        },
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
      progressIndicators={progressIndicators}
      showCopyImageButton
      title="Utilities"
    />
  );
};
