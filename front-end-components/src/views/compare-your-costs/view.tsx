import React, { useState } from "react";
import {
  TotalExpenditure,
  ExpenditureAccordion,
} from "src/views/compare-your-costs/partials";
import { CompareYourCostsViewProps } from "src/views/compare-your-costs";
import { ChartMode, ChartModeChart } from "src/components";
import {
  SelectedEstablishmentContext,
  //HasIncompleteDataContext,
  PhaseContext,
  useChartModeContext,
  ChartModeProvider,
} from "src/contexts";
import { useGovUk } from "src/hooks/useGovUk";

export const CompareYourCosts: React.FC<CompareYourCostsViewProps> = (
  props
) => {
  const { type, id, phases } = props;
  const { chartMode, setChartMode } = useChartModeContext();
  const [phase, setPhase] = useState<string | undefined>(
    phases ? phases[0] : undefined
  );

  useGovUk();

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
          {/*<HasIncompleteDataContext.Provider*/}
          {/*  value={{ hasIncompleteData, hasNoData }}*/}
          {/*>*/}
          <TotalExpenditure id={id} type={type} />
          <ExpenditureAccordion id={id} type={type} />
          {/*</HasIncompleteDataContext.Provider>*/}
        </ChartModeProvider>
      </PhaseContext.Provider>
    </SelectedEstablishmentContext.Provider>
  );
};
