import React from "react";
import ChartWrapper from "../chart-wrapper";
import {ChartMode} from "../../constants";

const EducationalSupplies: React.FC<EducationalSuppliesProps> = ({urn, schools, mode}) => {
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
                <ChartWrapper heading={<h3 className="govuk-heading-s">Examination fees costs</h3>}
                              data={examinationFeesBarData}
                              chosenSchoolName={chosenSchoolName}
                              mode={mode}/>
                <ChartWrapper heading={<h3 className="govuk-heading-s">Breakdown of educational supplies costs</h3>}
                              data={breakdownEducationalBarData}
                              chosenSchoolName={chosenSchoolName}
                              mode={mode}/>
                <ChartWrapper heading={<h3 className="govuk-heading-s">Learning resources (not ICT equipment) costs</h3>}
                              data={learningResourcesBarData}
                              chosenSchoolName={chosenSchoolName}
                              mode={mode}/>
            </div>
        </div>
    )
};

export default EducationalSupplies

export type EducationalSuppliesProps = {
    urn: string
    schools: EducationalSuppliesData[]
    mode: ChartMode
}

export type EducationalSuppliesData = {
    urn: string
    name: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
    examinationFeesCosts: number
    breakdownEducationalSuppliesCosts: number
    learningResourcesNonIctCosts: number
}