import React, {useState} from "react";
import ChartWrapper from "../chart-wrapper";
import {CalculateCostValue, CostCategories, DimensionHeading, PoundsPerPupil} from "../../chart-dimensions";
import {ChartDimensionContext} from "../../contexts";
import {ChartWrapperData} from "../../types";

const EducationalSupplies: React.FC<EducationalSuppliesProps> = ({schools}) => {
    const [dimension, setDimension] = useState(PoundsPerPupil)
    const tableHeadings = ["School name", "Local Authority", "School type", "Number of pupils", DimensionHeading(dimension)]

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    const chartDimensions = {dimensions: CostCategories, handleChange: handleSelectChange}

    const totalEducationalSuppliesBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.totalEducationalSuppliesCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const examinationFeesBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.examinationFeesCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const breakdownEducationalBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.breakdownEducationalSuppliesCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const learningResourcesBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.learningResourcesNonIctCosts,
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
                        <span className="govuk-accordion__section-button" id="accordion-heading-educational-supplies">
                            Educational supplies
                        </span>
                    </h2>
                </div>
                <div id="accordion-content-educational-supplies" className="govuk-accordion__section-content"
                     aria-labelledby="accordion-heading-educational-supplies">
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Total educational supplies costs</h3>}
                                  data={totalEducationalSuppliesBarData}
                                  elementId="total-educational-supplies-costs"
                                  chartDimensions={chartDimensions}
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Examination fees costs</h3>}
                                  data={examinationFeesBarData}
                                  elementId="examination-fees-costs"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Breakdown of educational supplies costs</h3>}
                                  data={breakdownEducationalBarData}
                                  elementId="breakdown-eductional-supplies-costs"
                    />
                    <ChartWrapper
                        heading={<h3 className="govuk-heading-s">Learning resources (not ICT equipment) costs</h3>}
                        data={learningResourcesBarData}
                        elementId="learning-resource-not-ict-costs"
                    />
                </div>
            </div>
        </ChartDimensionContext.Provider>
    )
};

export default EducationalSupplies

export type EducationalSuppliesProps = {
    schools: EducationalSuppliesData[]
}

export type EducationalSuppliesData = {
    urn: string
    name: string
    schoolType: string
    localAuthority: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
    totalEducationalSuppliesCosts: number
    examinationFeesCosts: number
    breakdownEducationalSuppliesCosts: number
    learningResourcesNonIctCosts: number
}