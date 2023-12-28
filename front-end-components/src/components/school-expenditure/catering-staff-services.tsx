import React, {useState} from "react";
import ChartWrapper from "../chart-wrapper";
import {CalculateCostValue, CostCategories, PoundsPerPupil} from "../../chart-dimensions";

const CateringStaffServices: React.FC<CateringStaffServicesProps> = ({urn, schools}) => {
    const labels = schools.map(result => result.name)
    const [dimension, setDimension] = useState(PoundsPerPupil)

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    const chartDimensions = {dimensions: CostCategories, handleChange: handleSelectChange}

    const netCateringBarData = {
        labels: labels,
        data: schools.map(result => CalculateCostValue({
            dimension: dimension,
            value: result.netCateringCosts,
            ...result
        }))
    }

    const cateringStaffBarData = {
        labels: labels,
        data: schools.map(result => CalculateCostValue({
            dimension: dimension,
            value: result.cateringStaffCosts,
            ...result
        }))
    }

    const cateringSuppliesBarData = {
        labels: labels,
        data: schools.map(result => CalculateCostValue({
            dimension: dimension,
            value: result.cateringSuppliesCosts,
            ...result
        }))
    }

    const incomeCateringBarData = {
        labels: labels,
        data: schools.map(result => CalculateCostValue({
            dimension: dimension,
            value: result.incomeCatering,
            ...result
        }))
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
                <ChartWrapper heading={<h3 className="govuk-heading-s">Net catering costs</h3>}
                              data={netCateringBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="net-catering-costs"
                              chartDimensions={chartDimensions}
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Catering staff costs</h3>}
                              data={cateringStaffBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="catering-staff-costs"
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Catering supplies costs</h3>}
                              data={cateringSuppliesBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="catering-supplies-costs"
                              selectedDimension={dimension}
                />
                <ChartWrapper heading={<h3 className="govuk-heading-s">Income from catering</h3>}
                              data={incomeCateringBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="income-from-catering"
                              selectedDimension={dimension}
                />
            </div>
        </div>
    )
};

export default CateringStaffServices

export type CateringStaffServicesProps = {
    urn: string
    schools: CateringStaffServicesData[]
}

export type CateringStaffServicesData = {
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