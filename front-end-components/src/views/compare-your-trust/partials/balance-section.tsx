import React from "react";
import {
  BalanceSectionProps,
  Balance,
} from "src/views/compare-your-trust/partials";
import { ChartMode } from "src/components";
import { useChartModeContext } from "src/contexts";

export const BalanceSection: React.FC<BalanceSectionProps> = ({ id }) => {
  const { chartMode, setChartMode } = useChartModeContext();

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
      </div>
      <div>
        <Balance id={id} />
      </div>
    </>
  );
};
