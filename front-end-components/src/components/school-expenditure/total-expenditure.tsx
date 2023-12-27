import React from "react";
import ChartWrapper from "../chart-wrapper";

const TotalExpenditure: React.FC<TotalExpenditureProps> = ({urn, schools}) => {

    const barData = {
        labels: schools.map(result => result.name),
        data: schools.map(result => result.totalExpenditure)
    }

    const chosenSchoolName = schools.find(school => school.urn === urn)?.name || '';

    return (
            <ChartWrapper heading={<h2 className="govuk-heading-l">Total Expenditure</h2>}
                          data={barData}
                          chosenSchoolName={chosenSchoolName}
                          fileName="total-expenditure"
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