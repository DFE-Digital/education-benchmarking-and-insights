import React, { useState } from "react";
import { CompareYourCensusViewProps } from "src/views";
import { ChartMode, ChartModeChart } from "src/components";
import {
  SelectedSchoolContext,
  ChartModeContext,
  PhaseContext,
} from "src/contexts";
import {
  AuxiliaryStaff,
  Headcount,
  NonClassroomSupport,
  SchoolWorkforce,
  SeniorLeadership,
  TeachingAssistants,
  TotalTeachers,
  TotalTeachersQualified,
} from "src/views/compare-your-census/partials";

export const CompareYourCensus: React.FC<CompareYourCensusViewProps> = (
  props
) => {
  const { type, id, phases } = props;
  const [displayMode, setDisplayMode] = useState<string>(ChartModeChart);
  const [phase, setPhase] = useState<string | undefined>(
    phases ? phases[0] : undefined
  );

  const toggleChartMode = (e: React.ChangeEvent<HTMLInputElement>) => {
    setDisplayMode(e.target.value);
  };

  const handlePhaseChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setPhase(e.target.value);
  };

  return (
    <SelectedSchoolContext.Provider value={{ urn: id, name: "" }}>
      <PhaseContext.Provider value={phase}>
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
                  onChange={handlePhaseChange}
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
            <ChartMode
              displayMode={displayMode}
              handleChange={toggleChartMode}
            />
          </div>
        </div>
        <ChartModeContext.Provider value={displayMode}>
          <SchoolWorkforce id={id} type={type} />
          <TotalTeachers id={id} type={type} />
          <TotalTeachersQualified id={id} type={type} />
          <SeniorLeadership id={id} type={type} />
          <TeachingAssistants id={id} type={type} />
          <NonClassroomSupport id={id} type={type} />
          <AuxiliaryStaff id={id} type={type} />
          <Headcount id={id} type={type} />
        </ChartModeContext.Provider>
      </PhaseContext.Provider>
    </SelectedSchoolContext.Provider>
  );
};
