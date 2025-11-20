import React from "react";
import { ChartPhasesProps } from "./types";
import "./styles.scss";

export const ChartPhases = ({
  phases,
  handlePhaseChange,
}: ChartPhasesProps) => {
  const handleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    handlePhaseChange(e.target.value);
  };

  if (!phases) {
    return null;
  }

  return (
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
  );
};
