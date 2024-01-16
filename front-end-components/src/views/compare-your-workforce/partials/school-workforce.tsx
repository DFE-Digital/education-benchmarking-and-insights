import React, {useState} from "react";
import {
    CalculateWorkforceValue,
    ChartDimensions, DimensionHeading,
    HeadcountPerFTE,
    HorizontalBarChartWrapper, HorizontalBarChartWrapperData,
    PercentageOfWorkforce, Total,
    WorkforceCategories
} from "src/components";
import {ChartDimensionContext} from 'src/contexts'
import {SchoolWorkforceProps} from "src/views/compare-your-workforce/partials";

export const SchoolWorkforce: React.FC<SchoolWorkforceProps> = (props) => {
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
                    value: school.schoolWorkforceFTE,
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
            <HorizontalBarChartWrapper data={chartData} chartId="school-workforce">
                <h3 className="govuk-heading-s">School workforce (Full Time Equivalent)</h3>
                <ChartDimensions dimensions={
                    WorkforceCategories.filter(function (category) {
                        return category !== PercentageOfWorkforce && category !== HeadcountPerFTE
                    })}
                                 handleChange={handleSelectChange}
                                 elementId="school-workforce"
                                 defaultValue={dimension}/>
            </HorizontalBarChartWrapper>
        </ChartDimensionContext.Provider>
    )
};

