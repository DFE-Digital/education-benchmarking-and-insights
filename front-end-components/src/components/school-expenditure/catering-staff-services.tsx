import React from "react";
import AccordionChartContent from "./accordion-chart-content.tsx";

const CateringStaffServices: React.FC<CateringStaffServicesExpenditure> = ({urn, schools}) => {
    const labels = schools.map(result => result.name)

    const netCateringBarData = {
        labels: labels,
        data: schools.map(result => result.netCateringCosts)
    }

    const cateringStaffBarData = {
        labels: labels,
        data: schools.map(result => result.cateringStaffCosts)
    }

    const cateringSuppliesBarData = {
        labels: labels,
        data: schools.map(result => result.cateringSuppliesCosts)
    }

    const incomeCateringBarData = {
        labels: labels,
        data: schools.map(result => result.incomeCatering)
    }

    const chosenSchoolName = schools.find(school => school.urn === urn)?.name || '';

    return (
        <div className="govuk-accordion__section">
            <div className="govuk-accordion__section-header">
                <h2 className="govuk-accordion__section-heading">
                        <span className="govuk-accordion__section-button"
                              id="accordion-heading-catering-staff-services">
                            Catering staff and services
                        </span>
                </h2>
            </div>
            <div id="accordion-content-catering-staff-services" className="govuk-accordion__section-content"
                 aria-labelledby="accordion-heading-catering-staff-services">
                <AccordionChartContent heading={'Net catering costs'}
                                       data={netCateringBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'Catering staff costs'}
                                       data={cateringStaffBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'Catering supplies costs'}
                                       data={cateringSuppliesBarData}
                                       chosenSchoolName={chosenSchoolName}/>
                <AccordionChartContent heading={'Income from catering'}
                                       data={incomeCateringBarData}
                                       chosenSchoolName={chosenSchoolName}/>
            </div>
        </div>
    )
};

export default CateringStaffServices

export type CateringStaffServicesExpenditure = {
    urn: string
    schools: CateringStaffServicesExpenditureData[]
}

export type CateringStaffServicesExpenditureData = {
    urn: string
    name: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
    netCateringCosts: number
    cateringStaffCosts: number
    cateringSuppliesCosts: number
    incomeCatering: number
}