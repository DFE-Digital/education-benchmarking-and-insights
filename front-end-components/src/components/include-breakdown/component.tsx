import React from "react";
import {
  BreakdownInclude,
  BreakdownExclude,
  IncludeBreakdownProps,
} from "src/components/include-breakdown";

export const IncludeBreakdown: React.FC<IncludeBreakdownProps> = (props) => {
  const { breakdown, handleChange, prefix } = props;

  if (breakdown === undefined) {
    return null;
  }

  return (
    <div className="govuk-form-group">
      <fieldset className="govuk-fieldset">
        <legend className="govuk-fieldset__legend govuk-fieldset__legend--s">
          <h2 className="govuk-fieldset__heading">Include central spending</h2>
        </legend>
        <div
          className="govuk-radios govuk-radios--small govuk-radios--inline"
          data-module="govuk-radios"
        >
          <div className="govuk-radios__item">
            <input
              className="govuk-radios__input"
              id={prefix ? `${prefix}-include-breakdown` : "include-breakdown"}
              name={
                prefix
                  ? `${prefix}ChangedIncludeBreakdown`
                  : "changedIncludeBreakdown"
              }
              type="radio"
              value={BreakdownInclude}
              onChange={handleChange}
              checked={breakdown == BreakdownInclude}
            />
            <label
              className="govuk-label govuk-radios__label"
              htmlFor={
                prefix ? `${prefix}-include-breakdown` : "include-breakdown"
              }
            >
              {BreakdownInclude}
            </label>
          </div>
          <div className="govuk-radios__item">
            <input
              className="govuk-radios__input"
              id={prefix ? `${prefix}-exclude-breakdown` : "exclude-breakdown"}
              name={
                prefix
                  ? `${prefix}ChangedIncludeBreakdown`
                  : "changedIncludeBreakdown"
              }
              type="radio"
              value={BreakdownExclude}
              onChange={handleChange}
              checked={breakdown == BreakdownExclude}
            />
            <label
              className="govuk-label govuk-radios__label"
              htmlFor={
                prefix ? `${prefix}-exclude-breakdown` : "exclude-breakdown"
              }
            >
              {BreakdownExclude}
            </label>
          </div>
        </div>
      </fieldset>
    </div>
  );
};
