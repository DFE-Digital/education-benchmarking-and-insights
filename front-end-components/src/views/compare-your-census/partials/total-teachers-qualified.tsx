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
  HasIncompleteDataContext,
  PhaseContext,
} from "src/contexts";
import { TotalTeachersQualifiedData } from "src/views/compare-your-census/partials";
import { Percent } from "src/components";
import { Census, CensusApi } from "src/services";

export const TotalTeachersQualified: React.FC<{ type: string; id: string }> = ({
  type,
  id,
}) => {
  const phase = useContext(PhaseContext);
  const [data, setData] = useState(new Array<Census>());
  const getData = useCallback(async () => {
    setData(new Array<Census>());
    return await CensusApi.query(type, id, "Total", "TeachersQualified", phase);
  }, [id, type, phase]);

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
        dataPoints: data.map((school) => {
          return {
            ...school,
            value: school.teachersQualified,
          };
        }),
        tableHeadings,
      };
    }, [data]);

  const hasIncompleteData = data.some((x) => x.hasIncompleteData);

  return (
    <HasIncompleteDataContext.Provider value={hasIncompleteData}>
      <ChartDimensionContext.Provider value={Percent}>
        <HorizontalBarChartWrapper
          data={chartData}
          chartName="teachers with qualified teacher status (%)"
        >
          <h2 className="govuk-heading-m">
            Teachers with qualified teacher status (%)
          </h2>
        </HorizontalBarChartWrapper>
      </ChartDimensionContext.Provider>
    </HasIncompleteDataContext.Provider>
  );
};
