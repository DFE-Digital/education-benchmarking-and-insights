import classNames from "classnames";
import React from "react";
import { ChartProgressProps } from "src/components/chart-progress";
import { ProgressBanding } from "src/views";

export const ChartProgress: React.FC<ChartProgressProps> = ({
  options,
  selected,
  stacked,
  onChecked,
}) => {
  if (!options || options.length === 0) {
    return null;
  }

  return (
    <div className="govuk-form-group">
      <fieldset className="govuk-fieldset">
        <legend className="govuk-fieldset__legend govuk-fieldset__legend--s">
          <h2 className="govuk-fieldset__heading">School performance</h2>
        </legend>
        <div
          className={classNames(
            "govuk-checkboxes govuk-checkboxes--small progress-checkboxes",
            { "govuk-checkboxes--inline": !stacked }
          )}
          data-module="govuk-checkboxes"
        >
          {options.map((o, i) => {
            let label: string = o;
            switch (o) {
              case ProgressBanding.AboveAverage:
                label = "Above average";
                break;
              case ProgressBanding.WellAboveAverage:
                label = "Well above average";
                break;
            }

            return (
              <div key={i} className="govuk-checkboxes__item">
                <input
                  className="govuk-checkboxes__input"
                  id={o}
                  name={o}
                  type="checkbox"
                  value={o}
                  onChange={() => onChecked(o)}
                  checked={selected.includes(o)}
                />
                <label
                  className="govuk-label govuk-checkboxes__label"
                  htmlFor={o}
                >
                  {label}
                </label>
              </div>
            );
          })}
        </div>
      </fieldset>
    </div>
  );
};
