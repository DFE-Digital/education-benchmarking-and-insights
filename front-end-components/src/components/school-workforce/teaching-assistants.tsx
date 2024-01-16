import React, {useState} from "react";
import ChartWrapper from "../chart-wrapper";
import {
    CalculateWorkforceValue,
    DimensionHeading,
    Total, WorkforceCategories
} from "../../chart-dimensions";
import {ChartDimensionContext} from "../../contexts";
import {ChartWrapperData} from "../../types";

const TeachingAssistants: React.FC<TeachingAssistantsProps> = (props) => {
    const {schools} = props
    const [dimension, setDimension] = useState(Total)
    const tableHeadings = ["School name", "Local Authority", "School type", "Number of pupils", DimensionHeading(dimension)]

    const chartData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateWorkforceValue({
                    dimension: dimension,
                    value: school.teachingAssistantsFTE,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    const chartDimensions = {dimensions: WorkforceCategories, handleChange: handleSelectChange}

    return (
        <ChartDimensionContext.Provider value={dimension}>
            <ChartWrapper heading={<h3 className="govuk-heading-s">Teaching Assistants (Full Time Equivalent)</h3>}
                          data={chartData}
                          elementId="teaching-assistants"
                          chartDimensions={chartDimensions}
            />
        </ChartDimensionContext.Provider>
    )
};

export default TeachingAssistants

export type TeachingAssistantsProps = {
    schools: TeachingAssistantsData[]
}

export type TeachingAssistantsData = {
    urn: string
    name: string
    schoolType: string
    localAuthority: string
    numberOfPupils: bigint
    schoolWorkforceFTE: number
    teachingAssistantsFTE : number
}