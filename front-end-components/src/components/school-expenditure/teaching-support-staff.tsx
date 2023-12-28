import React, {useState} from "react";
import ChartWrapper from "../chart-wrapper";
import {CalculateCostValue, CostCategories, PoundsPerPupil} from "../../chart-dimensions";

const TeachingSupportStaff: React.FC<TeachingSupportStaffProps> = ({urn, schools}) => {
    const labels = schools.map(result => result.name)
    const [dimension, setDimension] = useState(PoundsPerPupil)

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    const chartDimensions = {dimensions: CostCategories, handleChange: handleSelectChange}

    const totalTeachingBarData = {
        labels: labels,
        data: schools.map(result => CalculateCostValue({
            dimension: dimension,
            value: result.totalTeachingSupportStaffCosts,
            ...result
        }))
    }

    const teachingStaffBarData = {
        labels: labels,
        data: schools.map(result => CalculateCostValue({
            dimension: dimension,
            value: result.teachingStaffCosts,
            ...result
        }))
    }

    const supplyTeachingBarData = {
        labels: labels,
        data: schools.map(result => CalculateCostValue({
            dimension: dimension,
            value: result.supplyTeachingStaffCosts,
            ...result
        }))
    }

    const educationalConsultancyBarData = {
        labels: labels,
        data: schools.map(result => CalculateCostValue({
            dimension: dimension,
            value: result.educationalConsultancyCosts,
            ...result
        }))
    }

    const educationSupportStaffBarData = {
        labels: labels,
        data: schools.map(result => CalculateCostValue({
            dimension: dimension,
            value: result.educationSupportStaffCosts,
            ...result
        }))
    }

    const agencySupplyBarData = {
        labels: labels,
        data: schools.map(result => CalculateCostValue({
            dimension: dimension,
            value: result.agencySupplyTeachingStaffCosts,
            ...result
        }))
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
                              fileName="total-teaching-support-staff-cost"
                              chartDimensions={chartDimensions}
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Teaching staff costs</h3>}
                              data={teachingStaffBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="teaching-staff-costs"
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Supply teaching staff costs</h3>}
                              data={supplyTeachingBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="supply-teaching-staff-costs"
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Educational consultancy costs</h3>}
                              data={educationalConsultancyBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="educational-consultancy-costs"
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Educational support staff costs</h3>}
                              data={educationSupportStaffBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="education-support-stff-costs"
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Agency supply teaching staff costs</h3>}
                              data={agencySupplyBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="agency-supply-teaching-staff-costs"
                              selectedDimension={dimension}
                />
            </div>
        </div>
    )
};

export default TeachingSupportStaff

export type TeachingSupportStaffProps = {
    urn: string
    schools: TeachingSupportStaffData[]
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