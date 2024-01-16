import React, {useMemo, useState} from "react";
import ChartWrapper from "../chart-wrapper";
import {
    CalculateWorkforceValue,
    DimensionHeading, HeadcountPerFTE, PercentageOfWorkforce,
    Total, WorkforceCategories
} from "../../chart-dimensions";
import {ChartDimensionContext} from "../../contexts";
import {ChartWrapperData} from "../../types";

const SchoolWorkforce: React.FC<SchoolWorkforceProps> = (props) => {
    const {schools} = props
    const [dimension, setDimension] = useState(Total)
    const tableHeadings = ["School name", "Local Authority", "School type", "Number of pupils", DimensionHeading(dimension)]

    const chartData: ChartWrapperData = useMemo(() => {
        return {
            dataPoints: schools.map(school => {
                return {
                    school: school.name,
                    urn: school.urn,
                    value: CalculateWorkforceValue({
                        dimension: dimension,
                        value: school.schoolWorkforceFTE,
                        ...school
                    }),
                    additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
                }
            }),
            tableHeadings: tableHeadings
        }
    }, [schools, dimension]);

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    const chartDimensions = {dimensions: WorkforceCategories.filter(function(category) {
            return category !== PercentageOfWorkforce && category !== HeadcountPerFTE
        }), handleChange: handleSelectChange}

    return (
        <ChartDimensionContext.Provider value={dimension}>
            <ChartWrapper heading={<h3 className="govuk-heading-s">School workforce (Full Time Equivalent)</h3>}
                          data={chartData}
                          elementId="school-workforce"
                          chartDimensions={chartDimensions}
            />
        </ChartDimensionContext.Provider>
    )
};

export default SchoolWorkforce

export type SchoolWorkforceProps = {
    schools: SchoolWorkforceData[]
}

export type SchoolWorkforceData = {
    urn: string
    name: string
    schoolType: string
    localAuthority: string
    numberOfPupils: bigint
    schoolWorkforceFTE: number
}