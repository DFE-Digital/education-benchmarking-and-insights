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
  const [expenditureData, setExpenditureData] = useState<
    TotalExpenditureExpenditure[] | null
  >();
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
  const { progressIndicators, renderChartLegend } =
    useProgressIndicatorsContext();

  useEffect(() => {
    getData().then((result) => {
      setExpenditureData(result);
    });
  }, [getData]);

  useEffect(() => {
    const merged = expenditureData
      ? expenditureData.reduce<TotalExpenditureData[]>(
          (acc: TotalExpenditureData[], curr: TotalExpenditureExpenditure) => {
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

  useEffect(() => {
    if (onFetching) {
      onFetching(!data);
    }
  }, [data, onFetching]);

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

  const chartData: HorizontalBarChartWrapperData<TotalExpenditureData> =
    useMemo(() => {
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
    }, [data, tableHeadings]);

  const handleDimensionChange = (value: string) => {
    abort();

    const dimension =
      CostCategories.find((x) => x.value === value) ?? PoundsPerPupil;
    setDimension(dimension);
  };

  return (
    <DimensionedChart
      charts={[{ data: chartData, title: "Total expenditure" }]}
      costCodesUnderTitle
      dimension={dimension}
      dimensions={CostCategories.filter(function (category) {
        return category !== PercentageExpenditure;
      })}
      handleDimensionChange={handleDimensionChange}
      hasNoData={data?.length === 0}
      legend
      legendContent={renderChartLegend}
      legendHorizontalAlign="center"
      legendVerticalAlign="bottom"
      progressIndicators={progressIndicators}
      showCopyImageButton
      topLevel
      warningTag
    />
  );
};
