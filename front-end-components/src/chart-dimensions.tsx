import {CostValue, PremisesValue} from "./types";

export const PoundsPerPupil = "£ per pupil";
export const PoundsPerMetreSq = "£ per m²"
export const Actual = "actuals";
export const PercentageExpenditure = "percentage of expenditure";
export const PercentageIncome = "percentage of income";

export const CostCategories = [PoundsPerPupil, Actual, PercentageExpenditure, PercentageIncome]
export const PremisesCategories = [PoundsPerMetreSq, Actual, PercentageExpenditure, PercentageIncome]

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
            return "Percentage"
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