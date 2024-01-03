import React, {useCallback, useEffect, useLayoutEffect, useState} from 'react';
import TotalExpenditure from "../components/school-expenditure/total-expenditure";
import ExpenditureAccordion from "../components/school-expenditure/expenditure-accordion";
import SchoolApi, {ExpenditureResult, SchoolExpenditure} from "../services/school-api";
import {ChartMode, oppositeMode} from "../chart-mode";
import {CompareYourSchoolViewProps, SelectedSchool} from "../types";
import {ChartModeContext, SelectedSchoolContext} from '../contexts';

// @ts-ignore
import {initAll} from 'govuk-frontend'
import ToggleChartMode from "../components/toggle-chart-mode.tsx";


const CompareYourSchool: React.FC<CompareYourSchoolViewProps> = (props) => {
    const {urn, academyYear, maintainedYear} = props
    const [expenditureData, setExpenditureData] = useState<ExpenditureResult>();
    const [displayMode, setDisplayMode] = useState<ChartMode>(ChartMode.CHART);
    const [selectedSchool, setSelectedSchool] = useState<SelectedSchool>({urn: "", name:""});

    useLayoutEffect(() => {
        initAll();
    }, []);

    const getExpenditure = useCallback(async () => {
        return await SchoolApi.getSchoolExpenditure(urn);
    }, [urn])

    useEffect(() => {
        getExpenditure().then((data) => {
            setExpenditureData(data)

            const currentSchool = data.results.find(school => school.urn == urn);
            if(currentSchool) {
                setSelectedSchool({urn: currentSchool.urn, name: currentSchool.name})
            }
        })
    }, [getExpenditure])

    const toggleChartMode = () => {
        setDisplayMode(oppositeMode(displayMode));
    }

    return (
        <SelectedSchoolContext.Provider value={selectedSchool}>
            <div className="govuk-grid-row">
                <div className="govuk-grid-column-two-thirds">
                    <p className="govuk-body">
                        The data below is from the latest year available, For maintained schools this
                        is {maintainedYear},
                        academies for {academyYear}
                    </p>
                </div>
                <div className="govuk-grid-column-one-third">
                    <ToggleChartMode displayMode={displayMode} handleChange={toggleChartMode}/>
                </div>
            </div>
            <ChartModeContext.Provider value={displayMode}>
                <TotalExpenditure schools={expenditureData ? expenditureData.results : new Array<SchoolExpenditure>()}/>
                <ExpenditureAccordion
                    schools={expenditureData ? expenditureData.results : new Array<SchoolExpenditure>()}/>
            </ChartModeContext.Provider>
        </SelectedSchoolContext.Provider>
    )
};

export default CompareYourSchool;