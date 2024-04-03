import {
  CostValue,
  Dimension,
  PremisesValue,
  WorkforceValue,
} from "src/components/chart-dimensions";

/* eslint-disable react-refresh/only-export-components */
export * from "src/components/chart-dimensions/types";
export * from "src/components/chart-dimensions/component";

export const PoundsPerPupil: Dimension = {
  label: "£ per pupil",
  value: "PoundPerPupil",
  unit: "currency",
};
export const PoundsPerMetreSq: Dimension = {
  label: "£ per m²",
  value: "PoundPerSqMetre",
  unit: "currency",
};
export const Actual: Dimension = {
  label: "actuals",
  value: "Actuals",
  unit: "currency",
};
export const Percent: Dimension = {
  label: "percent",
  value: "Percent",
  unit: "%",
};

export const PercentageExpenditure: Dimension = {
  label: "percentage of expenditure",
  value: "PercentExpenditure",
  unit: "%",
};
export const PercentageIncome: Dimension = {
  label: "percentage of income",
  value: "PercentIncome",
  unit: "%",
};
export const Total: Dimension = { label: "total", value: "Total" };
export const HeadcountPerFTE: Dimension = {
  label: "headcount per FTE",
  value: "HeadcountPerFte",
};
export const PercentageOfWorkforce: Dimension = {
  label: "percentage of workforce",
  value: "PercentWorkforce",
  unit: "%",
};
export const PupilsPerStaffRole: Dimension = {
  label: "pupils per staff role",
  value: "PupilsPerStaffRole",
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

export function DimensionHeading(dimension: string): string {
  switch (dimension) {
    case PercentageExpenditure.value:
    case PercentageIncome.value:
    case PercentageOfWorkforce.value:
      return "Percentage";
    case Total.value:
      return "Count";
    case HeadcountPerFTE.value:
      return "Ratio";
    case PupilsPerStaffRole.value:
      return "Pupils per staff role";
    default:
      return "Amount";
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

export function CalculateWorkforceValue(
  workforceValue: WorkforceValue
): number {
  switch (workforceValue.dimension) {
    case Total.value:
      return workforceValue.value;
    case HeadcountPerFTE.value:
      return workforceValue.schoolWorkforceHeadcount / workforceValue.value;
    case PercentageOfWorkforce.value:
      return (workforceValue.value / workforceValue.schoolWorkforceFTE) * 100;
    case PupilsPerStaffRole.value:
      return Number(workforceValue.numberOfPupils) / workforceValue.value;
    default:
      return 0;
  }
}
