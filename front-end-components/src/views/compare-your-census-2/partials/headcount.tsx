import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  HeadcountPerFTE,
  PercentageOfWorkforce,
  PupilsPerStaffRole,
  CensusCategories,
} from "src/components";
import {
  PhaseContext,
  CustomDataContext,
  useProgressIndicatorsContext,
} from "src/contexts";
import { HeadcountData } from "src/views/compare-your-census-2/partials";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import { Census, CensusApi } from "src/services";
import { DimensionedChart } from "src/composed/dimensioned-chart";
import { useAbort } from "src/hooks/useAbort";

export const Headcount: React.FC<{ type: string; id: string }> = ({
  type,
  id,
}) => {
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [dimension, setDimension] = useState(PupilsPerStaffRole);
  const [censusData, setCensusData] = useState<Census[] | null>();
  const [data, setData] = useState<HeadcountData[] | null>();
  const { abort, signal } = useAbort();
  const getData = useCallback(async () => {
    setData(null);
    return await CensusApi.query(
      type,
      id,
      dimension.value,
      "WorkforceHeadcount",
      phase,
      customDataId,
      [signal]
    );
  }, [id, dimension, type, phase, customDataId, signal]);
  const { progressIndicators, renderChartLegend } =
    useProgressIndicatorsContext();

  useEffect(() => {
    getData().then((result) => {
      setCensusData(result);
    });
  }, [getData]);

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

  const chartData: HorizontalBarChartWrapperData<HeadcountData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.workforceHeadcount,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  useEffect(() => {
    const merged = censusData
      ? censusData.reduce<HeadcountData[]>(
          (acc: HeadcountData[], curr: Census) => {
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
  }, [censusData, progressIndicators]);

  const handleDimensionChange = (value: string) => {
    abort();

    const dimension =
      CensusCategories.find((x) => x.value === value) ?? PupilsPerStaffRole;
    setDimension(dimension);
  };

  return (
    <DimensionedChart
      charts={[{ data: chartData, title: "School workforce (Headcount)" }]}
      dimension={dimension}
      dimensions={CensusCategories.filter(
        (category) =>
          category !== HeadcountPerFTE && category !== PercentageOfWorkforce
      )}
      handleDimensionChange={handleDimensionChange}
      legend
      legendContent={renderChartLegend}
      legendHorizontalAlign="center"
      legendVerticalAlign="bottom"
      progressIndicators={progressIndicators}
      topLevel
    />
  );
};
