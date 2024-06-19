import React, { useState } from "react";
import { CompareYourCensusViewProps } from "src/views";
import { ChartModeChart } from "src/components";
import {
  SelectedEstablishmentContext,
  PhaseContext,
  ChartModeProvider,
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
import { ChartOptionsPhaseMode } from "src/components/chart-options-phase-mode";

export const CompareYourCensus: React.FC<CompareYourCensusViewProps> = (
  props
) => {
  const { type, id, phases } = props;
  const [phase, setPhase] = useState<string | undefined>(
    phases ? phases[0] : undefined
  );

  return (
    <SelectedEstablishmentContext.Provider value={id}>
      <PhaseContext.Provider value={phase}>
        <ChartModeProvider initialValue={ChartModeChart}>
          <ChartOptionsPhaseMode phases={phases} handlePhaseChange={setPhase} />
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
