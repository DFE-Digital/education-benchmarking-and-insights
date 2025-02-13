import React, { useState } from "react";
import {
  BalanceSectionProps,
  Balance,
} from "src/views/compare-your-trust/partials";
import {
  ChartDimensions,
  ChartMode,
  CostCategories,
  PoundsPerPupil,
} from "src/components";
import { useChartModeContext } from "src/contexts";

export const BalanceSection: React.FC<BalanceSectionProps> = ({ id }) => {
  const defaultDimension = PoundsPerPupil;
  const { chartMode, setChartMode } = useChartModeContext();
  const [dimension, setDimension] = useState(defaultDimension);

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    const dimension =
      CostCategories.find((x) => x.value === event.target.value) ??
      defaultDimension;
    setDimension(dimension);
  };

  return (
    <>
      <div className="chart-options trust-options">
        <div>
          <ChartDimensions
            dimensions={CostCategories}
            handleChange={handleSelectChange}
            elementId="balance"
            value={dimension.value}
          />
        </div>
        <div>
          <ChartMode
            chartMode={chartMode}
            handleChange={setChartMode}
            prefix="balance"
          />
        </div>
      </div>
      <hr className="govuk-section-break govuk-section-break--l govuk-section-break--visible govuk-!-margin-top-0" />
      <div>
        <Balance dimension={dimension} id={id} />
      </div>
    </>
  );
};
