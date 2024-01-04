import React, {useState} from "react";
import ChartWrapper from "../chart-wrapper";
import {CalculateCostValue, CostCategories, DimensionHeading, PoundsPerPupil} from "../../chart-dimensions";
import {ChartDimensionContext} from "../../contexts";
import {ChartWrapperData} from "../../types";

const NonEducationalSupportStaff: React.FC<NonEducationalSupportStaffProps> = ({schools}) => {
    const [dimension, setDimension] = useState(PoundsPerPupil)
    const tableHeadings = ["School name", "Local Authority", "School type", "Number of pupils", DimensionHeading(dimension)]

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    const chartDimensions = {dimensions: CostCategories, handleChange: handleSelectChange}

    const administrativeClericalBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.administrativeClericalStaffCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }


    const totalNonEducationalBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.totalNonEducationalSupportStaffCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const auditorsCostsBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.auditorsCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const otherStaffCostsBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.otherStaffCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const professionalServicesBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.professionalServicesNonCurriculumCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    return (
        <ChartDimensionContext.Provider value={dimension}>
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
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Total non-educational support staff costs</h3>}
                                  data={totalNonEducationalBarData}
                                  elementId="total-non-educational-support-staff-costs"
                                  chartDimensions={chartDimensions}
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Administrative and clerical staff costs</h3>}
                                  data={administrativeClericalBarData}
                                  elementId="administrative-clerical-staff-costs"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Auditors costs</h3>}
                                  data={auditorsCostsBarData}
                                  elementId="Auditors costs"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Other staff costs</h3>}
                                  data={otherStaffCostsBarData}
                                  elementId="Other staff costs"
                    />
                    <ChartWrapper
                        heading={<h3 className="govuk-heading-s">Professional services (non-curriculum) costs</h3>}
                        data={professionalServicesBarData}
                        elementId="profession-services-non-curriculum-costs"
                    />
                </div>
            </div>
        </ChartDimensionContext.Provider>
    )
};

export default NonEducationalSupportStaff

export type NonEducationalSupportStaffProps = {
    schools: NonEducationalSupportStaffData[]
}

export type NonEducationalSupportStaffData = {
    urn: string
    name: string
    schoolType: string
    localAuthority: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
    totalNonEducationalSupportStaffCosts: number
    administrativeClericalStaffCosts: number
    auditorsCosts: number
    otherStaffCosts: number
    professionalServicesNonCurriculumCosts: number
}