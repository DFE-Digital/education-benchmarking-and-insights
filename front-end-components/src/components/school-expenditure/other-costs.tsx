import React, {useState} from "react";
import ChartWrapper from "../chart-wrapper";
import {CostCategories, PoundsPerPupil} from "../../chart-dimensions";

const OtherCosts: React.FC<OtherCostsProps> = ({urn, schools}) => {
    const labels = schools.map(result => result.name)
    const [dimension, setDimension] = useState(PoundsPerPupil)

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    const chartDimensions = {dimensions: CostCategories, handleChange: handleSelectChange}

    const totalOtherCostsBarData = {
        labels: labels,
        data: schools.map(result => result.totalOtherCosts)
    }

    const otherInsurancePremiumsCostsBarData = {
        labels: labels,
        data: schools.map(result => result.otherInsurancePremiumsCosts)
    }

    const directRevenueFinancingCostsBarData = {
        labels: labels,
        data: schools.map(result => result.directRevenueFinancingCosts)
    }

    const groundsMaintenanceCostsBarData = {
        labels: labels,
        data: schools.map(result => result.groundsMaintenanceCosts)
    }

    const indirectEmployeeExpensesBarData = {
        labels: labels,
        data: schools.map(result => result.indirectEmployeeExpenses)
    }

    const interestChargesLoanBankBarData = {
        labels: labels,
        data: schools.map(result => result.interestChargesLoanBank)
    }

    const privateFinanceInitiativeChargesBarData = {
        labels: labels,
        data: schools.map(result => result.privateFinanceInitiativeCharges)
    }

    const rentRatesCostsBarData = {
        labels: labels,
        data: schools.map(result => result.rentRatesCosts)
    }

    const specialFacilitiesCostsBarData = {
        labels: labels,
        data: schools.map(result => result.specialFacilitiesCosts)
    }

    const staffDevelopmentTrainingCostsBarData = {
        labels: labels,
        data: schools.map(result => result.staffDevelopmentTrainingCosts)
    }

    const staffRelatedInsuranceCostsBarData = {
        labels: labels,
        data: schools.map(result => result.staffRelatedInsuranceCosts)
    }

    const supplyTeacherInsurableCostsBarData = {
        labels: labels,
        data: schools.map(result => result.supplyTeacherInsurableCosts)
    }

    const communityFocusedSchoolStaffBarData = {
        labels: labels,
        data: schools.map(result => result.communityFocusedSchoolStaff)
    }

    const communityFocusedSchoolCostsBarData = {
        labels: labels,
        data: schools.map(result => result.communityFocusedSchoolCosts)
    }

    const chosenSchoolName = schools.find(school => school.urn === urn)?.name || '';

    return (
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
                              chosenSchoolName={chosenSchoolName}
                              fileName="total-otehr-costs"
                              chartDimensions={chartDimensions}
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Other insurance costs</h3>}
                              data={otherInsurancePremiumsCostsBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="other-insurance-costs"
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Direct revenue financing costs</h3>}
                              data={directRevenueFinancingCostsBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="direct-revenue-financing-costs"
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Ground maintenance costs</h3>}
                              data={groundsMaintenanceCostsBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="ground-maintenance-costs"
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Indirect employee expenses</h3>}
                              data={indirectEmployeeExpensesBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="indirect-employee-expenses"
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Interest charges for loan and bank</h3>}
                              data={interestChargesLoanBankBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="interest-charges-loan-bank"
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">PFI costs</h3>}
                              data={privateFinanceInitiativeChargesBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="pfi-costs"
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Rent and rates costs</h3>}
                              data={rentRatesCostsBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="rent-rates-cots"
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Special facilities costs</h3>}
                              data={specialFacilitiesCostsBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="special-facilities-costs"
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Staff development and training costs</h3>}
                              data={staffDevelopmentTrainingCostsBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="staff-development-training-costs"
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Staff-related insurance costs</h3>}
                              data={staffRelatedInsuranceCostsBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="staff-related-insurance-costs"
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Supply teacher insurance costs</h3>}
                              data={supplyTeacherInsurableCostsBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="supply-teacher-insurance-costs"
                              selectedDimension={dimension}
                />
                <ChartWrapper
                    heading={<h3 className="govuk-heading-s">Community focused school staff (maintained schools
                        only)</h3>}
                    data={communityFocusedSchoolStaffBarData}
                    chosenSchoolName={chosenSchoolName}
                    fileName="community-focused-staff"
                    selectedDimension={dimension}
                />
                <ChartWrapper
                    heading={<h3 className="govuk-heading-s">Community focused school costs (maintained schools
                        only)</h3>}
                    data={communityFocusedSchoolCostsBarData}
                    chosenSchoolName={chosenSchoolName}
                    fileName="community-focused-costs"
                    selectedDimension={dimension}
                />
            </div>
        </div>
    )
};

export default OtherCosts

export type OtherCostsProps = {
    urn: string
    schools: OtherCostsData[]
}

export type OtherCostsData = {
    urn: string
    name: string
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