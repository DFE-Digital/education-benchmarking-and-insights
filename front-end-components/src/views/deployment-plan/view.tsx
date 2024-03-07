import React, { useRef } from "react";
import { ChartHandler } from "src/components";
import { VerticalBarChart } from "src/components/charts/vertical-bar-chart";
import { DeploymentPlanViewProps } from "src/views/deployment-plan/types.tsx";

export const DeploymentPlan: React.FC<DeploymentPlanViewProps> = (props) => {
  const { data } = props;
  const verticalChart2SeriesRef = useRef<ChartHandler>(null);

  return (
    <div style={{ height: 500 }}>
      <VerticalBarChart
        chartName="Percentage of pupils on roll and teacher cost"
        data={data}
        grid
        keyField="id"
        legend
        margin={20}
        multiLineAxisLabel
        ref={verticalChart2SeriesRef}
        seriesConfig={{
          pupilsOnRoll: {
            label: "Pupils on roll",
            visible: true,
          },
          teacherCost: {
            label: "Teacher cost",
            visible: true,
          },
        }}
        seriesLabelField="group"
        valueUnit="%"
      />
    </div>
  );
};
