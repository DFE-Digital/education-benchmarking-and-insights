import React from "react";
import {
  ChartModeChart,
  ChartModeProps,
  ChartModeTable,
} from "src/components/chart-mode";

export const ChartMode: React.FC<ChartModeProps> = (props) => {
  const { displayMode, handleChange } = props;

  return (
    <div className="govuk-form-group">
      <fieldset className="govuk-fieldset">
        <legend className="govuk-fieldset__legend govuk-fieldset__legend--s">
          <h2 className="govuk-fieldset__heading">View as</h2>
        </legend>
        <div
          className="govuk-radios govuk-radios--small govuk-radios--inline"
          data-module="govuk-radios"
        >
          <div className="govuk-radios__item">
            <input
              className="govuk-radios__input"
              id="mode-chart"
              name="changedChartMode"
              type="radio"
              value={ChartModeChart}
              defaultChecked={displayMode == ChartModeChart}
              onChange={handleChange}
              checked={displayMode == ChartModeChart}
            />
            <label
              className="govuk-label govuk-radios__label"
              htmlFor="mode-chart"
            >
              {ChartModeChart}
            </label>
          </div>
          <div className="govuk-radios__item">
            <input
              className="govuk-radios__input"
              id="mode-table"
              name="changedChartMode"
              type="radio"
              value={ChartModeTable}
              defaultChecked={displayMode == ChartModeTable}
              onChange={handleChange}
              checked={displayMode == ChartModeTable}
            />
            <label
              className="govuk-label govuk-radios__label"
              htmlFor="mode-table"
            >
              {ChartModeTable}
            </label>
          </div>
        </div>
      </fieldset>
    </div>
  );
};
