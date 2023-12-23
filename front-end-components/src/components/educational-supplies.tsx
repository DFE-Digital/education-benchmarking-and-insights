import React from "react";
import AccordionChartContent from "./accordion-chart-content";

const EducationalSupplies: React.FC<EducationalSuppliesExpenditure> = ({urn, schools}) => {
    const labels = schools.map(result => result.name)

    const examinationFeesBarData = {
        labels: labels,
        data: schools.map(result => result.examinationFeesCosts)
    }

    const breakdownEducationalBarData = {
        labels: labels,
        data: schools.map(result => result.breakdownEducationalSuppliesCosts)
    }

    const learningResourcesBarData = {
        labels: labels,
        data: schools.map(result => result.learningResourcesNonIctCosts)
    }


    const chosenSchoolName = schools.find(school => school.urn === urn)?.name || '';

    return (
        <div className="govuk-accordion__section">
            <div className="govuk-accordion__section-header">
                <h2 className="govuk-accordion__section-heading">
                        <span className="govuk-accordion__section-button" id="accordion-heading-educational-supplies">
                            Educational supplies
                        </span>
                </h2>
            </div>
            <div id="accordion-content-educational-supplies" className="govuk-accordion__section-content"
                 aria-labelledby="accordion-heading-educational-supplies">
                <AccordionChartContent heading={'Examination fees costs'}
                                       data={examinationFeesBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'Breakdown of educational supplies costs'}
                                       data={breakdownEducationalBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'Learning resources (not ICT equipment) costs'}
                                       data={learningResourcesBarData}
                                       chosenSchoolName={chosenSchoolName}/>
            </div>
        </div>
    )
};

export default EducationalSupplies

export type EducationalSuppliesExpenditure = {
    urn: string
    schools: EducationalSuppliesExpenditureData[]
}

export type EducationalSuppliesExpenditureData = {
    urn: string
    name: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
    examinationFeesCosts: number
    breakdownEducationalSuppliesCosts: number
    learningResourcesNonIctCosts: number
}