import React, {useCallback, useEffect, useLayoutEffect, useState} from 'react';
import TotalExpenditure from "../components/school-expenditure/total-expenditure";
import ExpenditureAccordion from "../components/school-expenditure/expenditure-accordion";
import SchoolApi, {ExpenditureResult, SchoolExpenditure} from "../services/school-api";
import {ChartMode, ChartModeContext, oppositeMode} from "../chart-more";

// @ts-ignore
import {initAll} from 'govuk-frontend'

type CompareYourSchoolViewProps = {
    urn: string
    academyYear: string
    maintainedYear: string
};

const CompareYourSchool: React.FC<CompareYourSchoolViewProps> = ({urn, academyYear, maintainedYear}) => {
    const [expenditureData, setExpenditureData] = useState<ExpenditureResult>();
    const [displayMode, setDisplayMode] = useState<ChartMode>(ChartMode.CHART);

    useLayoutEffect(() => {
        initAll();
    }, []);

    const getExpenditure = useCallback(async () => {
        return await SchoolApi.getSchoolExpenditure(urn);
    }, [urn])

    useEffect(() => {
        getExpenditure().then(setExpenditureData)
    }, [getExpenditure])


    function toggleChartMode() {
        setDisplayMode(oppositeMode(displayMode));
    }

    return (
        <>
            <div className="govuk-grid-row">
                <div className="govuk-grid-column-two-thirds">
                    <p className="govuk-body">
                        The data below is from the latest year available, For maintained schools this
                        is {maintainedYear},
                        academies for {academyYear}
                    </p>
                </div>
                <div className="govuk-grid-column-one-third">
                    <div className="govuk-form-group">
                        <fieldset className="govuk-fieldset">
                            <legend className="govuk-fieldset__legend govuk-fieldset__legend--s">
                                <h2 className="govuk-fieldset__heading">
                                    View as
                                </h2>
                            </legend>
                            <div className="govuk-radios govuk-radios--small govuk-radios--inline" data-module="govuk-radios">
                                <div className="govuk-radios__item">
                                    <input className="govuk-radios__input" id="mode-chart" name="changedChartMode"
                                           type="radio" value={ChartMode.CHART}
                                           defaultChecked={displayMode == ChartMode.CHART} onChange={toggleChartMode}/>
                                    <label className="govuk-label govuk-radios__label" htmlFor="mode-chart">
                                        Chart
                                    </label>
                                </div>
                                <div className="govuk-radios__item">
                                    <input className="govuk-radios__input" id="mode-table" name="changedChartMode"
                                           type="radio" value={ChartMode.TABLE}
                                           defaultChecked={displayMode == ChartMode.TABLE} onChange={toggleChartMode}/>
                                    <label className="govuk-label govuk-radios__label" htmlFor="mode-table">
                                        Table
                                    </label>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <button className="govuk-button govuk-button--secondary" data-module="govuk-button"
                            onClick={toggleChartMode}>
                        {oppositeMode(displayMode)}
                    </button>
                </div>
            </div>
            <ChartModeContext.Provider value={displayMode}>
                <TotalExpenditure urn={urn}
                                  schools={expenditureData ? expenditureData.results : new Array<SchoolExpenditure>()}/>
                <ExpenditureAccordion urn={urn}
                                      schools={expenditureData ? expenditureData.results : new Array<SchoolExpenditure>()}/>
            </ChartModeContext.Provider>
        </>
    )
};

export default CompareYourSchool;