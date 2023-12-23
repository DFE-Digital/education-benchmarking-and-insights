import React from "react";
import AccordionChartContent from "./accordion-chart-content";

const PremisesStaffServices: React.FC<PremisesStaffServicesExpenditure> = ({urn, schools}) => {
    const labels = schools.map(result => result.name)

    const cleaningCaretakingBarData = {
        labels: labels,
        data: schools.map(result => result.cleaningCaretakingCosts)
    }

    const maintenanceBarData = {
        labels: labels,
        data: schools.map(result => result.maintenancePremisesCosts)
    }

    const otherOccupationBarData = {
        labels: labels,
        data: schools.map(result => result.otherOccupationCosts)
    }

    const premisesStaffBarData = {
        labels: labels,
        data: schools.map(result => result.premisesStaffCosts)
    }

    const chosenSchoolName = schools.find(school => school.urn === urn)?.name || '';

    return (
        <div className="govuk-accordion__section">
            <div className="govuk-accordion__section-header">
                <h2 className="govuk-accordion__section-heading">
                        <span className="govuk-accordion__section-button"
                              id="accordion-heading-premises-staff-services">
                            Premises staff and services
                        </span>
                </h2>
            </div>
            <div id="accordion-content-premises-staff-services" className="govuk-accordion__section-content"
                 aria-labelledby="accordion-heading-premises-staff-services">
                <AccordionChartContent heading={'Cleaning and caretaking costs'}
                                       data={cleaningCaretakingBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'Maintenance of premises costs'}
                                       data={maintenanceBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'Other occupation costs'}
                                       data={otherOccupationBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'Premises staff costs'}
                                       data={premisesStaffBarData}
                                       chosenSchoolName={chosenSchoolName}/>
            </div>
        </div>
    )
};

export default PremisesStaffServices

export type PremisesStaffServicesExpenditure = {
    urn: string
    schools: PremisesStaffServicesExpenditureData[]
}

export type PremisesStaffServicesExpenditureData = {
    urn: string
    name: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
    cleaningCaretakingCosts: number
    maintenancePremisesCosts: number
    otherOccupationCosts: number
    premisesStaffCosts: number
}