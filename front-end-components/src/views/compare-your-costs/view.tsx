import React, { useState } from "react";
import {
  TotalExpenditure,
  ExpenditureAccordion,
} from "src/views/compare-your-costs/partials";
import { CompareYourCostsViewProps } from "src/views/compare-your-costs";
import { ChartModeChart } from "src/components";
import {
  SelectedEstablishmentContext,
  PhaseContext,
  CustomDataContext,
  ChartModeProvider,
  SuppressNegativeOrZeroContext,
} from "src/contexts";
import { useGovUk } from "src/hooks/useGovUk";
import { ChartOptionsPhaseMode } from "src/components/chart-options-phase-mode";

export const CompareYourCosts: React.FC<CompareYourCostsViewProps> = (
  props
) => {
  const { type, id, phases, customDataId, suppressNegativeOrZero } = props;
  const [phase, setPhase] = useState<string | undefined>(
    phases ? phases[0] : undefined
  );

  const message =
    "Only displaying other similar schools with positive expenditure";

  useGovUk();

  return (
    <SelectedEstablishmentContext.Provider value={id}>
      <PhaseContext.Provider value={phase}>
        <CustomDataContext.Provider value={customDataId}>
          <SuppressNegativeOrZeroContext.Provider
            value={{ suppressNegativeOrZero, message }}
          >
            <ChartModeProvider initialValue={ChartModeChart}>
              <ChartOptionsPhaseMode
                phases={phases}
                handlePhaseChange={setPhase}
              />
              <TotalExpenditure id={id} type={type} />
              <ExpenditureAccordion id={id} type={type} />
            </ChartModeProvider>
          </SuppressNegativeOrZeroContext.Provider>
        </CustomDataContext.Provider>
      </PhaseContext.Provider>
    </SelectedEstablishmentContext.Provider>
  );
};
