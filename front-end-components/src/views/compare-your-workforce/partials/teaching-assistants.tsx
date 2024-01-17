import React, {useState} from "react";
import {
    CalculateWorkforceValue,
    ChartDimensions, DimensionHeading,
    HorizontalBarChartWrapper,
    HorizontalBarChartWrapperData, Total,
    WorkforceCategories
} from "src/components";
import {ChartDimensionContext} from 'src/contexts'
import {TeachingAssistantsProps} from "src/views/compare-your-workforce/partials";

export const TeachingAssistants: React.FC<TeachingAssistantsProps> = (props) => {
    const {schools} = props
    const [dimension, setDimension] = useState(Total)
    const tableHeadings = ["School name", "Local Authority", "School type", "Number of pupils", DimensionHeading(dimension)]

    const chartData: HorizontalBarChartWrapperData = {
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

    return (
        <ChartDimensionContext.Provider value={dimension}>
            <HorizontalBarChartWrapper data={chartData} chartId="teaching-assistants">
                <h2 className="govuk-heading-m">Teaching Assistants (Full Time Equivalent)</h2>
                <ChartDimensions dimensions={WorkforceCategories}
                                 handleChange={handleSelectChange}
                                 elementId="teaching-assistants"
                                 defaultValue={dimension}/>
            </HorizontalBarChartWrapper>
        </ChartDimensionContext.Provider>
    )
};

