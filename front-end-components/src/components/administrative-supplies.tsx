import React from "react";
import AccordionChartContent from "./accordion-chart-content";

const AdministrativeSupplies: React.FC<AdministrativeSuppliesExpenditure> = ({urn, schools}) => {
    const labels = schools.map(result => result.name)

    const administrativeSuppliesBarData = {
        labels: labels,
        data: schools.map(result => result.administrativeSuppliesCosts)
    }


    const chosenSchoolName = schools.find(school => school.urn === urn)?.name || '';

    return (
        <div className="govuk-accordion__section">
            <div className="govuk-accordion__section-header">
                <h2 className="govuk-accordion__section-heading">
                        <span className="govuk-accordion__section-button"
                              id="accordion-heading-administrative-supplies">
                            Administrative supplies
                        </span>
                </h2>
            </div>
            <div id="accordion-content-administrative-supplies" className="govuk-accordion__section-content"
                 aria-labelledby="accordion-heading-administrative-supplies">
                <AccordionChartContent heading={'Administrative supplies (Non-educational)'}
                                       data={administrativeSuppliesBarData}
                                       chosenSchoolName={chosenSchoolName}/>
            </div>
        </div>
    )
};

export default AdministrativeSupplies

export type AdministrativeSuppliesExpenditure = {
    urn: string
    schools: AdministrativeSuppliesExpenditureData[]
}

export type AdministrativeSuppliesExpenditureData = {
    urn: string
    name: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
    administrativeSuppliesCosts: number
}