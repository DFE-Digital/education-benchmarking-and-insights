import React, { useCallback, useEffect, useMemo, useState } from "react";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { WarningBanner } from "src/components/warning-banner";
import { ChartDimensionContext } from "src/contexts";
import { TotalTeachersQualifiedData } from "src/views/compare-your-workforce/partials";
import { Percent } from "src/components";
import { Workforce, WorkforceApi } from "src/services";

export const TotalTeachersQualified: React.FC<{ type: string; id: string }> = ({
  type,
  id,
}) => {
  const [data, setData] = useState(new Array<Workforce>());
  const getData = useCallback(async () => {
    setData(new Array<Workforce>());
    return await WorkforceApi.query(type, id, "Percent", "teachers-qualified");
  }, [id, type]);

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
    <ChartDimensionContext.Provider value={Percent}>
      <HorizontalBarChartWrapper
        data={chartData}
        chartName="teachers with qualified teacher status (%)"
      >
        <h2 className="govuk-heading-m">
          Teachers with qualified teacher status (%)
        </h2>
        <WarningBanner
          isRendered={hasIncompleteData}
          icon="!"
          visuallyHiddenText="Warning"
          message="Some schools don't have a complete set of financial data for this period"
        />
      </HorizontalBarChartWrapper>
    </ChartDimensionContext.Provider>
  );
};
