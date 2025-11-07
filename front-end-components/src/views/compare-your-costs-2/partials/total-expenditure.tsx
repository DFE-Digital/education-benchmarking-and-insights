import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import { TotalExpenditureData } from "src/views/compare-your-costs-2/partials";
import {
  CustomDataContext,
  PhaseContext,
  useProgressIndicatorsContext,
} from "src/contexts";
import {
  CostCategories,
  PoundsPerPupil,
  PercentageExpenditure,
} from "src/components";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import { ExpenditureApi, TotalExpenditureExpenditure } from "src/services";
import { CompareYourCosts2Props } from "./accordion-sections/types";
import { DimensionedChart } from "src/composed/dimensioned-chart";
import { useAbort } from "src/hooks/useAbort";

export const TotalExpenditure: React.FC<CompareYourCosts2Props> = ({
  id,
  onFetching,
  type,
}) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [data, setData] = useState<TotalExpenditureData[] | null>();
  const { abort, signal } = useAbort();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<TotalExpenditureExpenditure>(
      type,
      id,
      dimension.value,
      "TotalExpenditure",
      phase,
      customDataId,
      [signal]
    );
  }, [type, id, dimension.value, phase, customDataId, signal]);
  const { data: progressIndicators } = useProgressIndicatorsContext();

  useEffect(() => {
    getData().then((result) => {
      const merged = result
        ? result.reduce<TotalExpenditureData[]>(
            (
              acc: TotalExpenditureData[],
              curr: TotalExpenditureExpenditure
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
    });
  }, [getData, progressIndicators]);

  useEffect(() => {
    if (onFetching) {
      onFetching(!data);
    }
  }, [data, onFetching]);

  const chartData: HorizontalBarChartWrapperData<TotalExpenditureData> =
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
              value: school.totalExpenditure,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [dimension, data]);

  const handleDimensionChange = (value: string) => {
    abort();

    const dimension =
      CostCategories.find((x) => x.value === value) ?? PoundsPerPupil;
    setDimension(dimension);
  };

  return (
    <DimensionedChart
      charts={[{ data: chartData, title: "Total expenditure" }]}
      dimension={dimension}
      dimensions={CostCategories.filter(function (category) {
        return category !== PercentageExpenditure;
      })}
      handleDimensionChange={handleDimensionChange}
      hasNoData={data?.length === 0}
      showCopyImageButton
      topLevel
    />
  );
};
