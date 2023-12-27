import React from "react";
import ChartWrapper from "../chart-wrapper";

const PremisesStaffServices: React.FC<PremisesStaffServicesProps> = ({urn, schools}) => {
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
                <ChartWrapper heading={<h3 className="govuk-heading-s">Cleaning and caretaking costs</h3>}
                              data={cleaningCaretakingBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="cleaning-caretaking-costs"/>
                <ChartWrapper heading={<h3 className="govuk-heading-s">Maintenance of premises costs</h3>}
                              data={maintenanceBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="maintenance-premises-costs"/>
                <ChartWrapper heading={<h3 className="govuk-heading-s">Other occupation costs</h3>}
                              data={otherOccupationBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="other-occupation-costs"/>
                <ChartWrapper heading={<h3 className="govuk-heading-s">Premises staff costs</h3>}
                              data={premisesStaffBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="premises staff costs"/>
            </div>
        </div>
    )
};

export default PremisesStaffServices

export type PremisesStaffServicesProps = {
    urn: string
    schools: PremisesStaffServicesData[]
}

export type PremisesStaffServicesData = {
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