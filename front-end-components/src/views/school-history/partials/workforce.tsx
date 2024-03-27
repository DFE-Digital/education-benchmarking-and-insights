import React, { useState } from "react";
import {
  ChartDimensions,
  ChartMode,
  ChartModeChart,
  PupilsPerStaffRole,
  WorkforceCategories,
} from "src/components";
import { ChartModeContext } from "src/contexts";

export const Workforce: React.FC = () => {
  const [displayMode, setDisplayMode] = useState<string>(ChartModeChart);
  const [dimension, setDimension] = useState(PupilsPerStaffRole);
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
            dimensions={WorkforceCategories}
            handleChange={handleSelectChange}
            elementId="workforce"
            defaultValue={dimension}
          />
        </div>
        <div className="govuk-grid-column-one-third">
          <ChartMode
            displayMode={displayMode}
            handleChange={toggleChartMode}
            prefix="workforce"
          />
        </div>
      </div>
      <hr className="govuk-section-break govuk-section-break--l govuk-section-break--visible govuk-!-margin-top-0" />

      <h2 className="govuk-heading-m">
        School workforce (full time equivalent)
      </h2>
      <details className="govuk-details">
        <summary className="govuk-details__summary">
          <span className="govuk-details__summary-text">
            More about school workforce
          </span>
        </summary>
        <div className="govuk-details__text">
          <p>
            This includes non-classroom based support staff, and full-time
            equivalent:
            <ul className="govuk-list govuk-list--bullet">
              <li>classroom teachers</li>
              <li>senior leadership</li>
              <li>teaching assistants</li>
            </ul>
          </p>
        </div>
      </details>
      <h2 className="govuk-heading-m">
        Total number of teachers (full time equivalent)
      </h2>
      <details className="govuk-details">
        <summary className="govuk-details__summary">
          <span className="govuk-details__summary-text">
            More about total number of teachers workforce
          </span>
        </summary>
        <div className="govuk-details__text">
          <p>
            This is the full-time equivalent of all classroom and leadership
            teachers.
          </p>
        </div>
      </details>
      <h2 className="govuk-heading-m">
        Teachers with qualified teacher status
      </h2>
      <details className="govuk-details">
        <summary className="govuk-details__summary">
          <span className="govuk-details__summary-text">
            More about teachers with qualified teacher status
          </span>
        </summary>
        <div className="govuk-details__text">
          <p>
            We divided the number of teachers with qualified teacher status by
            the total number of teachers.
          </p>
        </div>
      </details>
      <h2 className="govuk-heading-m">
        Senior leadership (full time equivalent)
      </h2>
      <details className="govuk-details">
        <summary className="govuk-details__summary">
          <span className="govuk-details__summary-text">
            More about senior leadership
          </span>
        </summary>
        <div className="govuk-details__text">
          <p>
            This is the full-time equivalent of senior leadership roles,
            including:
            <ul className="govuk-list govuk-list--bullet">
              <li>headteachers</li>
              <li>deputy headteachers</li>
              <li>assistant headteachers</li>
            </ul>
          </p>
        </div>
      </details>

      <h2 className="govuk-heading-m">
        Teaching assistants (full time equivalent)
      </h2>
      <details className="govuk-details">
        <summary className="govuk-details__summary">
          <span className="govuk-details__summary-text">
            More about teaching assistants
          </span>
        </summary>
        <div className="govuk-details__text">
          <p>
            This is the full-time equivalent of teaching assistants, including:
            <ul className="govuk-list govuk-list--bullet">
              <li>teaching assistants</li>
              <li>higher level teaching assistants</li>
              <li>education needs support staff</li>
            </ul>
          </p>
        </div>
      </details>
      <h2 className="govuk-heading-m">
        Non-classroom support staff - excluding auxiliary staff (full time
        equivalent)
      </h2>
      <details className="govuk-details">
        <summary className="govuk-details__summary">
          <span className="govuk-details__summary-text">
            More about non-classroom support staff
          </span>
        </summary>
        <div className="govuk-details__text">
          <p>
            This is the full-time equivalent of non-classroom-based support
            staff, excluding:
            <ul className="govuk-list govuk-list--bullet">
              <li>auxiliary staff</li>
              <li>third party support staff</li>
            </ul>
          </p>
        </div>
      </details>
      <h2 className="govuk-heading-m">
        Auxiliary staff (full time equivalent)
      </h2>
      <details className="govuk-details">
        <summary className="govuk-details__summary">
          <span className="govuk-details__summary-text">
            More about auxiliary staff
          </span>
        </summary>
        <div className="govuk-details__text">
          <p>
            This is the full-time equivalent of auxiliary staff, including;
            <ul className="govuk-list govuk-list--bullet">
              <li>catering</li>
              <li>school maintenance staff</li>
            </ul>
          </p>
        </div>
      </details>
      <h2 className="govuk-heading-m">School workforce (headcount)</h2>
      <details className="govuk-details">
        <summary className="govuk-details__summary">
          <span className="govuk-details__summary-text">
            More about school workforce (headcount)
          </span>
        </summary>
        <div className="govuk-details__text">
          <p>
            This is the total headcount of the school workforce, including:
            <ul className="govuk-list govuk-list--bullet">
              <li>
                full and part-time teachers (including school leadership
                teachers)
              </li>
              <li>teaching assistant</li>
              <li>non-classroom based support staff</li>
            </ul>
          </p>
        </div>
      </details>
    </ChartModeContext.Provider>
  );
};
