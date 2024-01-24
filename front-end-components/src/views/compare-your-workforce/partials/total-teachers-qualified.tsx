import React from "react";
import {HorizontalBarChartWrapper, HorizontalBarChartWrapperData} from "src/components";
import {ChartDimensionContext} from 'src/contexts'
import {ChartDimensions, WorkforceCategories} from "src/components";
import {useState} from "react";
import {TotalTeachersQualifiedProps} from "src/views/compare-your-workforce/partials";

export const TotalTeachersQualified: React.FC<TotalTeachersQualifiedProps> = (props) => {
    const {schools} = props
    const tableHeadings = ["School name", "Local Authority", "School type", "Number of pupils", "Percent"]
    const [dimension, setDimension] = useState(WorkforceCategories[0]);


    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value);
    };

    const chartData: HorizontalBarChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: school.teachersWithQTSFTE,
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    return (
        <ChartDimensionContext.Provider value={dimension}>
    <HorizontalBarChartWrapper data={chartData} chartId="total-teachers-qualified">
        <h2 className="govuk-heading-m">Teachers with qualified teacher status (Full Time Equivalent)</h2>
        <ChartDimensions dimensions={WorkforceCategories}
                         handleChange={handleSelectChange}
                         elementId="total-teachers-qualified"
                         defaultValue={dimension}/>
    </HorizontalBarChartWrapper>
</ChartDimensionContext.Provider>
    )
};

