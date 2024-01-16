import React, {useState} from "react";
import ChartWrapper from "../chart-wrapper";
import {
    CalculateWorkforceValue,
    DimensionHeading,
    Total, WorkforceCategories
} from "../../chart-dimensions";
import {ChartDimensionContext} from "../../contexts";
import {ChartWrapperData} from "../../types";

const Headcount: React.FC<HeadcountProps> = (props) => {
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
                    value: school.schoolWorkforceHeadcount,
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
            <ChartWrapper heading={<h3 className="govuk-heading-s">School workforce (headcount)</h3>}
                          data={chartData}
                          elementId="headcount"
                          chartDimensions={chartDimensions}
            />
        </ChartDimensionContext.Provider>
    )
};

export default Headcount

export type HeadcountProps = {
    schools: HeadcountData[]
}

export type HeadcountData = {
    urn: string
    name: string
    schoolType: string
    localAuthority: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
    schoolWorkforceHeadcount : number
}