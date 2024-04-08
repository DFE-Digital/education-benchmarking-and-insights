import {
  CostValue,
  Dimension,
  PremisesValue,
} from "src/components/chart-dimensions";

/* eslint-disable react-refresh/only-export-components */
export * from "src/components/chart-dimensions/types";
export * from "src/components/chart-dimensions/component";

export const PoundsPerPupil: Dimension = {
  label: "£ per pupil",
  value: "PoundPerPupil",
  unit: "currency",
  heading: "Amount",
};
export const PoundsPerMetreSq: Dimension = {
  label: "£ per m²",
  value: "PoundPerSqMetre",
  unit: "currency",
  heading: "Amount",
};
export const Actual: Dimension = {
  label: "actuals",
  value: "Actuals",
  unit: "currency",
  heading: "Amount",
};
export const Percent: Dimension = {
  label: "percent",
  value: "Percent",
  unit: "%",
  heading: "Percentage",
};

export const PercentageExpenditure: Dimension = {
  label: "percentage of expenditure",
  value: "PercentExpenditure",
  unit: "%",
  heading: "Percentage",
};
export const PercentageIncome: Dimension = {
  label: "percentage of income",
  value: "PercentIncome",
  unit: "%",
  heading: "Percentage",
};
export const Total: Dimension = {
  label: "total",
  value: "Total",
  heading: "Count",
};
export const HeadcountPerFTE: Dimension = {
  label: "headcount per FTE",
  value: "HeadcountPerFte",
  heading: "Ratio",
};
export const PercentageOfWorkforce: Dimension = {
  label: "percentage of workforce",
  value: "PercentWorkforce",
  unit: "%",
  heading: "Percentage",
};
export const PupilsPerStaffRole: Dimension = {
  label: "pupils per staff role",
  value: "PupilsPerStaffRole",
  heading: "Pupils per staff role",
};

export const CostCategories = [
  PoundsPerPupil,
  Actual,
  PercentageExpenditure,
  PercentageIncome,
];
export const PremisesCategories = [
  PoundsPerMetreSq,
  Actual,
  PercentageExpenditure,
  PercentageIncome,
];
export const WorkforceCategories = [
  Total,
  HeadcountPerFTE,
  PercentageOfWorkforce,
  PupilsPerStaffRole,
];

export function CalculateCostValue(costValue: CostValue): number {
  switch (costValue.dimension) {
    case PoundsPerPupil.value:
      return costValue.value / Number(costValue.numberOfPupils);
    case PercentageExpenditure.value:
      return (costValue.value / costValue.totalExpenditure) * 100;
    case PercentageIncome.value:
      return (costValue.value / costValue.totalIncome) * 100;
    case Actual.value:
      return costValue.value;
    default:
      return 0;
  }
}

export function CalculatePremisesValue(premisesValue: PremisesValue): number {
  switch (premisesValue.dimension) {
    case PercentageExpenditure.value:
      return (premisesValue.value / premisesValue.totalExpenditure) * 100;
    case PercentageIncome.value:
      return (premisesValue.value / premisesValue.totalIncome) * 100;
    case Actual.value:
      return premisesValue.value;
    default:
      return 0;
  }
}
