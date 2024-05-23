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
} from "src/contexts";
import { TotalTeachersData } from "src/views/compare-your-census/partials";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { Census, CensusApi } from "src/services";

export const TotalTeachers: React.FC<{ type: string; id: string }> = ({
  type,
  id,
}) => {
  const phase = useContext(PhaseContext);
  const [dimension, setDimension] = useState(PupilsPerStaffRole);
  const [data, setData] = useState<Census[]>();
  const getData = useCallback(async () => {
    setData(new Array<Census>());
    return await CensusApi.query(
      type,
      id,
      dimension.value,
      "TeachersFte",
      phase
    );
  }, [id, dimension, type, phase]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const chartData: HorizontalBarChartWrapperData<TotalTeachersData> =
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
              value: school.teachersFte,
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

  const hasIncompleteData = data?.some((x) => x.hasIncompleteData);
  const hasNoData = data?.length === 0;

  return (
    <HasIncompleteDataContext.Provider value={{ hasIncompleteData, hasNoData }}>
      <ChartDimensionContext.Provider value={dimension}>
        <HorizontalBarChartWrapper
          data={chartData}
          chartName="total number of teachers (full time equivalent)"
        >
          <h2 className="govuk-heading-m">
            Total number of teachers (Full Time Equivalent)
          </h2>
          <ChartDimensions
            dimensions={CensusCategories}
            handleChange={handleSelectChange}
            elementId="total-teachers"
            defaultValue={dimension.value}
          />
        </HorizontalBarChartWrapper>
      </ChartDimensionContext.Provider>
    </HasIncompleteDataContext.Provider>
  );
};
