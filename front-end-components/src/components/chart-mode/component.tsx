import React from "react";
import {ChartModeProps, ChartModes} from "src/components/chart-mode";

export const OppositeMode = (currentMode: ChartModes) => {
    return currentMode == ChartModes.TABLE ? ChartModes.CHART : ChartModes.TABLE
}

export const ChartMode: React.FC<ChartModeProps> = (props) => {
    const {displayMode, handleChange} = props

    return (
        <div className="govuk-form-group">
            <fieldset className="govuk-fieldset">
                <legend className="govuk-fieldset__legend govuk-fieldset__legend--s">
                    <h2 className="govuk-fieldset__heading">
                        View as
                    </h2>
                </legend>
                <div className="govuk-radios govuk-radios--small govuk-radios--inline"
                     data-module="govuk-radios">
                    <div className="govuk-radios__item">
                        <input className="govuk-radios__input" id="mode-chart" name="changedChartMode"
                               type="radio" value={ChartModes.CHART}
                               defaultChecked={displayMode == ChartModes.CHART}
                               onChange={handleChange}
                               checked={displayMode == ChartModes.CHART}
                        />
                        <label className="govuk-label govuk-radios__label" htmlFor="mode-chart">
                            Chart
                        </label>
                    </div>
                    <div className="govuk-radios__item">
                        <input className="govuk-radios__input" id="mode-table" name="changedChartMode"
                               type="radio" value={ChartModes.TABLE}
                               defaultChecked={displayMode == ChartModes.TABLE}
                               onChange={handleChange}
                               checked={displayMode == ChartModes.TABLE}
                        />
                        <label className="govuk-label govuk-radios__label" htmlFor="mode-table">
                            Table
                        </label>
                    </div>
                </div>
            </fieldset>
            <button className="govuk-button govuk-button--secondary"
                    data-module="govuk-button"
                    onClick={handleChange}
            >
                {OppositeMode(displayMode)}
            </button>
        </div>
    )
};