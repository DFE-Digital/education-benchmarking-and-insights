import { useMemo } from "react";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { ChartDimensionContext } from "src/contexts";
import {
  TotalTeachersQualifiedData,
  TotalTeachersQualifiedProps,
} from "src/views/compare-your-workforce/partials";

export const TotalTeachersQualified: React.FC<TotalTeachersQualifiedProps> = (
  props
) => {
  const { schools } = props;

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
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: school.teachersWithQTSFTE,
          };
        }),
        tableHeadings,
      };
    }, [schools]);

  return (
    <ChartDimensionContext.Provider value={"percent"}>
      <HorizontalBarChartWrapper
        data={chartData}
        chartName="teachers with qualified teacher status (%)"
      >
        <h2 className="govuk-heading-m">
          Teachers with qualified Teacher Status (%)
        </h2>
      </HorizontalBarChartWrapper>
    </ChartDimensionContext.Provider>
  );
};
