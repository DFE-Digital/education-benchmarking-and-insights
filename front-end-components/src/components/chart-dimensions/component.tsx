import React from "react";
import { ChartDimensionsProps } from "src/components/chart-dimensions";
import { ChartModeChart } from "src/components";
import { useChartModeContext } from "src/contexts";

export const ChartDimensions: React.FC<ChartDimensionsProps> = (props) => {
  const { dimensions, elementId, handleChange, value } = props;
  const { chartMode } = useChartModeContext();

  return (
    <div className="govuk-form-group">
      <label className="govuk-label" htmlFor={`${elementId}-dimension`}>
        {chartMode == ChartModeChart ? "View graph as" : "View table as"}
      </label>
      <select
        className="govuk-select"
        name="dimension"
        id={`${elementId}-dimension`}
        onChange={handleChange}
        value={value}
      >
        {dimensions.map((dimension) => {
          return (
            <option key={dimension.value} value={dimension.value}>
              {dimension.label}
            </option>
          );
        })}
      </select>
    </div>
  );
};
