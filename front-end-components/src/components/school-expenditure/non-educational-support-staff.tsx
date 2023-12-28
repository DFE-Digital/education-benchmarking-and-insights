import React, {useState} from "react";
import ChartWrapper from "../chart-wrapper";
import {CalculateCostValue, CostCategories, PoundsPerPupil} from "../../chart-dimensions";

const NonEducationalSupportStaff: React.FC<NonEducationalSupportStaffProps> = ({urn, schools}) => {
    const labels = schools.map(result => result.name)
    const [dimension, setDimension] = useState(PoundsPerPupil)

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    const chartDimensions = {dimensions: CostCategories, handleChange: handleSelectChange}

    const administrativeClericalBarData = {
        labels: labels,
        data: schools.map(result => CalculateCostValue({
            dimension: dimension,
            value: result.administrativeClericalStaffCosts,
            ...result
        }))
    }

    const auditorsCostsBarData = {
        labels: labels,
        data: schools.map(result => CalculateCostValue({
            dimension: dimension,
            value: result.auditorsCosts,
            ...result
        }))
    }

    const otherStaffCostsBarData = {
        labels: labels,
        data: schools.map(result => CalculateCostValue({
            dimension: dimension,
            value: result.otherStaffCosts,
            ...result
        }))
    }

    const professionalServicesBarData = {
        labels: labels,
        data: schools.map(result => CalculateCostValue({
            dimension: dimension,
            value: result.professionalServicesNonCurriculumCosts,
            ...result
        }))
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
                              fileName="administrative-clerical-staff-costs"
                              chartDimensions={chartDimensions}
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Auditors costs</h3>}
                              data={auditorsCostsBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="Auditors costs"
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Other staff costs</h3>}
                              data={otherStaffCostsBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="Other staff costs"
                              selectedDimension={dimension}
                />
                <ChartWrapper
                    heading={<h3 className="govuk-heading-s">Professional services (non-curriculum) costs</h3>}
                    data={professionalServicesBarData}
                    chosenSchoolName={chosenSchoolName}
                    fileName="profession-services-non-curriculum-costs"
                    selectedDimension={dimension}
                />
            </div>
        </div>
    )
};

export default NonEducationalSupportStaff

export type NonEducationalSupportStaffProps = {
    urn: string
    schools: NonEducationalSupportStaffData[]
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