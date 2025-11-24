import React, { useState } from "react";
import {
  TotalExpenditure,
  ExpenditureAccordion,
} from "src/views/compare-your-costs/partials";
import { CompareYourCostsViewProps } from "src/views/compare-your-costs";
import { ChartModeChart, ChartOptions } from "src/components";
import {
  SelectedEstablishmentContext,
  PhaseContext,
  CustomDataContext,
  ChartModeProvider,
  SuppressNegativeOrZeroContext,
  CostCodesProvider,
} from "src/contexts";
import { useGovUk } from "src/hooks/useGovUk";

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
            <CostCodesProvider costCodeMap={costCodeMap} tags={tags}>
              <ChartModeProvider initialValue={ChartModeChart}>
                <ChartOptions
                  className="flex-spaced"
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
            </CostCodesProvider>
          </SuppressNegativeOrZeroContext.Provider>
        </CustomDataContext.Provider>
      </PhaseContext.Provider>
    </SelectedEstablishmentContext.Provider>
  );
};
