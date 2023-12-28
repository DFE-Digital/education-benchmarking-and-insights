import React from 'react';

export const PoundsPerPupil = "£ per pupil";
export const PoundsPerMetreSq = "£ per m²"
export const Actual = "actuals";
export const PercentageExpenditure = "percentage of expenditure";
export const PercentageIncome = "percentage of income";

export const CostCategories = [PoundsPerPupil, Actual, PercentageExpenditure, PercentageIncome]
export const PremisesCategories = [PoundsPerMetreSq, Actual, PercentageExpenditure, PercentageIncome]

export type ChartDimensions = {
    dimensions: string[]
    handleChange: React.ChangeEventHandler<HTMLSelectElement>
}