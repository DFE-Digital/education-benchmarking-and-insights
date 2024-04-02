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
  AuxiliaryStaffData,
  AuxiliaryStaffProps,
} from "src/views/compare-your-workforce/partials";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";

export const AuxiliaryStaff: React.FC<AuxiliaryStaffProps> = (props) => {
  const { schools } = props;
  const [dimension, setDimension] = useState(PupilsPerStaffRole);

  const chartData: HorizontalBarChartWrapperData<AuxiliaryStaffData> =
    useMemo(() => {
      const tableHeadings = [
        "School name",
        "Local Authority",
        "School type",
        "Number of pupils",
        DimensionHeading(dimension.value),
      ];

      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateWorkforceValue({
              dimension: dimension.value,
              value: school.auxiliaryStaffFTE,
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
    const dimension =
      WorkforceCategories.find((x) => x.value === event.target.value) ??
      PupilsPerStaffRole;
    setDimension(dimension);
  };

  return (
    <ChartDimensionContext.Provider value={dimension}>
      <HorizontalBarChartWrapper
        data={chartData}
        chartName="auxiliary staff (full time equivalent)"
      >
        <h2 className="govuk-heading-m">
          Auxiliary staff (Full Time Equivalent)
        </h2>
        <ChartDimensions
          dimensions={WorkforceCategories}
          handleChange={handleSelectChange}
          elementId="auxiliary-staff"
          defaultValue={dimension.value}
        />
      </HorizontalBarChartWrapper>
    </ChartDimensionContext.Provider>
  );
};
