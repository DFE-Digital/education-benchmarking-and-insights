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
        <div>
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