import React, {useLayoutEffect, useCallback, useState, useEffect} from 'react';
import TotalExpenditure from "../components/school-expenditure/total-expenditure";
import ExpenditureAccordion from "../components/school-expenditure/expenditure-accordion";
import SchoolApi, {ExpenditureResult, SchoolExpenditure} from "../services/school-api";
import {ChartMode} from "../constants";

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
        const data = await SchoolApi.getSchoolExpenditure(urn);
        setExpenditureData(data);
    }, [urn])

    useEffect(() => {
        getExpenditure()
    }, [getExpenditure])

    const oppositeMode = (currentMode : ChartMode) => {
        return currentMode == ChartMode.TABLE ? ChartMode.CHART : ChartMode.TABLE
    }

    function toggleChartMode() {
        setDisplayMode(oppositeMode(displayMode));
    }

    return (
        <>
            <div className="govuk-grid-row">
                <div className="govuk-grid-column-two-thirds">
                    <p className="govuk-body">
                        The data below is from the latest year available, For maintained schools this is {maintainedYear},
                        academies for {academyYear}
                    </p>
                </div>
                <div className="govuk-grid-column-one-third">
                    <button className="govuk-button" data-module="govuk-button" onClick={toggleChartMode}>
                        {oppositeMode(displayMode)}
                    </button>
                </div>
            </div>
            <TotalExpenditure urn={urn}
                              schools={expenditureData ? expenditureData.results : new Array<SchoolExpenditure>()}
                              mode={displayMode}/>
            <ExpenditureAccordion urn={urn}
                                  schools={expenditureData ? expenditureData.results : new Array<SchoolExpenditure>()}
                                  mode={displayMode}/>
        </>
    )
};

export default CompareYourSchool;