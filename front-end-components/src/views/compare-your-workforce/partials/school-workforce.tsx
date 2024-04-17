import React, { useCallback, useEffect, useMemo, useState } from "react";
import {
  ChartDimensions,
  PercentageOfWorkforce,
  PupilsPerStaffRole,
  WorkforceCategories,
} from "src/components";
import { ChartDimensionContext } from "src/contexts";
import { SchoolWorkforceData } from "src/views/compare-your-workforce/partials";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { WarningBanner } from "src/components/warning-banner";
import { Workforce, WorkforceApi } from "src/services";

export const SchoolWorkforce: React.FC<{ type: string; id: string }> = ({
  type,
  id,
}) => {
  const [dimension, setDimension] = useState(PupilsPerStaffRole);
  const [data, setData] = useState(new Array<Workforce>());
  const getData = useCallback(async () => {
    setData(new Array<Workforce>());
    return await WorkforceApi.query(type, id, dimension.value, "workforce-fte");
  }, [id, dimension, type]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const chartData: HorizontalBarChartWrapperData<SchoolWorkforceData> =
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
            value: school.workforceFte,
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
        chartName="school workforce (full time equivalent)"
      >
        <h2 className="govuk-heading-m">
          School workforce (Full Time Equivalent)
        </h2>
        {hasIncompleteData ? (
          <WarningBanner
            icon="!"
            visuallyHiddenText="Warning"
            message="Some schools don't have a complete set of financial data for this period"
          />
        ) : null}
        <ChartDimensions
          dimensions={WorkforceCategories.filter(
            (category) => category !== PercentageOfWorkforce
          )}
          handleChange={handleSelectChange}
          elementId="school-workforce"
          defaultValue={dimension.value}
        />
      </HorizontalBarChartWrapper>
    </ChartDimensionContext.Provider>
  );
};
