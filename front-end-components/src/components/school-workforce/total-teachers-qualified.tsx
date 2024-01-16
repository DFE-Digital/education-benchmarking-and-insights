import React, {useMemo} from "react";
import ChartWrapper from "../chart-wrapper";
import {ChartDimensionContext} from "../../contexts";
import {ChartWrapperData} from "../../types";

const TotalTeachersQualified: React.FC<TotalTeachersQualifiedProps> = (props) => {
    const {schools} = props
    const tableHeadings = ["School name", "Local Authority", "School type", "Number of pupils", "Percent"]

    const chartData: ChartWrapperData = useMemo(() => {
        return {
            dataPoints: schools.map(school => {
                return {
                    school: school.name,
                    urn: school.urn,
                    value: school.teachersWithQTSFTE,
                    additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
                }
            }),
            tableHeadings: tableHeadings
        }
    }, [schools]);


    return (
        <ChartDimensionContext.Provider value={"percent"}>
            <ChartWrapper heading={<h3 className="govuk-heading-s">Teachers with qualified Teacher Status (%)</h3>}
                          data={chartData}
                          elementId="total-teachers-qualified"
            />
        </ChartDimensionContext.Provider>
    )
};

export default TotalTeachersQualified

export type TotalTeachersQualifiedProps = {
    schools: TotalTeachersQualifiedData[]
}

export type TotalTeachersQualifiedData = {
    urn: string
    name: string
    schoolType: string
    localAuthority: string
    numberOfPupils: bigint
    schoolWorkforceFTE: number
    teachersWithQTSFTE: number
}