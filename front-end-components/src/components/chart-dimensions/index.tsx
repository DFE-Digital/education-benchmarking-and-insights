import { Dimension } from "src/components/chart-dimensions";

/* eslint-disable react-refresh/only-export-components */
export * from "src/components/chart-dimensions/types";
export * from "src/components/chart-dimensions/component";

export const PoundsPerPupil: Dimension = {
  label: "£ per pupil",
  value: "PerUnit",
  unit: "currency",
  heading: "Amount",
};
export const PoundsPerMetreSq: Dimension = {
  label: "£ per m²",
  value: "PerUnit",
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
export const CensusCategories = [
  Total,
  HeadcountPerFTE,
  PercentageOfWorkforce,
  PupilsPerStaffRole,
];
