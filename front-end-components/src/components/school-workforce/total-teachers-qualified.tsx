import React, {useState} from "react";
import ChartWrapper from "../chart-wrapper";
import {
    CalculateWorkforceValue,
    DimensionHeading,
    Total, WorkforceCategories
} from "../../chart-dimensions";
import {ChartDimensionContext} from "../../contexts";
import {ChartWrapperData} from "../../types";

const TotalTeachersQualified: React.FC<TotalTeachersQualifiedProps> = (props) => {
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
                    value: school.teachersWithQTSFTE,
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
            <ChartWrapper heading={<h3 className="govuk-heading-s">Teachers with qualified Teacher Status (%)</h3>}
                          data={chartData}
                          elementId="total-teachers-qualified"
                          chartDimensions={chartDimensions}
            />
        </ChartDimensionContext.Provider>
    )
};

export default TotalTeachersQualified

export type TotalTeachersQualifiedProps = {
    schools: TotalTeachersQualifiedData[]
}

export type TotalTeachersQualifiedData = {
    urn: string
    name: string
    schoolType: string
    localAuthority: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
    teachersWithQTSFTE : number
}