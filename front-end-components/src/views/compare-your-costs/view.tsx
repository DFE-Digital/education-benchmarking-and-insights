import React, { useState } from "react";
import {
  TotalExpenditure,
  ExpenditureAccordion,
} from "src/views/compare-your-costs/partials";
import { CompareYourCostsViewProps } from "src/views/compare-your-costs";
import { ChartMode, ChartModeChart } from "src/components";
import {
  SelectedEstablishmentContext,
  ChartModeContext,
  //HasIncompleteDataContext,
  PhaseContext,
} from "src/contexts";
import { useGovUk } from "src/hooks/useGovUk";

export const CompareYourCosts: React.FC<CompareYourCostsViewProps> = (
  props
) => {
  const { type, id, phases } = props;
  const [displayMode, setDisplayMode] = useState<string>(ChartModeChart);
  const [phase, setPhase] = useState<string | undefined>(
    phases ? phases[0] : undefined
  );

  useGovUk();

  const toggleChartMode = (e: React.ChangeEvent<HTMLInputElement>) => {
    setDisplayMode(e.target.value);
  };

  const handlePhaseChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setPhase(e.target.value);
  };

  return (
    <SelectedEstablishmentContext.Provider value={id}>
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
        {/*<HasIncompleteDataContext.Provider*/}
        {/*  value={{ hasIncompleteData, hasNoData }}*/}
        {/*>*/}
        <ChartModeContext.Provider value={displayMode}>
          <TotalExpenditure id={id} type={type} />
          <ExpenditureAccordion id={id} type={type} />
        </ChartModeContext.Provider>
        {/*</HasIncompleteDataContext.Provider>*/}
      </PhaseContext.Provider>
    </SelectedEstablishmentContext.Provider>
  );
};
