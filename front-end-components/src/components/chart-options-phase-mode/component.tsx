import React from "react";
import { ChartMode } from "src/components";
import { useChartModeContext } from "src/contexts";
import { ChartOptionsPhaseModeProps } from "./types";

export const ChartOptionsPhaseMode = ({
  phases,
  handlePhaseChange,
}: ChartOptionsPhaseModeProps) => {
  const { chartMode, setChartMode } = useChartModeContext();

  const handleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    handlePhaseChange(e.target.value);
  };

  return (
    <div className="chart-options">
      <div>
        {phases && (
          <div className="govuk-form-group">
            <label className="govuk-label govuk-label--s" htmlFor="phase">
              Phase
            </label>
            <select
              className="govuk-select"
              name="phase"
              id="phase"
              onChange={handleChange}
            >
              {phases.map((phase) => {
                return (
                  <option key={phase} value={phase}>
                    {phase}
                  </option>
                );
              })}
            </select>
          </div>
        )}
      </div>
      <div>
        <ChartMode chartMode={chartMode} handleChange={setChartMode} />
      </div>
    </div>
  );
};
