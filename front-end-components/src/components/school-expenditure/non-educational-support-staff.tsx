import React from "react";
import ChartWrapper from "../chart-wrapper";
import {ChartMode} from "../../constants";

const NonEducationalSupportStaff: React.FC<NonEducationalSupportStaffProps> = ({urn, schools, mode}) => {
    const labels = schools.map(result => result.name)

    const administrativeClericalBarData = {
        labels: labels,
        data: schools.map(result => result.administrativeClericalStaffCosts)
    }

    const auditorsCostsBarData = {
        labels: labels,
        data: schools.map(result => result.auditorsCosts)
    }

    const otherStaffCostsBarData = {
        labels: labels,
        data: schools.map(result => result.otherStaffCosts)
    }

    const professionalServicesBarData = {
        labels: labels,
        data: schools.map(result => result.professionalServicesNonCurriculumCosts)
    }

    const chosenSchoolName = schools.find(school => school.urn === urn)?.name || '';

    return (
        <div className="govuk-accordion__section">
            <div className="govuk-accordion__section-header">
                <h2 className="govuk-accordion__section-heading">
                        <span className="govuk-accordion__section-button"
                              id="accordion-heading-non-educational-support-staff">
                            Non-educational support staff
                        </span>
                </h2>
            </div>
            <div id="accordion-content-non-educational-support-staff"
                 className="govuk-accordion__section-content"
                 aria-labelledby="accordion-heading-non-educational-support-staff">
                <ChartWrapper heading={<h3 className="govuk-heading-s">Administrative and clerical staff costs</h3>}
                              data={administrativeClericalBarData}
                              chosenSchoolName={chosenSchoolName}
                              mode={mode}/>
                <ChartWrapper heading={<h3 className="govuk-heading-s">Auditors costs</h3>}
                              data={auditorsCostsBarData}
                              chosenSchoolName={chosenSchoolName}
                              mode={mode}/>
                <ChartWrapper heading={<h3 className="govuk-heading-s">Other staff costs</h3>}
                              data={otherStaffCostsBarData}
                              chosenSchoolName={chosenSchoolName}
                              mode={mode}/>
                <ChartWrapper heading={<h3 className="govuk-heading-s">Professional services (non-curriculum) costs</h3>}
                              data={professionalServicesBarData}
                              chosenSchoolName={chosenSchoolName}
                              mode={mode}/>
            </div>
        </div>
    )
};

export default NonEducationalSupportStaff

export type NonEducationalSupportStaffProps = {
    urn: string
    schools: NonEducationalSupportStaffData[]
    mode: ChartMode
}

export type NonEducationalSupportStaffData = {
    urn: string
    name: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
    administrativeClericalStaffCosts: number
    auditorsCosts: number
    otherStaffCosts: number
    professionalServicesNonCurriculumCosts: number
}