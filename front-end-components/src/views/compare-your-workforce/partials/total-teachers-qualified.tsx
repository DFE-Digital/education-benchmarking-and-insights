import React from "react";
import {HorizontalBarChartWrapper, HorizontalBarChartWrapperData} from "src/components";
import {ChartDimensionContext} from 'src/contexts'
import {TotalTeachersQualifiedProps} from "src/views/compare-your-workforce/partials";

export const TotalTeachersQualified: React.FC<TotalTeachersQualifiedProps> = (props) => {
    const {schools} = props
    const tableHeadings = ["School name", "Local Authority", "School type", "Number of pupils", "Percent"]

    const chartData: HorizontalBarChartWrapperData = {
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

    return (
        <ChartDimensionContext.Provider value={"percent"}>
            <HorizontalBarChartWrapper data={chartData} chartId="total-teachers-qualified">
                <h2 className="govuk-heading-m">Teachers with qualified Teacher Status (%)</h2>
            </HorizontalBarChartWrapper>
        </ChartDimensionContext.Provider>
    )
};

