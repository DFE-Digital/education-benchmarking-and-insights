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
  CostCodeMapProvider,
} from "src/contexts";
import { useGovUk } from "src/hooks/useGovUk";
import { ChartOptionsPhaseMode } from "src/components/chart-options-phase-mode";

export const CompareYourCosts: React.FC<CompareYourCostsViewProps> = ({
  costCodeMap,
  customDataId,
  dispatchEventType,
  id,
  phases,
  suppressNegativeOrZero,
  tags,
  type,
}) => {
  const [phase, setPhase] = useState<string | undefined>(
    phases ? phases[0] : undefined
  );

  const handleFetching = (fetching: boolean) => {
    if (dispatchEventType) {
      document.dispatchEvent(
        new CustomEvent<boolean>(dispatchEventType, {
          detail: !fetching,
        })
      );
    }
  };

  const message = "Only displaying schools with positive expenditure.";

  useGovUk();

  return (
    <SelectedEstablishmentContext.Provider value={id}>
      <PhaseContext.Provider value={phase}>
        <CustomDataContext.Provider value={customDataId}>
          <SuppressNegativeOrZeroContext.Provider
            value={{ suppressNegativeOrZero, message }}
          >
            <CostCodeMapProvider costCodeMap={costCodeMap} tags={tags}>
              <ChartModeProvider initialValue={ChartModeChart}>
                <ChartOptionsPhaseMode
                  phases={phases}
                  handlePhaseChange={setPhase}
                />
                <TotalExpenditure
                  id={id}
                  type={type}
                  onFetching={handleFetching}
                />
                <ExpenditureAccordion id={id} type={type} />
              </ChartModeProvider>
            </CostCodeMapProvider>
          </SuppressNegativeOrZeroContext.Provider>
        </CustomDataContext.Provider>
      </PhaseContext.Provider>
    </SelectedEstablishmentContext.Provider>
  );
};
