import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  ChartDimensions,
  HeadcountPerFTE,
  PercentageOfWorkforce,
  PupilsPerStaffRole,
  CensusCategories,
} from "src/components";
import {
  ChartDimensionContext,
  PhaseContext,
  CustomDataContext,
} from "src/contexts";
import { HeadcountData } from "src/views/compare-your-census/partials";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { Census, CensusApi } from "src/services";

export const Headcount: React.FC<{ type: string; id: string }> = ({
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
      "WorkforceHeadcount",
      phase,
      customDataId
    );
  }, [id, dimension, type, phase, customDataId]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const chartData: HorizontalBarChartWrapperData<HeadcountData> =
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
              value: school.workforceHeadcount,
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

  return (
    <ChartDimensionContext.Provider value={dimension}>
      <HorizontalBarChartWrapper
        data={chartData}
        chartName="school workforce (headcount)"
      >
        <h2 className="govuk-heading-m">School workforce (Headcount)</h2>
        <ChartDimensions
          dimensions={CensusCategories.filter(
            (category) =>
              category !== HeadcountPerFTE && category !== PercentageOfWorkforce
          )}
          handleChange={handleSelectChange}
          elementId="headcount"
          value={dimension.value}
        />
      </HorizontalBarChartWrapper>
    </ChartDimensionContext.Provider>
  );
};
