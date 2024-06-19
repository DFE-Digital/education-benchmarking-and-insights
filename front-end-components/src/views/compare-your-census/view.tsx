import React, { useState } from "react";
import { CompareYourCensusViewProps } from "src/views";
import { ChartMode, ChartModeChart } from "src/components";
import {
  SelectedEstablishmentContext,
  PhaseContext,
  ChartModeProvider,
  useChartModeContext,
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
  const { chartMode, setChartMode } = useChartModeContext();
  const [phase, setPhase] = useState<string | undefined>(
    phases ? phases[0] : undefined
  );

  const handlePhaseChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setPhase(e.target.value);
  };

  return (
    <SelectedEstablishmentContext.Provider value={id}>
      <PhaseContext.Provider value={phase}>
        <ChartModeProvider initialValue={ChartModeChart}>
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
              <ChartMode chartMode={chartMode} handleChange={setChartMode} />
            </div>
          </div>
          <SchoolWorkforce id={id} type={type} />
          <TotalTeachers id={id} type={type} />
          <TotalTeachersQualified id={id} type={type} />
          <SeniorLeadership id={id} type={type} />
          <TeachingAssistants id={id} type={type} />
          <NonClassroomSupport id={id} type={type} />
          <AuxiliaryStaff id={id} type={type} />
          <Headcount id={id} type={type} />
        </ChartModeProvider>
      </PhaseContext.Provider>
    </SelectedEstablishmentContext.Provider>
  );
};
