import React from "react";
import ChartWrapper from "../chart-wrapper";
import {ChartMode} from "../../constants";

const TeachingSupportStaff: React.FC<TeachingSupportStaffProps> = ({urn, schools, mode}) => {
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
        <div className="govuk-accordion__section">
            <div className="govuk-accordion__section-header">
                <h2 className="govuk-accordion__section-heading">
                        <span className="govuk-accordion__section-button" id="accordion-heading-teaching-support-staff">
                            Teaching and teaching support staff
                        </span>
                </h2>
            </div>
            <div id="accordion-content-teaching-support-staff" className="govuk-accordion__section-content"
                 aria-labelledby="accordion-heading-teaching-support-staff">
                <ChartWrapper heading={<h3 className="govuk-heading-s">Total teaching and teaching support staff</h3>}
                              data={totalTeachingBarData}
                              chosenSchoolName={chosenSchoolName}
                              mode={mode} fileName="total-teaching-support-staff-cost"/>
                <ChartWrapper heading={<h3 className="govuk-heading-s">Teaching staff costs</h3>}
                              data={teachingStaffBarData}
                              chosenSchoolName={chosenSchoolName}
                              mode={mode} fileName="teaching-staff-costs"/>
                <ChartWrapper heading={<h3 className="govuk-heading-s">Supply teaching staff costs</h3>}
                              data={supplyTeachingBarData}
                              chosenSchoolName={chosenSchoolName}
                              mode={mode} fileName="supply-teaching-staff-costs"/>
                <ChartWrapper heading={<h3 className="govuk-heading-s">Educational consultancy costs</h3>}
                              data={educationalConsultancyBarData}
                              chosenSchoolName={chosenSchoolName}
                              mode={mode} fileName="educational-consultancy-costs"/>
                <ChartWrapper heading={<h3 className="govuk-heading-s">Educational support staff costs</h3>}
                              data={educationSupportStaffBarData}
                              chosenSchoolName={chosenSchoolName}
                              mode={mode} fileName="education-support-stff-costs"/>
                <ChartWrapper heading={<h3 className="govuk-heading-s">Agency supply teaching staff costs</h3>}
                              data={agencySupplyBarData}
                              chosenSchoolName={chosenSchoolName}
                              mode={mode} fileName="agency-supply-teaching-staff-costs"/>
            </div>
        </div>
    )
};

export default TeachingSupportStaff

export type TeachingSupportStaffProps = {
    urn: string
    schools: TeachingSupportStaffData[]
    mode: ChartMode
}

export type TeachingSupportStaffData = {
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