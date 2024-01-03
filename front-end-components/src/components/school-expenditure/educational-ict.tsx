import React, {useState} from "react";
import ChartWrapper from "../chart-wrapper";
import {CalculateCostValue, CostCategories, DimensionHeading, PoundsPerPupil} from "../../chart-dimensions";
import {ChartDimensionContext} from "../../contexts";
import {ChartWrapperData} from "../../types";

const EducationalIct: React.FC<EducationalIctProps> = ({ schools}) => {
    const [dimension, setDimension] = useState(PoundsPerPupil)
    const tableHeadings = ["School name", "Local Authority", "School type", "Number of pupils", DimensionHeading(dimension)]

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    const chartDimensions = {dimensions: CostCategories, handleChange: handleSelectChange}

    const learningResourcesBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.learningResourcesIctCosts,
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
                        <span className="govuk-accordion__section-button" id="accordion-heading-educational-ict">
                            Educational ICT
                        </span>
                    </h2>
                </div>
                <div id="accordion-content-educational-ict" className="govuk-accordion__section-content"
                     aria-labelledby="accordion-heading-educational-ict">
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Educational learning resources costs</h3>}
                                  data={learningResourcesBarData}
                                  elementId="eductional-learning-resources-costs"
                                  chartDimensions={chartDimensions}
                    />
                </div>
            </div>
        </ChartDimensionContext.Provider>
    )
};

export default EducationalIct

export type EducationalIctProps = {
    schools: EducationalIctData[]
}

export type EducationalIctData = {
    urn: string
    name: string
    schoolType: string
    localAuthority: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
    learningResourcesIctCosts: number
}