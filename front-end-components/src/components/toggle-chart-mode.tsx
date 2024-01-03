import React from "react";
import {ChartMode, oppositeMode} from "../chart-mode";
import {ToggleChartModeProps} from "../types";

const ToggleChartMode: React.FC<ToggleChartModeProps> = (props) => {
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
                               type="radio" value={ChartMode.CHART}
                               defaultChecked={displayMode == ChartMode.CHART}
                               onChange={handleChange}
                               checked={displayMode == ChartMode.CHART}
                        />
                        <label className="govuk-label govuk-radios__label" htmlFor="mode-chart">
                            Chart
                        </label>
                    </div>
                    <div className="govuk-radios__item">
                        <input className="govuk-radios__input" id="mode-table" name="changedChartMode"
                               type="radio" value={ChartMode.TABLE}
                               defaultChecked={displayMode == ChartMode.TABLE}
                               onChange={handleChange}
                               checked={displayMode == ChartMode.TABLE}
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
                {oppositeMode(displayMode)}
            </button>
        </div>
    )
};

export default ToggleChartMode