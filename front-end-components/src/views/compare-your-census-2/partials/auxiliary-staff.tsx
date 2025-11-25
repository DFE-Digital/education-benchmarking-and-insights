import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import { PupilsPerStaffRole, CensusCategories } from "src/components";
import {
  PhaseContext,
  CustomDataContext,
  useProgressIndicatorsContext,
} from "src/contexts";
import { AuxiliaryStaffData } from "src/views/compare-your-census-2/partials";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import { Census, CensusApi } from "src/services";
import { DimensionedChart } from "src/composed/dimensioned-chart";
import { useAbort } from "src/hooks/useAbort";

export const AuxiliaryStaff: React.FC<{ type: string; id: string }> = ({
  type,
  id,
}) => {
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [dimension, setDimension] = useState(PupilsPerStaffRole);
  const [censusData, setCensusData] = useState<Census[] | null>();
  const [data, setData] = useState<AuxiliaryStaffData[] | null>();
  const { abort, signal } = useAbort();
  const getData = useCallback(async () => {
    setData(null);
    return await CensusApi.query(
      type,
      id,
      dimension.value,
      "AuxiliaryStaffFte",
      phase,
      customDataId,
      [signal]
    );
  }, [type, id, dimension.value, phase, customDataId, signal]);
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

  const chartData: HorizontalBarChartWrapperData<AuxiliaryStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.auxiliaryStaff,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  useEffect(() => {
    const merged = censusData
      ? censusData.reduce<AuxiliaryStaffData[]>(
          (acc: AuxiliaryStaffData[], curr: Census) => {
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
      charts={[
        { data: chartData, title: "Auxiliary staff (Full Time Equivalent)" },
      ]}
      dimension={dimension}
      dimensions={CensusCategories}
      handleDimensionChange={handleDimensionChange}
      legend
      legendContent={renderChartLegend}
      legendHorizontalAlign="center"
      legendVerticalAlign="bottom"
      progressIndicators={progressIndicators}
      topLevel
      warningTag
    />
  );
};
