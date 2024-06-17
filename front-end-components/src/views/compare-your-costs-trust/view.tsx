import React, { useState } from "react";
import {
  TotalExpenditure,
  ExpenditureAccordion,
} from "src/views/compare-your-costs-trust/partials";
import { CompareYourCostsTrustViewProps } from "src/views/compare-your-costs-trust";
import { ChartMode, ChartModeChart } from "src/components";
import {
  SelectedEstablishmentContext,
  ChartModeContext,
  IncludeBreakdownContext,
} from "src/contexts";
import { useGovUk } from "src/hooks/useGovUk";
import {
  BreakdownInclude,
  IncludeBreakdown,
} from "src/components/include-breakdown";

export const CompareYourCostsTrust: React.FC<CompareYourCostsTrustViewProps> = (
  props
) => {
  const { type, id } = props;
  const [displayMode, setDisplayMode] = useState<string>(ChartModeChart);
  const [breakdown, setBreakdown] = useState<string | undefined>(
    BreakdownInclude
  );

  useGovUk();

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
          <TotalExpenditure id={id} type={type} />
          <ExpenditureAccordion id={id} type={type} />
        </IncludeBreakdownContext.Provider>
      </ChartModeContext.Provider>
    </SelectedEstablishmentContext.Provider>
  );
};
