import React, {useState} from "react";
import ChartWrapper from "../chart-wrapper";
import {CalculateCostValue, CostCategories, DimensionHeading, PoundsPerPupil} from "../../chart-dimensions";
import {ChartDimensionContext} from "../../contexts";
import {ChartWrapperData} from "../../types";

const AdministrativeSupplies: React.FC<AdministrativeSuppliesProps> = ({schools}) => {
    const [dimension, setDimension] = useState(PoundsPerPupil)
    const tableHeadings = ["School name", "Local Authority", "School type", "Number of pupils", DimensionHeading(dimension)]

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    const chartDimensions = {dimensions: CostCategories, handleChange: handleSelectChange}

    const administrativeSuppliesBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.administrativeSuppliesCosts,
                    ...school
                }),
                additionalData: ["", "", school.numberOfPupils]
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
                              id="accordion-heading-administrative-supplies">
                            Administrative supplies
                        </span>
                    </h2>
                </div>
                <div id="accordion-content-administrative-supplies" className="govuk-accordion__section-content"
                     aria-labelledby="accordion-heading-administrative-supplies">
                    <ChartWrapper
                        heading={<h3 className="govuk-heading-s">Administrative supplies (Non-educational)</h3>}
                        data={administrativeSuppliesBarData}
                        fileName="administrative-supplies-non-eductional"
                        chartDimensions={chartDimensions}
                    />
                </div>
            </div>
        </ChartDimensionContext.Provider>
    )
};

export default AdministrativeSupplies

export type AdministrativeSuppliesProps = {
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