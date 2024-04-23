import React, { useCallback, useEffect, useMemo, useState } from "react";
import {
  ChartDimensions,
  HeadcountPerFTE,
  PercentageOfWorkforce,
  PupilsPerStaffRole,
  WorkforceCategories,
} from "src/components";
import { ChartDimensionContext } from "src/contexts";
import { HeadcountData } from "src/views/compare-your-workforce/partials";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { WarningBanner } from "src/components/warning-banner";
import { Workforce, WorkforceApi } from "src/services";

export const Headcount: React.FC<{ type: string; id: string }> = ({
  type,
  id,
}) => {
  const [dimension, setDimension] = useState(PupilsPerStaffRole);
  const [data, setData] = useState(new Array<Workforce>());
  const getData = useCallback(async () => {
    setData(new Array<Workforce>());
    return await WorkforceApi.query(
      type,
      id,
      dimension.value,
      "workforce-headcount"
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
      WorkforceCategories.find((x) => x.value === event.target.value) ??
      PupilsPerStaffRole;
    setDimension(dimension);
  };

  const hasIncompleteData = data.some((x) => x.hasIncompleteData);

  return (
    <ChartDimensionContext.Provider value={dimension}>
      <HorizontalBarChartWrapper
        data={chartData}
        chartName="school workforce (headcount)"
      >
        <h2 className="govuk-heading-m">School workforce (Headcount)</h2>
        <WarningBanner
          isRendered={hasIncompleteData}
          icon="!"
          visuallyHiddenText="Warning"
          message="Some schools don't have a complete set of financial data for this period"
        />
        <ChartDimensions
          dimensions={WorkforceCategories.filter(
            (category) =>
              category !== HeadcountPerFTE && category !== PercentageOfWorkforce
          )}
          handleChange={handleSelectChange}
          elementId="headcount"
          defaultValue={dimension.value}
        />
      </HorizontalBarChartWrapper>
    </ChartDimensionContext.Provider>
  );
};
