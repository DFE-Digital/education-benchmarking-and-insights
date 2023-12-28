import React, {useState} from "react";
import ChartWrapper from "../chart-wrapper";
import {CalculateCostValue, CostCategories, DimensionHeading, PoundsPerPupil} from "../../chart-dimensions";
import {ChartDimensionContext} from "../../contexts";
import {ChartWrapperData} from "../../types";

const TeachingSupportStaff: React.FC<TeachingSupportStaffProps> = ({urn, schools}) => {
    const [dimension, setDimension] = useState(PoundsPerPupil)
    const tableHeadings = ["School name", "Local Authority", "School type", "Number of pupils", DimensionHeading(dimension)]

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    const chartDimensions = {dimensions: CostCategories, handleChange: handleSelectChange}

    const totalTeachingBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.totalTeachingSupportStaffCosts,
                    ...school
                }),
                additionalData: ["", "", school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const teachingStaffBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.teachingStaffCosts,
                    ...school
                }),
                additionalData: ["", "", school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const supplyTeachingBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.supplyTeachingStaffCosts,
                    ...school
                }),
                additionalData: ["", "", school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const educationalConsultancyBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.educationalConsultancyCosts,
                    ...school
                }),
                additionalData: ["", "", school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const educationSupportStaffBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.educationSupportStaffCosts,
                    ...school
                }),
                additionalData: ["", "", school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const agencySupplyBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.agencySupplyTeachingStaffCosts,
                    ...school
                }),
                additionalData: ["", "", school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const chosenSchoolName = schools.find(school => school.urn === urn)?.name || '';

    return (
        <ChartDimensionContext.Provider value={dimension}>
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
                    <ChartWrapper
                        heading={<h3 className="govuk-heading-s">Total teaching and teaching support staff</h3>}
                        data={totalTeachingBarData}
                        chosenSchoolName={chosenSchoolName}
                        fileName="total-teaching-support-staff-cost"
                        chartDimensions={chartDimensions}
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Teaching staff costs</h3>}
                                  data={teachingStaffBarData}
                                  chosenSchoolName={chosenSchoolName}
                                  fileName="teaching-staff-costs"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Supply teaching staff costs</h3>}
                                  data={supplyTeachingBarData}
                                  chosenSchoolName={chosenSchoolName}
                                  fileName="supply-teaching-staff-costs"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Educational consultancy costs</h3>}
                                  data={educationalConsultancyBarData}
                                  chosenSchoolName={chosenSchoolName}
                                  fileName="educational-consultancy-costs"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Educational support staff costs</h3>}
                                  data={educationSupportStaffBarData}
                                  chosenSchoolName={chosenSchoolName}
                                  fileName="education-support-stff-costs"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Agency supply teaching staff costs</h3>}
                                  data={agencySupplyBarData}
                                  chosenSchoolName={chosenSchoolName}
                                  fileName="agency-supply-teaching-staff-costs"
                    />
                </div>
            </div>
        </ChartDimensionContext.Provider>
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