import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import {
  ChartDimensionContext,
  PhaseContext,
  CustomDataContext,
} from "src/contexts";
import { TotalTeachersQualifiedData } from "src/views/compare-your-census/partials";
import { Percent } from "src/components";
import { Census, CensusApi } from "src/services";

export const TotalTeachersQualified: React.FC<{ type: string; id: string }> = ({
  type,
  id,
}) => {
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [data, setData] = useState<Census[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await CensusApi.query(
      type,
      id,
      "Total",
      "TeachersQualified",
      phase,
      customDataId
    );
  }, [id, type, phase, customDataId]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const chartData: HorizontalBarChartWrapperData<TotalTeachersQualifiedData> =
    useMemo(() => {
      const tableHeadings = [
        "School name",
        "Local Authority",
        "School type",
        "Number of pupils",
        "Percent",
      ];

      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.percentTeacherWithQualifiedStatus,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data]);

  const title = "Teachers with qualified teacher status (percentage)";

  return (
    <ChartDimensionContext.Provider value={Percent}>
      <HorizontalBarChartWrapper data={chartData} chartTitle={title}>
        <h2 className="govuk-heading-m">{title}</h2>
      </HorizontalBarChartWrapper>
    </ChartDimensionContext.Provider>
  );
};
