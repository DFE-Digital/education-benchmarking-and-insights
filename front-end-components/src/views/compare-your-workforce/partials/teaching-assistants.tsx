import React, { useMemo, useState } from "react";
import {
  CalculateWorkforceValue,
  ChartDimensions,
  DimensionHeading,
  PupilsPerStaffRole,
  WorkforceCategories,
} from "src/components";
import { ChartDimensionContext } from "src/contexts";
import {
  TeachingAssistantsData,
  TeachingAssistantsProps,
} from "src/views/compare-your-workforce/partials";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";

export const TeachingAssistants: React.FC<TeachingAssistantsProps> = (
  props
) => {
  const { schools } = props;
  const [dimension, setDimension] = useState(PupilsPerStaffRole.value);

  const chartData: HorizontalBarChartWrapperData<TeachingAssistantsData> =
    useMemo(() => {
      const tableHeadings = [
        "School name",
        "Local Authority",
        "School type",
        "Number of pupils",
        DimensionHeading(dimension),
      ];

      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateWorkforceValue({
              dimension,
              value: school.teachingAssistantsFTE,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools]);

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    setDimension(event.target.value);
  };

  return (
    <ChartDimensionContext.Provider value={dimension}>
      <HorizontalBarChartWrapper
        data={chartData}
        chartName="teaching assistants (full time equivalent)"
      >
        <h2 className="govuk-heading-m">
          Teaching Assistants (Full Time Equivalent)
        </h2>
        <ChartDimensions
          dimensions={WorkforceCategories}
          handleChange={handleSelectChange}
          elementId="teaching-assistants"
          defaultValue={dimension}
        />
      </HorizontalBarChartWrapper>
    </ChartDimensionContext.Provider>
  );
};
