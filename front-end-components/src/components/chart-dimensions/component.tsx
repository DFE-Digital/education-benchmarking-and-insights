import {ChartDimensionsProps, CostValue, PremisesValue, WorkforceValue} from "src/components/chart-dimensions";
import React, {useContext} from "react";
import {ChartModeContext} from "src/contexts";
import {ChartModeChart} from "src/components";

export const PoundsPerPupil = "£ per pupil";
export const PoundsPerMetreSq = "£ per m²"
export const Actual = "actuals";
export const PercentageExpenditure = "percentage of expenditure";
export const PercentageIncome = "percentage of income";
export const Total = "total";
export const HeadcountPerFTE = "headcount per FTE";
export const PercentageOfWorkforce = "percentage of workforce";
export const PupilsPerStaffRole = "pupils per staff role";

export const CostCategories = [PoundsPerPupil, Actual, PercentageExpenditure, PercentageIncome]
export const PremisesCategories = [PoundsPerMetreSq, Actual, PercentageExpenditure, PercentageIncome]
export const WorkforceCategories = [Total, HeadcountPerFTE, PercentageOfWorkforce, PupilsPerStaffRole]

export function CalculateCostValue(costValue: CostValue): number {
    switch (costValue.dimension) {
        case PoundsPerPupil :
            return costValue.value / Number(costValue.numberOfPupils)
        case PercentageExpenditure :
            return (costValue.value / costValue.totalExpenditure) * 100
        case PercentageIncome :
            return (costValue.value / costValue.totalIncome) * 100
        case Actual :
            return costValue.value
        default:
            return 0
    }
}

export function DimensionHeading(dimension: string): string {
    switch (dimension) {
        case PercentageExpenditure :
        case PercentageIncome :
        case PercentageOfWorkforce:
            return "Percentage"
        case Total :
            return "Count"
        case HeadcountPerFTE:
            return "Ratio"
        case PupilsPerStaffRole:
            return "Pupils per staff role"
        default:
            return "Amount"
    }
}

export function CalculatePremisesValue(premisesValue: PremisesValue): number {
    switch (premisesValue.dimension) {
        case PercentageExpenditure :
            return (premisesValue.value / premisesValue.totalExpenditure) * 100
        case PercentageIncome :
            return (premisesValue.value / premisesValue.totalIncome) * 100
        case Actual :
            return premisesValue.value
        default:
            return 0
    }
}

export function CalculateWorkforceValue(workforceValue: WorkforceValue): number {
    switch (workforceValue.dimension) {
        case Total :
            return workforceValue.value
        case HeadcountPerFTE :
            return workforceValue.schoolWorkforceFTE / workforceValue.value
        case PercentageOfWorkforce :
            return (workforceValue.value / workforceValue.schoolWorkforceFTE) * 100
        case PupilsPerStaffRole :
            return Number(workforceValue.numberOfPupils) / workforceValue.value
        default:
            return 0
    }
}

export const ChartDimensions: React.FC<ChartDimensionsProps> = (props) => {
    const {dimensions, elementId, handleChange, defaultValue} = props;
    const mode = useContext(ChartModeContext);

    return <div className="govuk-form-group">
        <label className="govuk-label" htmlFor={`${elementId}-dimension`}>
            {mode == ChartModeChart ? 'View graph as' : 'View table as'}
        </label>
        <select className="govuk-select" name="dimension" id={`${elementId}-dimension`}
                onChange={handleChange} defaultValue={defaultValue}>
            {dimensions.map((dimension, idx) => {
                return <option key={idx} value={dimension}>{dimension}</option>;
            })}
        </select>
    </div>
};