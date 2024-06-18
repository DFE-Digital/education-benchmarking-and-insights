import React, { useState } from "react";
import {
  TotalExpenditure,
  ExpenditureAccordion,
  SpendingSectionProps,
} from "src/views/compare-your-trust/partials";
import { ChartMode, ChartModeChart } from "src/components";
import {
  SelectedEstablishmentContext,
  ChartModeContext,
  IncludeBreakdownContext,
} from "src/contexts";
import {
  BreakdownInclude,
  IncludeBreakdown,
} from "src/components/include-breakdown";

export const SpendingSection: React.FC<SpendingSectionProps> = ({ id }) => {
  const [displayMode, setDisplayMode] = useState<string>(ChartModeChart);
  const [breakdown, setBreakdown] = useState<string | undefined>(
    BreakdownInclude
  );

  const toggleChartMode = (e: React.ChangeEvent<HTMLInputElement>) => {
    setDisplayMode(e.target.value);
  };

  const toggleBreakdown = (e: React.ChangeEvent<HTMLInputElement>) => {
    setBreakdown(e.target.value);
  };

  return (
    <SelectedEstablishmentContext.Provider value={id}>
      <div className="chart-options trust-options">
        <div>
          <ChartMode displayMode={displayMode} handleChange={toggleChartMode} />
        </div>
        <div>
          <IncludeBreakdown
            breakdown={breakdown}
            handleChange={toggleBreakdown}
          />
        </div>
      </div>
      <ChartModeContext.Provider value={displayMode}>
        <IncludeBreakdownContext.Provider value={breakdown}>
          <TotalExpenditure id={id} />
          <ExpenditureAccordion id={id} />
        </IncludeBreakdownContext.Provider>
      </ChartModeContext.Provider>
    </SelectedEstablishmentContext.Provider>
  );
};
