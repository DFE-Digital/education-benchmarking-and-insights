import React from "react";
import {
  ChartModeChart,
  ChartModeProps,
  ChartModeTable,
} from "src/components/chart-mode";

export const ChartMode: React.FC<ChartModeProps> = (props) => {
  const { chartMode, handleChange, prefix } = props;

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
              id={prefix ? `${prefix}-mode-chart` : "mode-chart"}
              name={prefix ? `${prefix}ChangedChartMode` : "changedChartMode"}
              type="radio"
              value={ChartModeChart}
              onChange={(e) => handleChange(e.target.value)}
              checked={chartMode == ChartModeChart}
            />
            <label
              className="govuk-label govuk-radios__label"
              htmlFor={prefix ? `${prefix}-mode-chart` : "mode-chart"}
            >
              {ChartModeChart}
            </label>
          </div>
          <div className="govuk-radios__item">
            <input
              className="govuk-radios__input"
              id={prefix ? `${prefix}-mode-table` : "mode-table"}
              name={prefix ? `${prefix}ChangedChartMode` : "changedChartMode"}
              type="radio"
              value={ChartModeTable}
              onChange={(e) => handleChange(e.target.value)}
              checked={chartMode == ChartModeTable}
            />
            <label
              className="govuk-label govuk-radios__label"
              htmlFor={prefix ? `${prefix}-mode-table` : "mode-table"}
            >
              {ChartModeTable}
            </label>
          </div>
        </div>
      </fieldset>
    </div>
  );
};
