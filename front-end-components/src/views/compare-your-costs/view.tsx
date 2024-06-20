import React, { useState } from "react";
import {
  TotalExpenditure,
  ExpenditureAccordion,
} from "src/views/compare-your-costs/partials";
import { CompareYourCostsViewProps } from "src/views/compare-your-costs";
import { ChartModeChart } from "src/components";
import {
  SelectedEstablishmentContext,
  //HasIncompleteDataContext,
  PhaseContext,
  CustomDataContext,
  ChartModeProvider,
} from "src/contexts";
import { useGovUk } from "src/hooks/useGovUk";
import { ChartOptionsPhaseMode } from "src/components/chart-options-phase-mode";

export const CompareYourCosts: React.FC<CompareYourCostsViewProps> = (
  props
) => {
  const { type, id, phases, customDataId } = props;
  const [phase, setPhase] = useState<string | undefined>(
    phases ? phases[0] : undefined
  );

  useGovUk();

  return (
    <SelectedEstablishmentContext.Provider value={id}>
      <PhaseContext.Provider value={phase}>
        <CustomDataContext.Provider value={customDataId}>
          <ChartModeProvider initialValue={ChartModeChart}>
            <ChartOptionsPhaseMode
              phases={phases}
              handlePhaseChange={setPhase}
            />
            {/*<HasIncompleteDataContext.Provider*/}
            {/*  value={{ hasIncompleteData, hasNoData }}*/}
            {/*>*/}
            <TotalExpenditure id={id} type={type} />
            <ExpenditureAccordion id={id} type={type} />
            {/*</HasIncompleteDataContext.Provider>*/}
          </ChartModeProvider>
        </CustomDataContext.Provider>
      </PhaseContext.Provider>
    </SelectedEstablishmentContext.Provider>
  );
};
