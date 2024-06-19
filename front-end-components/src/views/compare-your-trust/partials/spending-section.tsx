import React from "react";
import {
  TotalExpenditure,
  ExpenditureAccordion,
  SpendingSectionProps,
} from "src/views/compare-your-trust/partials";
import { ChartMode } from "src/components";
import { IncludeBreakdown } from "src/components/include-breakdown";
import { useChartModeContext, useBreakdownContext } from "src/contexts";

export const SpendingSection: React.FC<SpendingSectionProps> = ({ id }) => {
  const { chartMode, setChartMode } = useChartModeContext();
  const { breakdown, setBreakdown } = useBreakdownContext(true);

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
      <hr className="govuk-section-break govuk-section-break--l govuk-section-break--visible govuk-!-margin-top-0" />
      <div>
        <TotalExpenditure id={id} />
        <ExpenditureAccordion id={id} />
      </div>
    </>
  );
};
