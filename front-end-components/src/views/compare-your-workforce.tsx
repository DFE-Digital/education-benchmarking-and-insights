import React, {useCallback, useEffect, useLayoutEffect, useState} from 'react';
import SchoolApi, {WorkforceBenchmarkResult} from '../services/school-api';
import {ChartMode, oppositeMode} from "../chart-mode";
import {ChartModeContext, SelectedSchoolContext} from '../contexts';
import {ChartWrapperData, SelectedSchool, ChartDataPoint} from "../types";

// @ts-ignore
import {initAll} from 'govuk-frontend';
import ToggleChartMode from "../components/toggle-chart-mode.tsx";
import ChartWrapper from "../components/chart-wrapper.tsx";
import {CalculateCostValue, CostCategories, DimensionHeading, PoundsPerPupil} from "../chart-dimensions.tsx";

type CompareYourWorkforceViewProps = {
    urn: string;
    academyYear: string;
    maintainedYear: string;
};

const CompareYourWorkforce: React.FC<CompareYourWorkforceViewProps> = ({urn}) => {
    const [dimension, setDimension] = useState(PoundsPerPupil)
    const [workforceData, setWorkforceData] = useState<WorkforceBenchmarkResult>();
    const [displayMode, setDisplayMode] = useState<ChartMode>(ChartMode.CHART);
    const [selectedSchool, setSelectedSchool] = useState<SelectedSchool>({urn: "", name: ""});
    const tableHeadings = ["School name", "Local Authority", "School type", "Number of pupils", DimensionHeading(dimension)]

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

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    const chartDimensions = {dimensions: CostCategories, handleChange: handleSelectChange}

    const schoolWorkforceBarData: ChartWrapperData = {
        dataPoints: workforceData ? workforceData.results.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.schoolWorkforceFTE,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }) : new Array<ChartDataPoint>(),
        tableHeadings: tableHeadings
    }

    const totalNumberOfTeachersFTEBarData: ChartWrapperData = {
        dataPoints: workforceData ? workforceData.results.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.totalNumberOfTeachersFTE,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }) : new Array<ChartDataPoint>(),
        tableHeadings: tableHeadings
    }

    const teachersWithQTSFTEBarData: ChartWrapperData = {
        dataPoints: workforceData ? workforceData.results.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.teachersWithQTSFTE,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }) : new Array<ChartDataPoint>(),
        tableHeadings: tableHeadings
    }

    const seniorLeadershipFTEBarData: ChartWrapperData = {
        dataPoints: workforceData ? workforceData.results.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.seniorLeadershipFTE,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }) : new Array<ChartDataPoint>(),
        tableHeadings: tableHeadings
    }

    const teachingAssistantsFTEBarData: ChartWrapperData = {
        dataPoints: workforceData ? workforceData.results.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.teachingAssistantsFTE,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }) : new Array<ChartDataPoint>(),
        tableHeadings: tableHeadings
    }

    const nonClassroomSupportStaffFTEBarData: ChartWrapperData = {
        dataPoints: workforceData ? workforceData.results.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.nonClassroomSupportStaffFTE,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }) : new Array<ChartDataPoint>(),
        tableHeadings: tableHeadings
    }

    const auxiliaryStaffFTEBarData: ChartWrapperData = {
        dataPoints: workforceData ? workforceData.results.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.auxiliaryStaffFTE,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }) : new Array<ChartDataPoint>(),
        tableHeadings: tableHeadings
    }

    const schoolWorkforceHeadcountBarData: ChartWrapperData = {
        dataPoints: workforceData ? workforceData.results.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.schoolWorkforceHeadcount,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }) : new Array<ChartDataPoint>(),
        tableHeadings: tableHeadings
    }
    return (
        <SelectedSchoolContext.Provider value={selectedSchool}>
            <div className="govuk-grid-row">
                <div className="govuk-grid-column-two-thirds">
                    <p className="govuk-body"></p>
                </div>
                <div className="govuk-grid-column-one-third">
                    <ToggleChartMode displayMode={displayMode} handleChange={toggleChartMode}/>
                </div>
            </div>
            <ChartModeContext.Provider value={displayMode}>
                <ChartWrapper heading={<h3 className="govuk-heading-s">School workforce (Full Time Equivalent)</h3>}
                              data={schoolWorkforceBarData}
                              elementId="school-workforce"
                              chartDimensions={chartDimensions}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Total number of teachers (Full Time Equivalent)</h3>}
                              data={totalNumberOfTeachersFTEBarData}
                              elementId="total-teachers"
                              chartDimensions={chartDimensions}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Teachers with qualified Teacher Status (%)</h3>}
                              data={teachersWithQTSFTEBarData}
                              elementId="total-teachers-qualified"
                              chartDimensions={chartDimensions}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Senior Leadership (Full Time Equivalent)</h3>}
                              data={seniorLeadershipFTEBarData}
                              elementId="senior-leadership"
                              chartDimensions={chartDimensions}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Teaching Assistants (Full Time Equivalent)</h3>}
                              data={teachingAssistantsFTEBarData}
                              elementId="teaching-assistants"
                              chartDimensions={chartDimensions}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Non-classroom support staff - excluding auxiliary staff (Full Time Equivalent)</h3>}
                              data={nonClassroomSupportStaffFTEBarData}
                              elementId="teachers-qualified"
                              chartDimensions={chartDimensions}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Auxiliary staff (Full Time Equivalent)</h3>}
                              data={auxiliaryStaffFTEBarData}
                              elementId="auxiliary-staff"
                              chartDimensions={chartDimensions}
                />

                <ChartWrapper heading={<h3 className="govuk-heading-s">School workforce (headcount)</h3>}
                              data={schoolWorkforceHeadcountBarData}
                              elementId="headcount-data"
                              chartDimensions={chartDimensions}
                />
            </ChartModeContext.Provider>
        </SelectedSchoolContext.Provider>

    );
};

export default CompareYourWorkforce;
