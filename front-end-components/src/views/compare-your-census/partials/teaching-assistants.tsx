import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  ChartDimensions,
  PupilsPerStaffRole,
  CensusCategories,
} from "src/components";
import {
  ChartDimensionContext,
  HasIncompleteDataContext,
  PhaseContext,
  CustomDataContext,
} from "src/contexts";
import { TeachingAssistantsData } from "src/views/compare-your-census/partials";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { Census, CensusApi } from "src/services";

export const TeachingAssistants: React.FC<{ type: string; id: string }> = ({
  type,
  id,
}) => {
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [dimension, setDimension] = useState(PupilsPerStaffRole);
  const [data, setData] = useState<Census[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await CensusApi.query(
      type,
      id,
      dimension.value,
      "TeachingAssistantsFte",
      phase,
      customDataId
    );
  }, [id, dimension, type, phase, customDataId]);

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

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    const dimension =
      CensusCategories.find((x) => x.value === event.target.value) ??
      PupilsPerStaffRole;
    setDimension(dimension);
  };

  const hasNoData = data?.length === 0;

  return (
    <HasIncompleteDataContext.Provider value={{ hasNoData }}>
      <ChartDimensionContext.Provider value={dimension}>
        <HorizontalBarChartWrapper
          data={chartData}
          chartName="teaching assistants (full time equivalent)"
        >
          <h2 className="govuk-heading-m">
            Teaching Assistants (Full Time Equivalent)
          </h2>
          <ChartDimensions
            dimensions={CensusCategories}
            handleChange={handleSelectChange}
            elementId="teaching-assistants"
            value={dimension.value}
          />
        </HorizontalBarChartWrapper>
      </ChartDimensionContext.Provider>
    </HasIncompleteDataContext.Provider>
  );
};
