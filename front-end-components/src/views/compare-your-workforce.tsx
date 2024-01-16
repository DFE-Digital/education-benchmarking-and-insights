import React, {useCallback, useEffect, useLayoutEffect, useState} from 'react';
import SchoolApi, {WorkforceBenchmark, WorkforceBenchmarkResult} from '../services/school-api';
import {ChartMode, oppositeMode} from "../chart-mode";
import {ChartModeContext, SelectedSchoolContext} from '../contexts';

// @ts-ignore
import {initAll} from 'govuk-frontend';
import ToggleChartMode from "../components/toggle-chart-mode.tsx";
import SchoolWorkforce from "../components/school-workforce/school-workforce.tsx";
import TotalTeachers from "../components/school-workforce/total-teachers.tsx";
import TotalTeachersQualified from "../components/school-workforce/total-teachers-qualified.tsx";
import SeniorLeadership from "../components/school-workforce/senior-leadership.tsx";
import TeachingAssistants from "../components/school-workforce/teaching-assistants.tsx";
import NonClassroomSupport from "../components/school-workforce/non-classroom-support.tsx";
import AuxiliaryStaff from "../components/school-workforce/auxiliary-staff.tsx";
import Headcount from "../components/school-workforce/headcount.tsx";
import {SelectedSchool} from "../types.tsx";

type CompareYourWorkforceViewProps = {
    urn: string;
    academyYear: string;
    maintainedYear: string;
};

const CompareYourWorkforce: React.FC<CompareYourWorkforceViewProps> = (props) => {
    const {urn, academyYear, maintainedYear} = props
    const [workforceData, setWorkforceData] = useState<WorkforceBenchmarkResult>();
    const [displayMode, setDisplayMode] = useState<ChartMode>(ChartMode.CHART);
    const [selectedSchool, setSelectedSchool] = useState<SelectedSchool>({urn: "", name: ""});

    useLayoutEffect(() => {
        initAll();
    }, []);

    const getWorkforceData = useCallback(async () => {
        return await SchoolApi.getWorkforceBenchmarkData(urn);
    }, [urn]);

    useEffect(() => {
        getWorkforceData().then((data) => {
            setWorkforceData(data)

            const currentSchool = data.results.find(school => school.urn == urn);
            if (currentSchool) {
                setSelectedSchool({urn: currentSchool.urn, name: currentSchool.name})
            }
        })

    }, [getWorkforceData]);

    // Function to toggle between chart and table display modes
    const toggleChartMode = () => {
        setDisplayMode(oppositeMode(displayMode));
    };

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
                <SchoolWorkforce schools={workforceData ? workforceData.results : new Array<WorkforceBenchmark>()}/>
                <TotalTeachers schools={workforceData ? workforceData.results : new Array<WorkforceBenchmark>()}/>
                <TotalTeachersQualified schools={workforceData ? workforceData.results : new Array<WorkforceBenchmark>()}/>
                <SeniorLeadership schools={workforceData ? workforceData.results : new Array<WorkforceBenchmark>()}/>
                <TeachingAssistants schools={workforceData ? workforceData.results : new Array<WorkforceBenchmark>()}/>
                <NonClassroomSupport schools={workforceData ? workforceData.results : new Array<WorkforceBenchmark>()}/>
                <AuxiliaryStaff schools={workforceData ? workforceData.results : new Array<WorkforceBenchmark>()}/>
                <Headcount schools={workforceData ? workforceData.results : new Array<WorkforceBenchmark>()}/>
            </ChartModeContext.Provider>
        </SelectedSchoolContext.Provider>
    );
};

export default CompareYourWorkforce;
