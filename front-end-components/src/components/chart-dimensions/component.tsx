import { ChartDimensionsProps } from "src/components/chart-dimensions";
import React, { useContext } from "react";
import { ChartModeContext } from "src/contexts";
import { ChartModeChart } from "src/components";

export const ChartDimensions: React.FC<ChartDimensionsProps> = (props) => {
  const { dimensions, elementId, handleChange, defaultValue } = props;
  const mode = useContext(ChartModeContext);

  return (
    <div className="govuk-form-group">
      <label className="govuk-label" htmlFor={`${elementId}-dimension`}>
        {mode == ChartModeChart ? "View graph as" : "View table as"}
      </label>
      <select
        className="govuk-select"
        name="dimension"
        id={`${elementId}-dimension`}
        onChange={handleChange}
        defaultValue={defaultValue}
      >
        {dimensions.map((dimension, idx) => {
          return (
            <option key={idx} value={dimension}>
              {dimension}
            </option>
          );
        })}
      </select>
    </div>
  );
};
