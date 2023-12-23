import React from "react";
import AccordionChartContent from "./accordion-chart-content.tsx";

const OtherCosts: React.FC<OtherCostsExpenditure> = ({urn, schools}) => {
    const labels = schools.map(result => result.name)

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
                <AccordionChartContent heading={'Total other costs'}
                                       data={totalOtherCostsBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'Other insurance costs'}
                                       data={otherInsurancePremiumsCostsBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'Direct revenue financing costs'}
                                       data={directRevenueFinancingCostsBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'Ground maintenance costs'}
                                       data={groundsMaintenanceCostsBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'Indirect employee expenses'}
                                       data={indirectEmployeeExpensesBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'Interest charges for loan and bank'}
                                       data={interestChargesLoanBankBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'PFI costs'}
                                       data={privateFinanceInitiativeChargesBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'Rent and rates costs'}
                                       data={rentRatesCostsBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'Special facilities costs'}
                                       data={specialFacilitiesCostsBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'Staff development and training costs'}
                                       data={staffDevelopmentTrainingCostsBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'Staff-related insurance costs'}
                                       data={staffRelatedInsuranceCostsBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'Supply teacher insurance costs'}
                                       data={supplyTeacherInsurableCostsBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'Community focused school staff (maintained schools only)'}
                                       data={communityFocusedSchoolStaffBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'Community focused school costs (maintained schools only)'}
                                       data={communityFocusedSchoolCostsBarData}
                                       chosenSchoolName={chosenSchoolName}/>
            </div>
        </div>
    )
};

export default OtherCosts

export type OtherCostsExpenditure = {
    urn: string
    schools: OtherCostsExpenditureData[]
}

export type OtherCostsExpenditureData = {
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