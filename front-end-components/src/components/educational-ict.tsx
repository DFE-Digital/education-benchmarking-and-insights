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
        <div className="govuk-accordion__section">
            <div className="govuk-accordion__section-header">
                <h2 className="govuk-accordion__section-heading">
                        <span className="govuk-accordion__section-button" id="accordion-heading-educational-ict">
                            Educational ICT
                        </span>
                </h2>
            </div>
            <div id="accordion-content-educational-ict" className="govuk-accordion__section-content"
                 aria-labelledby="accordion-heading-educational-ict">
                <AccordionChartContent heading={'Educational learning resources costs'}
                                       data={learningResourcesBarData}
                                       chosenSchoolName={chosenSchoolName}/>
            </div>
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