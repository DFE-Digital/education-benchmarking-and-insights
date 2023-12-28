import React, {useState} from "react";
import ChartWrapper from "../chart-wrapper";
import {CalculateCostValue, CostCategories, PoundsPerPupil} from "../../chart-dimensions";

const TotalExpenditure: React.FC<TotalExpenditureProps> = ({urn, schools}) => {
    const [dimension, setDimension] = useState(PoundsPerPupil)

    const barData = {
        labels: schools.map(result => result.name),
        data: schools.map(result => CalculateCostValue({
            dimension: dimension,
            value: result.totalExpenditure,
            ...result
        }))
    }

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    const chosenSchoolName = schools.find(school => school.urn === urn)?.name || '';
    const chartDimensions = {dimensions: CostCategories, handleChange: handleSelectChange}

    return (
        <ChartWrapper heading={<h2 className="govuk-heading-l">Total Expenditure</h2>}
                      data={barData}
                      chosenSchoolName={chosenSchoolName}
                      fileName="total-expenditure"
                      chartDimensions={chartDimensions}
                      selectedDimension={dimension}
        />
    )
};

export default TotalExpenditure

export type TotalExpenditureProps = {
    urn: string
    schools: TotalExpenditureData[]
}

export type TotalExpenditureData = {
    urn: string
    name: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
}