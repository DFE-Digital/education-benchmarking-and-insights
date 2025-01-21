import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  CompareYourCostsProps,
  UtilitiesData,
} from "src/views/compare-your-costs/partials/accordion-sections/types";
import { PoundsPerMetreSq, PremisesCategories } from "src/components";
import { PhaseContext, CustomDataContext } from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import { ExpenditureApi, UtilitiesExpenditure } from "src/services";
import { AccordionSection } from "src/composed/accordion-section";

export const Utilities: React.FC<CompareYourCostsProps> = ({ type, id }) => {
  const [dimension, setDimension] = useState(PoundsPerMetreSq);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [data, setData] = useState<UtilitiesExpenditure[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<UtilitiesExpenditure>(
      type,
      id,
      dimension.value,
      "Utilities",
      phase,
      customDataId
    );
  }, [id, dimension, type, phase, customDataId]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

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
    />
  );
};
