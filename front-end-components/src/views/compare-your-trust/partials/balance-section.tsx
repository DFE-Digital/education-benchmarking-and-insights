import React from "react";
import {
  BalanceSectionProps,
  Balance,
} from "src/views/compare-your-trust/partials";
import { ChartMode } from "src/components";
import { IncludeBreakdown } from "src/components/include-breakdown";
import { useChartModeContext, useIncludeBreakdownContext } from "src/contexts";

export const BalanceSection: React.FC<BalanceSectionProps> = ({ id }) => {
  const { chartMode, setChartMode } = useChartModeContext();
  const { breakdown, setBreakdown } = useIncludeBreakdownContext();

  return (
    <>
      <div className="chart-options trust-options">
        <div>
          <ChartMode
            chartMode={chartMode}
            handleChange={setChartMode}
            prefix="balance"
          />
        </div>
        <div>
          <IncludeBreakdown
            breakdown={breakdown}
            handleChange={setBreakdown}
            prefix="balance"
          />
        </div>
      </div>
      <div>
        <Balance id={id} />
      </div>
    </>
  );
};
