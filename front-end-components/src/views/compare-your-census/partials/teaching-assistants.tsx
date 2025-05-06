import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import { PupilsPerStaffRole, CensusCategories } from "src/components";
import { PhaseContext, CustomDataContext } from "src/contexts";
import { TeachingAssistantsData } from "src/views/compare-your-census/partials";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import { Census, CensusApi } from "src/services";
import { DimensionedChart } from "src/composed/dimensioned-chart";
import { useAbort } from "src/hooks/useAbort";

export const TeachingAssistants: React.FC<{ type: string; id: string }> = ({
  type,
  id,
}) => {
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [dimension, setDimension] = useState(PupilsPerStaffRole);
  const [data, setData] = useState<Census[] | null>();
  const { abort, signal } = useAbort();
  const getData = useCallback(async () => {
    setData(null);
    return await CensusApi.query(
      type,
      id,
      dimension.value,
      "TeachingAssistantsFte",
      phase,
      customDataId,
      [signal]
    );
  }, [id, dimension, type, phase, customDataId, signal]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const chartData: HorizontalBarChartWrapperData<TeachingAssistantsData> =
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
              value: school.teachingAssistant,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [dimension, data]);

  const handleDimensionChange = (value: string) => {
    abort();

    const dimension =
      CensusCategories.find((x) => x.value === value) ?? PupilsPerStaffRole;
    setDimension(dimension);
  };

  return (
    <DimensionedChart
      charts={[
        {
          data: chartData,
          title: "Teaching Assistants (Full Time Equivalent)",
        },
      ]}
      dimension={dimension}
      dimensions={CensusCategories}
      handleDimensionChange={handleDimensionChange}
      topLevel
    />
  );
};
