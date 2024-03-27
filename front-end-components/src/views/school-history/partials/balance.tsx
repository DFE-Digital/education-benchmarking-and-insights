import React, { useState } from "react";
import {
  ChartDimensions,
  ChartMode,
  ChartModeChart,
  CostCategories,
  PoundsPerPupil,
} from "src/components";
import { ChartModeContext } from "src/contexts";

export const Balance: React.FC = () => {
  const [displayMode, setDisplayMode] = useState<string>(ChartModeChart);
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const toggleChartMode = (e: React.ChangeEvent<HTMLInputElement>) => {
    setDisplayMode(e.target.value);
  };

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    setDimension(event.target.value);
  };

  return (
    <ChartModeContext.Provider value={displayMode}>
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-two-thirds">
          <ChartDimensions
            dimensions={CostCategories}
            handleChange={handleSelectChange}
            elementId="balance"
            defaultValue={dimension}
          />
        </div>
        <div className="govuk-grid-column-one-third">
          <ChartMode
            displayMode={displayMode}
            handleChange={toggleChartMode}
            prefix="balance"
          />
        </div>
      </div>
      <hr className="govuk-section-break govuk-section-break--l govuk-section-break--visible govuk-!-margin-top-0" />

      <h2 className="govuk-heading-m">In-year balance</h2>
      <h2 className="govuk-heading-m">Revenue reserve</h2>
      <details className="govuk-details">
        <summary className="govuk-details__summary">
          <span className="govuk-details__summary-text">
            More about revenue reserve
          </span>
        </summary>
        <div className="govuk-details__text">
          <p>
            Local authority maintained schools and single academy trusts
            <br />
            Reserves are legally associated with one school and appear in that
            school’s graphs.
          </p>
          <p>
            Local authority maintained schools
            <br />
            Reserves include committed and uncommitted revenue balance. They
            also include the community-focused extended school revenue balance.
          </p>

          <p>
            Single academy trusts
            <br />
            This is calculated by:
            <ul className="govuk-list govuk-list--bullet">
              <li>
                carrying forward the closing balance (restricted and
                unrestricted funds) from the previous year
              </li>
              <li>
                adding total income in the current year (revenue, funds
                inherited on conversion/transfer and contributions from
                academies to trust)
              </li>
              <li>subtracting total expenditure in the current year</li>
            </ul>
          </p>
          <p>
            Multi-academy trust
            <br />
            The trust is the legal entity and all revenue reserves legally
            belong to it.
          </p>
          <p>
            Single academies in multi-academy trusts (MATs)
            <br />
            We estimated a value per academy by diving up and sharing out the
            trust’s reserves on a pro-rata basis. For this we used the full-time
            equivalent (FTE) number of pupils in each academy in that MAT.
          </p>
        </div>
      </details>
    </ChartModeContext.Provider>
  );
};
