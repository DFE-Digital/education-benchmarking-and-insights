import React, {useState} from "react";
import ChartWrapper from "../chart-wrapper";
import {CostCategories, PoundsPerPupil} from "../../chart-dimensions";

const AdministrativeSupplies: React.FC<AdministrativeSuppliesProps> = ({urn, schools}) => {
    const labels = schools.map(result => result.name)
    const [dimension, setDimension] = useState(PoundsPerPupil)

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    const chartDimensions = {dimensions: CostCategories, handleChange: handleSelectChange}

    const administrativeSuppliesBarData = {
        labels: labels,
        data: schools.map(result => result.administrativeSuppliesCosts)
    }

    const chosenSchoolName = schools.find(school => school.urn === urn)?.name || '';

    return (
        <div className="govuk-accordion__section">
            <div className="govuk-accordion__section-header">
                <h2 className="govuk-accordion__section-heading">
                        <span className="govuk-accordion__section-button"
                              id="accordion-heading-administrative-supplies">
                            Administrative supplies
                        </span>
                </h2>
            </div>
            <div id="accordion-content-administrative-supplies" className="govuk-accordion__section-content"
                 aria-labelledby="accordion-heading-administrative-supplies">
                <ChartWrapper heading={<h3 className="govuk-heading-s">Administrative supplies (Non-educational)</h3>}
                              data={administrativeSuppliesBarData}
                              chosenSchoolName={chosenSchoolName}
                              fileName="administrative-supplies-non-eductional"
                              chartDimensions={chartDimensions}
                              selectedDimension={dimension}
                />
            </div>
        </div>
    )
};

export default AdministrativeSupplies

export type AdministrativeSuppliesProps = {
    urn: string
    schools: AdministrativeSuppliesData[]
}

export type AdministrativeSuppliesData = {
    urn: string
    name: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
    administrativeSuppliesCosts: number
}