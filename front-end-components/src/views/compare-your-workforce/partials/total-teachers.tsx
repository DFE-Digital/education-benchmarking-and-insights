import React, { useCallback, useEffect, useMemo, useState } from "react";
import {
  ChartDimensions,
  PupilsPerStaffRole,
  WorkforceCategories,
} from "src/components";
import { ChartDimensionContext } from "src/contexts";
import { TotalTeachersData } from "src/views/compare-your-workforce/partials";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { Workforce, WorkforceApi } from "src/services";

export const TotalTeachers: React.FC<{ type: string; id: string }> = ({
  type,
  id,
}) => {
  const [dimension, setDimension] = useState(PupilsPerStaffRole);
  const [data, setData] = useState(new Array<Workforce>());
  const getData = useCallback(async () => {
    setData(new Array<Workforce>());
    return await WorkforceApi.query(type, id, dimension.value, "teachers-fte");
  }, [id, dimension, type]);

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
        dataPoints: data.map((school) => {
          return {
            ...school,
            value: school.teachersFte,
          };
        }),
        tableHeadings,
      };
    }, [dimension, data]);

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    const dimension =
      WorkforceCategories.find((x) => x.value === event.target.value) ??
      PupilsPerStaffRole;
    setDimension(dimension);
  };

  return (
    <ChartDimensionContext.Provider value={dimension}>
      <HorizontalBarChartWrapper
        data={chartData}
        chartName="total number of teachers (full time equivalent)"
      >
        <h2 className="govuk-heading-m">
          Total number of teachers (Full Time Equivalent)
        </h2>
        <ChartDimensions
          dimensions={WorkforceCategories}
          handleChange={handleSelectChange}
          elementId="total-teachers"
          defaultValue={dimension.value}
        />
      </HorizontalBarChartWrapper>
    </ChartDimensionContext.Provider>
  );
};
