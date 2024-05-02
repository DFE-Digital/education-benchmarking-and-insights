import React, { useCallback, useEffect, useMemo, useState } from "react";
import {
  ChartDimensions,
  HeadcountPerFTE,
  PercentageOfWorkforce,
  PupilsPerStaffRole,
  CensusCategories,
} from "src/components";
import { ChartDimensionContext, HasIncompleteDataContext } from "src/contexts";
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
  const [dimension, setDimension] = useState(PupilsPerStaffRole);
  const [data, setData] = useState(new Array<Census>());
  const getData = useCallback(async () => {
    setData(new Array<Census>());
    return await CensusApi.query(
      type,
      id,
      dimension.value,
      "WorkforceHeadcount"
    );
  }, [id, dimension, type]);

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
        dataPoints: data.map((school) => {
          return {
            ...school,
            value: school.workforceHeadcount,
          };
        }),
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

  const hasIncompleteData = data.some((x) => x.hasIncompleteData);

  return (
    <HasIncompleteDataContext.Provider value={hasIncompleteData}>
      <ChartDimensionContext.Provider value={dimension}>
        <HorizontalBarChartWrapper
          data={chartData}
          chartName="school workforce (headcount)"
        >
          <h2 className="govuk-heading-m">School workforce (Headcount)</h2>
          <ChartDimensions
            dimensions={CensusCategories.filter(
              (category) =>
                category !== HeadcountPerFTE &&
                category !== PercentageOfWorkforce
            )}
            handleChange={handleSelectChange}
            elementId="headcount"
            defaultValue={dimension.value}
          />
        </HorizontalBarChartWrapper>
      </ChartDimensionContext.Provider>
    </HasIncompleteDataContext.Provider>
  );
};
