import React, {useState} from "react";
import {AdministrativeSuppliesProps} from "src/views/compare-your-costs/partials/accordion-sections/types";
import {
    CalculateCostValue,
    CostCategories,
    DimensionHeading,
    PoundsPerPupil,
    HorizontalBarChartWrapper,
    HorizontalBarChartWrapperData, ChartDimensions
} from "src/components";
import {ChartDimensionContext} from "src/contexts";

export const AdministrativeSupplies: React.FC<AdministrativeSuppliesProps> = ({schools}) => {
    const [dimension, setDimension] = useState(PoundsPerPupil)
    const tableHeadings = ["School name", "Local Authority", "School type", "Number of pupils", DimensionHeading(dimension)]

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    const administrativeSuppliesBarData: HorizontalBarChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.administrativeSuppliesCosts,
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
                        <span className="govuk-accordion__section-button"
                              id="accordion-heading-administrative-supplies">
                            Administrative supplies
                        </span>
                    </h2>
                </div>
                <div id="accordion-content-administrative-supplies" className="govuk-accordion__section-content"
                     aria-labelledby="accordion-heading-administrative-supplies">
                    <HorizontalBarChartWrapper data={administrativeSuppliesBarData}
                                               elementId="administrative-supplies-non-eductional">
                        <h3 className="govuk-heading-s">Administrative supplies (Non-educational)</h3>
                        <ChartDimensions dimensions={CostCategories} handleChange={handleSelectChange} elementId="administrative-supplies-non-eductional" defaultValue={dimension} />
                    </HorizontalBarChartWrapper>
                </div>
            </div>
        </ChartDimensionContext.Provider>
    )
};

