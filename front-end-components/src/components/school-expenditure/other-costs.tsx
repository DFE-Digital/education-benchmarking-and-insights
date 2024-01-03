import React, {useState} from "react";
import ChartWrapper from "../chart-wrapper";
import {CalculateCostValue, CostCategories, DimensionHeading, PoundsPerPupil} from "../../chart-dimensions";
import {ChartDimensionContext} from "../../contexts";
import {ChartWrapperData} from "../../types";

const OtherCosts: React.FC<OtherCostsProps> = ({schools}) => {
    const [dimension, setDimension] = useState(PoundsPerPupil)
    const tableHeadings = ["School name", "Local Authority", "School type", "Number of pupils", DimensionHeading(dimension)]

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    const chartDimensions = {dimensions: CostCategories, handleChange: handleSelectChange}

    const totalOtherCostsBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.totalOtherCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const otherInsurancePremiumsCostsBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.otherInsurancePremiumsCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const directRevenueFinancingCostsBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.directRevenueFinancingCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const groundsMaintenanceCostsBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.groundsMaintenanceCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const indirectEmployeeExpensesBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.indirectEmployeeExpenses,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const interestChargesLoanBankBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.interestChargesLoanBank,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const privateFinanceInitiativeChargesBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.privateFinanceInitiativeCharges,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const rentRatesCostsBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.rentRatesCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const specialFacilitiesCostsBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.specialFacilitiesCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const staffDevelopmentTrainingCostsBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.staffDevelopmentTrainingCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const staffRelatedInsuranceCostsBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.staffRelatedInsuranceCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const supplyTeacherInsurableCostsBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.supplyTeacherInsurableCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const communityFocusedSchoolStaffBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.communityFocusedSchoolStaff,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const communityFocusedSchoolCostsBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.communityFocusedSchoolCosts,
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
                        <span className="govuk-accordion__section-button" id="accordion-heading-other-cost">
                            Other costs
                        </span>
                    </h2>
                </div>
                <div id="accordion-content-other-cost" className="govuk-accordion__section-content"
                     aria-labelledby="accordion-heading-other-cost">
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Total other costs</h3>}
                                  data={totalOtherCostsBarData}
                                  elementId="total-otehr-costs"
                                  chartDimensions={chartDimensions}
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Other insurance costs</h3>}
                                  data={otherInsurancePremiumsCostsBarData}
                                  elementId="other-insurance-costs"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Direct revenue financing costs</h3>}
                                  data={directRevenueFinancingCostsBarData}
                                  elementId="direct-revenue-financing-costs"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Ground maintenance costs</h3>}
                                  data={groundsMaintenanceCostsBarData}
                                  elementId="ground-maintenance-costs"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Indirect employee expenses</h3>}
                                  data={indirectEmployeeExpensesBarData}
                                  elementId="indirect-employee-expenses"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Interest charges for loan and bank</h3>}
                                  data={interestChargesLoanBankBarData}
                                  elementId="interest-charges-loan-bank"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">PFI costs</h3>}
                                  data={privateFinanceInitiativeChargesBarData}
                                  elementId="pfi-costs"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Rent and rates costs</h3>}
                                  data={rentRatesCostsBarData}
                                  elementId="rent-rates-cots"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Special facilities costs</h3>}
                                  data={specialFacilitiesCostsBarData}
                                  elementId="special-facilities-costs"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Staff development and training costs</h3>}
                                  data={staffDevelopmentTrainingCostsBarData}
                                  elementId="staff-development-training-costs"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Staff-related insurance costs</h3>}
                                  data={staffRelatedInsuranceCostsBarData}
                                  elementId="staff-related-insurance-costs"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Supply teacher insurance costs</h3>}
                                  data={supplyTeacherInsurableCostsBarData}
                                  elementId="supply-teacher-insurance-costs"
                    />
                    <ChartWrapper
                        heading={<h3 className="govuk-heading-s">Community focused school staff (maintained schools
                            only)</h3>}
                        data={communityFocusedSchoolStaffBarData}
                        elementId="community-focused-staff"
                    />
                    <ChartWrapper
                        heading={<h3 className="govuk-heading-s">Community focused school costs (maintained schools
                            only)</h3>}
                        data={communityFocusedSchoolCostsBarData}
                        elementId="community-focused-costs"
                    />
                </div>
            </div>
        </ChartDimensionContext.Provider>
    )
};

export default OtherCosts

export type OtherCostsProps = {
    schools: OtherCostsData[]
}

export type OtherCostsData = {
    urn: string
    name: string
    schoolType: string
    localAuthority: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
    totalOtherCosts: number
    otherInsurancePremiumsCosts: number
    directRevenueFinancingCosts: number
    groundsMaintenanceCosts: number
    indirectEmployeeExpenses: number
    interestChargesLoanBank: number
    privateFinanceInitiativeCharges: number
    rentRatesCosts: number
    specialFacilitiesCosts: number
    staffDevelopmentTrainingCosts: number
    staffRelatedInsuranceCosts: number
    supplyTeacherInsurableCosts: number
    communityFocusedSchoolStaff: number
    communityFocusedSchoolCosts: number
}