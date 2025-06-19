/* eslint-disable react-refresh/only-export-components */
export * from "src/views/la-national-rank/types";
export * from "src/views/la-national-rank/view";

export const plannedExpenditureSummary = (year: string): string => {
  return `Outturn includes recoupment for the financial year ending March ${year}.`;
};
export const plannedExpenditureTitle = "planned-expenditure";
export const plannedExpenditureValueLabel =
  "Outturn as a percentage of planned expenditure";

export const fundingSummary = (year: string): string => {
  return `Outturn includes recoupment for the financial year ending March ${year}. Outturn more than 100% indicates that spend is greater than the funding allocation.`;
};
export const fundingTitle = "funding";
export const fundingValueLabel = "Outturn as a percentage of funding";
