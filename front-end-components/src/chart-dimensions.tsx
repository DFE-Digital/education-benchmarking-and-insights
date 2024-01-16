import {CostValue, PremisesValue, WorkforceValue} from "./types";

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