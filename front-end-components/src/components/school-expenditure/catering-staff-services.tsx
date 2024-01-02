import React, {useState} from "react";
import ChartWrapper from "../chart-wrapper";
import {CalculateCostValue, CostCategories, DimensionHeading, PoundsPerPupil} from "../../chart-dimensions";
import {ChartDimensionContext} from "../../contexts";
import {ChartWrapperData} from "../../types";

const CateringStaffServices: React.FC<CateringStaffServicesProps> = ({schools}) => {
    const [dimension, setDimension] = useState(PoundsPerPupil)
    const tableHeadings = ["School name", "Local Authority", "School type", "Number of pupils", DimensionHeading(dimension)]

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    const chartDimensions = {dimensions: CostCategories, handleChange: handleSelectChange}

    const netCateringBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.netCateringCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const cateringStaffBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.cateringStaffCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const cateringSuppliesBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.cateringSuppliesCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const incomeCateringBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.incomeCatering,
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
                              id="accordion-heading-catering-staff-services">
                            Catering staff and services
                        </span>
                    </h2>
                </div>
                <div id="accordion-content-catering-staff-services" className="govuk-accordion__section-content"
                     aria-labelledby="accordion-heading-catering-staff-services">
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Net catering costs</h3>}
                                  data={netCateringBarData}
                                  elementId="net-catering-costs"
                                  chartDimensions={chartDimensions}
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Catering staff costs</h3>}
                                  data={cateringStaffBarData}
                                  elementId="catering-staff-costs"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Catering supplies costs</h3>}
                                  data={cateringSuppliesBarData}
                                  elementId="catering-supplies-costs"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Income from catering</h3>}
                                  data={incomeCateringBarData}
                                  elementId="income-from-catering"
                    />
                </div>
            </div>
        </ChartDimensionContext.Provider>
    )
};

export default CateringStaffServices

export type CateringStaffServicesProps = {
    schools: CateringStaffServicesData[]
}

export type CateringStaffServicesData = {
    urn: string
    name: string
    schoolType: string
    localAuthority: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
    netCateringCosts: number
    cateringStaffCosts: number
    cateringSuppliesCosts: number
    incomeCatering: number
}