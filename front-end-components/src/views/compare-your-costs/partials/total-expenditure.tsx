import React, {useState} from "react";
import {TotalExpenditureProps} from "src/views/compare-your-costs/partials";
import {ChartDimensionContext} from "src/contexts";
import {
    HorizontalBarChartWrapper,
    HorizontalBarChartWrapperData,
    CalculateCostValue,
    CostCategories,
    DimensionHeading,
    PoundsPerPupil, ChartDimensions
} from "src/components";

export const TotalExpenditure: React.FC<TotalExpenditureProps> = ({schools}) => {

    const [dimension, setDimension] = useState(PoundsPerPupil)
    const tableHeadings = ["School name", "Local Authority", "School type", "Number of pupils", DimensionHeading(dimension)]

    const chartData: HorizontalBarChartWrapperData = {
        dataPoints: schools.map(school => {
            return {
                school: school.name,
                urn: school.urn,
                value: CalculateCostValue({
                    dimension: dimension,
                    value: school.totalExpenditure,
                    ...school
                }),
                additionalData: [school.localAuthority, school.schoolType, school.numberOfPupils]
            }
        }),
        tableHeadings: tableHeadings
    }

    const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        setDimension(event.target.value)
    }

    return (
        <ChartDimensionContext.Provider value={dimension}>
            <HorizontalBarChartWrapper data={chartData} elementId="total-expenditure">
                <h2 className="govuk-heading-l">Total Expenditure</h2>
                <ChartDimensions dimensions={CostCategories}
                                 handleChange={handleSelectChange}
                                 elementId="total-expenditure"
                                 defaultValue={dimension}/>
            </HorizontalBarChartWrapper>
        </ChartDimensionContext.Provider>
    )
};

