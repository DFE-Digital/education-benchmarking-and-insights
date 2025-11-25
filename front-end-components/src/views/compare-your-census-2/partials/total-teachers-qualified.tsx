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
  useProgressIndicatorsContext,
} from "src/contexts";
import { TotalTeachersQualifiedData } from "src/views/compare-your-census-2/partials";
import { Percent, ProgressBanding } from "src/components";
import { Census, CensusApi } from "src/services";

export const TotalTeachersQualified: React.FC<{ type: string; id: string }> = ({
  type,
  id,
}) => {
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [censusData, setCensusData] = useState<Census[] | null>();
  const [data, setData] = useState<TotalTeachersQualifiedData[] | null>();
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
      "Percent",
    ];

    if (Object.keys(progressIndicators).length > 0) {
      headings.push("Progress 8 banding");
    }

    return headings;
  }, [progressIndicators]);

  const chartData: HorizontalBarChartWrapperData<TotalTeachersQualifiedData> =
    useMemo(() => {
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
    }, [data, tableHeadings]);

  useEffect(() => {
    const merged = censusData
      ? censusData.reduce<TotalTeachersQualifiedData[]>(
          (acc: TotalTeachersQualifiedData[], curr: Census) => {
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

  const title = "Teachers with qualified teacher status (percentage)";

  return (
    <ChartDimensionContext.Provider value={Percent}>
      <HorizontalBarChartWrapper
        chartTitle={title}
        data={chartData}
        legend
        legendContent={renderChartLegend}
        legendHorizontalAlign="center"
        legendVerticalAlign="bottom"
        linkToEstablishment
        progressAboveAverageKeys={
          progressIndicators
            ? Object.entries(progressIndicators)
                .filter((e) => e[1] === ProgressBanding.AboveAverage)
                .map((e) => e[0])
            : undefined
        }
        progressWellAboveAverageKeys={
          progressIndicators
            ? Object.entries(progressIndicators)
                .filter((e) => e[1] === ProgressBanding.WellAboveAverage)
                .map((e) => e[0])
            : undefined
        }
        tooltip
        warningTag
      >
        <h2 className="govuk-heading-m">{title}</h2>
      </HorizontalBarChartWrapper>
    </ChartDimensionContext.Provider>
  );
};
