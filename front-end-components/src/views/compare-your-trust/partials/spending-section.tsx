import React from "react";
import {
  TotalExpenditure,
  ExpenditureAccordion,
  SpendingSectionProps,
} from "src/views/compare-your-trust/partials";
import { ChartMode } from "src/components";
import { CentralServicesBreakdown } from "src/components/central-services-breakdown";
import {
  useChartModeContext,
  useCentralServicesBreakdownContext,
} from "src/contexts";

export const SpendingSection: React.FC<SpendingSectionProps> = ({ id }) => {
  const { chartMode, setChartMode } = useChartModeContext();
  const { breakdown, setBreakdown } = useCentralServicesBreakdownContext(true);

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
          <CentralServicesBreakdown
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
