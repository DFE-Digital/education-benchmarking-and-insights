import React, {useState} from "react";
import {OtherCostsProps} from "src/views/compare-your-costs/partials/accordion-sections/types";
import {
    CalculateCostValue,
    CostCategories,
    DimensionHeading,
    PoundsPerPupil, HorizontalBarChartWrapper, HorizontalBarChartWrapperData, ChartDimensions
} from "src/components";
import {ChartDimensionContext} from "src/contexts";

export const OtherCosts: React.FC<OtherCostsProps> = ({schools}) => {
    const [dimension, setDimension] = useState(PoundsPerPupil)
    const tableHeadings = ["School name", "Local Authority", "School type", "Number of pupils", DimensionHeading(dimension)]

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    const totalOtherCostsBarData: HorizontalBarChartWrapperData = {
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

    const otherInsurancePremiumsCostsBarData: HorizontalBarChartWrapperData = {
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

    const directRevenueFinancingCostsBarData: HorizontalBarChartWrapperData = {
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

    const groundsMaintenanceCostsBarData: HorizontalBarChartWrapperData = {
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

    const indirectEmployeeExpensesBarData: HorizontalBarChartWrapperData = {
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

    const interestChargesLoanBankBarData: HorizontalBarChartWrapperData = {
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

    const privateFinanceInitiativeChargesBarData: HorizontalBarChartWrapperData = {
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

    const rentRatesCostsBarData: HorizontalBarChartWrapperData = {
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

    const specialFacilitiesCostsBarData: HorizontalBarChartWrapperData = {
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

    const staffDevelopmentTrainingCostsBarData: HorizontalBarChartWrapperData = {
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

    const staffRelatedInsuranceCostsBarData: HorizontalBarChartWrapperData = {
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

    const supplyTeacherInsurableCostsBarData: HorizontalBarChartWrapperData = {
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

    const communityFocusedSchoolStaffBarData: HorizontalBarChartWrapperData = {
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

    const communityFocusedSchoolCostsBarData: HorizontalBarChartWrapperData = {
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
                    <HorizontalBarChartWrapper data={totalOtherCostsBarData} chartId="total-otehr-costs">
                        <h3 className="govuk-heading-s">Total other costs</h3>
                        <ChartDimensions dimensions={CostCategories} handleChange={handleSelectChange} elementId="total-otehr-costs" defaultValue={dimension} />
                    </HorizontalBarChartWrapper>
                    <HorizontalBarChartWrapper data={otherInsurancePremiumsCostsBarData} chartId="other-insurance-premiums-costs">
                        <h3 className="govuk-heading-s">Other insurance premiums costs</h3>
                    </HorizontalBarChartWrapper>
                    <HorizontalBarChartWrapper data={directRevenueFinancingCostsBarData} chartId="direct-revenue-financing-costs">
                        <h3 className="govuk-heading-s">Direct revenue financing costs</h3>
                    </HorizontalBarChartWrapper>
                    <HorizontalBarChartWrapper data={groundsMaintenanceCostsBarData} chartId="ground-maintenance-costs">
                        <h3 className="govuk-heading-s">Ground maintenance costs</h3>
                    </HorizontalBarChartWrapper>
                    <HorizontalBarChartWrapper data={indirectEmployeeExpensesBarData} chartId="indirect-employee-expenses">
                        <h3 className="govuk-heading-s">Indirect employee expenses</h3>
                    </HorizontalBarChartWrapper>
                    <HorizontalBarChartWrapper data={interestChargesLoanBankBarData} chartId="interest-charges-loan-bank">
                        <h3 className="govuk-heading-s">Interest charges for loan and bank</h3>
                    </HorizontalBarChartWrapper>
                    <HorizontalBarChartWrapper data={privateFinanceInitiativeChargesBarData} chartId="pfi-charges">
                        <h3 className="govuk-heading-s">PFI charges</h3>
                    </HorizontalBarChartWrapper>
                    <HorizontalBarChartWrapper data={rentRatesCostsBarData} chartId="rent-rates-cots">
                        <h3 className="govuk-heading-s">Rent and rates costs</h3>
                    </HorizontalBarChartWrapper>
                    <HorizontalBarChartWrapper data={specialFacilitiesCostsBarData} chartId="special-facilities-costs">
                        <h3 className="govuk-heading-s">Special facilities costs</h3>
                    </HorizontalBarChartWrapper>
                    <HorizontalBarChartWrapper data={staffDevelopmentTrainingCostsBarData} chartId="staff-development-training-costs">
                        <h3 className="govuk-heading-s">Staff development and training costs</h3>
                    </HorizontalBarChartWrapper>
                    <HorizontalBarChartWrapper data={staffRelatedInsuranceCostsBarData} chartId="staff-related-insurance-costs">
                        <h3 className="govuk-heading-s">Staff-related insurance costs</h3>
                    </HorizontalBarChartWrapper>
                    <HorizontalBarChartWrapper data={supplyTeacherInsurableCostsBarData} chartId="supply-teacher-insurance-costs">
                        <h3 className="govuk-heading-s">Supply teacher insurance costs</h3>
                    </HorizontalBarChartWrapper>
                    <HorizontalBarChartWrapper data={communityFocusedSchoolStaffBarData} chartId="community-focused-staff">
                        <h3 className="govuk-heading-s">Community focused school staff (maintained schools only)</h3>
                    </HorizontalBarChartWrapper>
                    <HorizontalBarChartWrapper data={communityFocusedSchoolCostsBarData} chartId="community-focused-costs">
                        <h3 className="govuk-heading-s">Community focused school costs (maintained schools only)</h3>
                    </HorizontalBarChartWrapper>
                </div>
            </div>
        </ChartDimensionContext.Provider>
    )
};