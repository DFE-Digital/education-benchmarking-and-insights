import React from "react";
import ChartWrapper from "../chart-wrapper";
import {ChartMode} from "../../constants";

const EducationalIct: React.FC<EducationalIctProps> = ({urn, schools,mode}) => {
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
                <ChartWrapper heading={<h3 className="govuk-heading-s">Educational learning resources costs</h3>}
                              data={learningResourcesBarData}
                              chosenSchoolName={chosenSchoolName}
                              mode={mode} fileName="eductional-learning-resources-costs"/>
            </div>
        </div>
    )
};

export default EducationalIct

export type EducationalIctProps = {
    urn: string
    schools: EducationalIctData[]
    mode: ChartMode
}

export type EducationalIctData = {
    urn: string
    name: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
    learningResourcesIctCosts: number
}