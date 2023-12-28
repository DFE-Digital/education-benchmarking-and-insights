import React, {useState} from "react";
import ChartWrapper from "../chart-wrapper";
import {CalculateCostValue, CostCategories, DimensionHeading, PoundsPerPupil} from "../../chart-dimensions";
import {ChartDimensionContext} from "../../contexts";
import {ChartWrapperData} from "../../types";

const TotalExpenditure: React.FC<TotalExpenditureProps> = ({ schools}) => {

    const [dimension, setDimension] = useState(PoundsPerPupil)
    const tableHeadings = ["School name", "Local Authority", "School type", "Number of pupils", DimensionHeading(dimension)]

    const chartData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.totalExpenditure,
                    ...school
                }),
                additionalData: ["", "", school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }


    const chartDimensions = {dimensions: CostCategories, handleChange: handleSelectChange}

    return (
        <ChartDimensionContext.Provider value={dimension}>
            <ChartWrapper heading={<h2 className="govuk-heading-l">Total Expenditure</h2>}
                          data={chartData}
                          fileName="total-expenditure"
                          chartDimensions={chartDimensions}
            />
        </ChartDimensionContext.Provider>
    )
};

export default TotalExpenditure

export type TotalExpenditureProps = {
    schools: TotalExpenditureData[]
}

export type TotalExpenditureData = {
    urn: string
    name: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
}