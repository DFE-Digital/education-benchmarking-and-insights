import React from "react";
import AccordionChartContent from "./accordion-chart-content";

const NonEducationalSupportStaff: React.FC<NonEducationalSupportStaffExpenditure> = ({urn, schools}) => {
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
        <div>
            <AccordionChartContent heading={'Administrative and clerical staff costs'}
                                   data={administrativeClericalBarData}
                                   chosenSchoolName={chosenSchoolName}/>
            <AccordionChartContent heading={'Auditors costs'}
                                   data={auditorsCostsBarData}
                                   chosenSchoolName={chosenSchoolName}/>
            <AccordionChartContent heading={'Other staff costs'}
                                   data={otherStaffCostsBarData}
                                   chosenSchoolName={chosenSchoolName}/>
            <AccordionChartContent heading={'Professional services (non-curriculum) costs'}
                                   data={professionalServicesBarData}
                                   chosenSchoolName={chosenSchoolName}/>
        </div>
    )
};

export default NonEducationalSupportStaff

export type NonEducationalSupportStaffExpenditure = {
    urn: string
    schools: NonEducationalSupportStaffExpenditureData[]
}

export type NonEducationalSupportStaffExpenditureData = {
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