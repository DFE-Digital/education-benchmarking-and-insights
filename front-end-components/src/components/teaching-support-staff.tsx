import React from "react";
import AccordionChartContent from "./accordion-chart-content";

const TeachingSupportStaff: React.FC<TeachingSupportStaffExpenditure> = ({urn, schools}) => {
    const labels = schools.map(result => result.name)

    const totalTeachingBarData = {
        labels: labels,
        data: schools.map(result => result.totalTeachingSupportStaffCosts)
    }

    const teachingStaffBarData = {
        labels: labels,
        data: schools.map(result => result.teachingStaffCosts)
    }

    const supplyTeachingBarData = {
        labels: labels,
        data: schools.map(result => result.supplyTeachingStaffCosts)
    }

    const educationalConsultancyBarData = {
        labels: labels,
        data: schools.map(result => result.educationalConsultancyCosts)
    }

    const educationSupportStaffBarData = {
        labels: labels,
        data: schools.map(result => result.educationSupportStaffCosts)
    }

    const agencySupplyBarData = {
        labels: labels,
        data: schools.map(result => result.agencySupplyTeachingStaffCosts)
    }

    const chosenSchoolName = schools.find(school => school.urn === urn)?.name || '';

    return (
        <div>
            <AccordionChartContent heading={'Total teaching and teaching support staff'}
                                   data={totalTeachingBarData}
                                   chosenSchoolName={chosenSchoolName}/>
            <AccordionChartContent heading={'Teaching staff costs'}
                                   data={teachingStaffBarData}
                                   chosenSchoolName={chosenSchoolName}/>
            <AccordionChartContent heading={'Supply teaching staff costs'}
                                   data={supplyTeachingBarData}
                                   chosenSchoolName={chosenSchoolName}/>
            <AccordionChartContent heading={'Educational consultancy costs'}
                                   data={educationalConsultancyBarData}
                                   chosenSchoolName={chosenSchoolName}/>
            <AccordionChartContent heading={'Educational support staff costs'}
                                   data={educationSupportStaffBarData}
                                   chosenSchoolName={chosenSchoolName}/>
            <AccordionChartContent heading={'Agency supply teaching staff costs'}
                                   data={agencySupplyBarData}
                                   chosenSchoolName={chosenSchoolName}/>
        </div>
    )
};

export default TeachingSupportStaff

export type TeachingSupportStaffExpenditure = {
    urn: string
    schools: TeachingSupportStaffExpenditureData[]
}

export type TeachingSupportStaffExpenditureData = {
    urn: string
    name: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
    totalTeachingSupportStaffCosts: number
    teachingStaffCosts: number
    supplyTeachingStaffCosts: number
    educationalConsultancyCosts: number
    educationSupportStaffCosts: number
    agencySupplyTeachingStaffCosts: number
}