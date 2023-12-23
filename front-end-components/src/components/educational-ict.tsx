import React from "react";
import AccordionChartContent from "./accordion-chart-content";

const EducationalIct: React.FC<EducationalIctExpenditure> = ({urn, schools}) => {
    const labels = schools.map(result => result.name)

    const learningResourcesBarData = {
        labels: labels,
        data: schools.map(result => result.learningResourcesIctCosts)
    }


    const chosenSchoolName = schools.find(school => school.urn === urn)?.name || '';

    return (
        <div>
            <AccordionChartContent heading={'Educational learning resources costs'}
                                   data={learningResourcesBarData}
                                   chosenSchoolName={chosenSchoolName}/>
        </div>
    )
};

export default EducationalIct

export type EducationalIctExpenditure = {
    urn: string
    schools: EducationalIctExpenditureData[]
}

export type EducationalIctExpenditureData = {
    urn: string
    name: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
    learningResourcesIctCosts: number
}