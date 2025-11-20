export type ProgressBandingTagProps = {
  banding: ProgressBanding;
};

export enum ProgressBanding {
  WellBelowAverage = "WellBelowAverage",
  BelowAverage = "BelowAverage",
  Average = "Average",
  AboveAverage = "AboveAverage",
  WellAboveAverage = "WellAboveAverage",
}

export type KS4ProgressBanding = {
  urn: string;
  banding: ProgressBanding;
};

export type ProgressIndicators = KS4ProgressBanding[];
