import React, {useState} from "react";
import ChartWrapper from "../chart-wrapper";
import {
    CalculatePremisesValue,
    DimensionHeading,
    PoundsPerMetreSq,
    PremisesCategories
} from "../../chart-dimensions";
import {ChartDimensionContext} from "../../contexts";
import {ChartWrapperData} from "../../types";

const PremisesStaffServices: React.FC<PremisesStaffServicesProps> = ({schools}) => {
    const [dimension, setDimension] = useState(PoundsPerMetreSq)
    const tableHeadings = ["School name", "Local Authority", "School type", "Number of pupils", DimensionHeading(dimension)]

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    const chartDimensions = {dimensions: PremisesCategories, handleChange: handleSelectChange}

    const totalPremisesStaffServiceCostsBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculatePremisesValue({
                    dimension: dimension,
                    value: school.totalPremisesStaffServiceCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const cleaningCaretakingBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculatePremisesValue({
                    dimension: dimension,
                    value: school.cleaningCaretakingCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const maintenanceBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculatePremisesValue({
                    dimension: dimension,
                    value: school.maintenancePremisesCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const otherOccupationBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculatePremisesValue({
                    dimension: dimension,
                    value: school.otherOccupationCosts,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const premisesStaffBarData: ChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculatePremisesValue({
                    dimension: dimension,
                    value: school.premisesStaffCosts,
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
                              id="accordion-heading-premises-staff-services">
                            Premises staff and services
                        </span>
                    </h2>
                </div>
                <div id="accordion-content-premises-staff-services" className="govuk-accordion__section-content"
                     aria-labelledby="accordion-heading-premises-staff-services">
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Total premises staff and service costs</h3>}
                                  data={totalPremisesStaffServiceCostsBarData}
                                  elementId="total-premises-staff-service-costs"
                                  chartDimensions={chartDimensions}
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Cleaning and caretaking costs</h3>}
                                  data={cleaningCaretakingBarData}
                                  elementId="cleaning-caretaking-costs"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Maintenance of premises costs</h3>}
                                  data={maintenanceBarData}
                                  elementId="maintenance-premises-costs"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Other occupation costs</h3>}
                                  data={otherOccupationBarData}
                                  elementId="other-occupation-costs"
                    />
                    <ChartWrapper heading={<h3 className="govuk-heading-s">Premises staff costs</h3>}
                                  data={premisesStaffBarData}
                                  elementId="premises staff costs"
                    />
                </div>
            </div>
        </ChartDimensionContext.Provider>
    )
};

export default PremisesStaffServices

export type PremisesStaffServicesProps = {
    schools: PremisesStaffServicesData[]
}

export type PremisesStaffServicesData = {
    urn: string
    name: string
    schoolType: string
    localAuthority: string
    totalIncome: number
    totalExpenditure: number
    numberOfPupils: bigint
    totalPremisesStaffServiceCosts: number
    cleaningCaretakingCosts: number
    maintenancePremisesCosts: number
    otherOccupationCosts: number
    premisesStaffCosts: number
}