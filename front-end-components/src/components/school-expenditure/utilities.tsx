import React, {useState} from "react";
import {CalculatePremisesValue, DimensionHeading, PoundsPerMetreSq, PremisesCategories} from "../../chart-dimensions";
import {ChartWrapperData} from "../../types.tsx";
import {ChartDimensionContext} from "../../contexts.tsx";
import ChartWrapper from "../chart-wrapper.tsx";

const Utilities: React.FC<UtilitiesProps> = ({schools}) => {
    const [dimension, setDimension] = useState(PoundsPerMetreSq)
    const tableHeadings = ["School name", "Local Authority", "School type", "Number of pupils", DimensionHeading(dimension)]

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    const chartDimensions = {dimensions: PremisesCategories, handleChange: handleSelectChange}

    const totalUtilitiesCostsBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculatePremisesValue({
                    dimension: dimension,
                    value: school.totalUtilitiesCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const energyBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculatePremisesValue({
                    dimension: dimension,
                    value: school.energyCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const waterSewerageBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculatePremisesValue({
                    dimension: dimension,
                    value: school.waterSewerageCosts,
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
                        <span className="govuk-accordion__section-button" id="accordion-heading-utilities">
                            Utilities
                        </span>
                    </h2>
                </div>
                <div id="accordion-content-utilities" className="govuk-accordion__section-content"
                     aria-labelledby="accordion-heading-utilities">
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Total utilities costs</h3>}
                                  data={totalUtilitiesCostsBarData}
                                  elementId="total-utilities-costs"
                                  chartDimensions={chartDimensions}
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Energy costs</h3>}
                                  data={energyBarData}
                                  elementId="energy-costs"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Water and sewerage costs</h3>}
                                  data={waterSewerageBarData}
                                  elementId="water-sewerage-costs"
                    />
                </div>
            </div>
        </ChartDimensionContext.Provider>
)
};

export default Utilities

export type UtilitiesProps = {
    schools: UtilitiesData[]
}

export type UtilitiesData = {
    urn: string
    name: string
    schoolType: string
    localAuthority: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
    totalUtilitiesCosts: number
    energyCosts: number
    waterSewerageCosts: number
}