import React from "react";
import HorizontalBarChart from "../horizontal-bar-chart/horizontal-bar-chart";

const TotalExpenditure: React.FC<SchoolTotalExpenditure> = ({urn, schools}) => {

    const barData = {
        labels: schools.map(result => result.name),
        data: schools.map(result => result.totalExpenditure)
    }

    const chosenSchoolName = schools.find(school => school.urn === urn)?.name || '';

    return (
        <div>
            <div className="govuk-grid-row">
                <div className="govuk-grid-column-two-thirds">
                    <h2 className="govuk-heading-l">Total Expenditure</h2>
                </div>
                <div className="govuk-grid-column-one-third">
                    <p className="govuk-body">[Save as image]</p>
                </div>
            </div>
            {schools.length > 0 &&
                <div className="govuk-grid-row">
                    <div className="govuk-grid-column-full">
                        <HorizontalBarChart data={barData} chosenSchool={chosenSchoolName} xLabel='per pupil'/>
                    </div>
                </div>
            }
        </div>
    )
};

export default TotalExpenditure

export type SchoolTotalExpenditure = {
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