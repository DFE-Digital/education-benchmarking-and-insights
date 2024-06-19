import React from "react";
import {
  TotalExpenditure,
  ExpenditureAccordion,
  SpendingSectionProps,
} from "src/views/compare-your-trust/partials";
import { ChartMode } from "src/components";
import { IncludeBreakdown } from "src/components/include-breakdown";
import { useChartModeContext, useIncludeBreakdownContext } from "src/contexts";

export const SpendingSection: React.FC<SpendingSectionProps> = ({ id }) => {
  const { chartMode, setChartMode } = useChartModeContext();
  const { breakdown, setBreakdown } = useIncludeBreakdownContext();

  return (
    <>
      <div className="chart-options trust-options">
        <div>
          <ChartMode
            chartMode={chartMode}
            handleChange={setChartMode}
            prefix="spending"
          />
        </div>
        <div>
          <IncludeBreakdown
            breakdown={breakdown}
            handleChange={setBreakdown}
            prefix="spending"
          />
        </div>
      </div>
      <div>
        <TotalExpenditure id={id} />
        <ExpenditureAccordion id={id} />
      </div>
    </>
  );
};
