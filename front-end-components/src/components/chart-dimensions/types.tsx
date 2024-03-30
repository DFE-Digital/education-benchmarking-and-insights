import React from "react";

export type Dimension = {
  label: string;
  value: string;
};

export type CostValue = {
  dimension: string;
  totalExpenditure: number;
  totalIncome: number;
  numberOfPupils: bigint;
  value: number;
};

export type PremisesValue = {
  dimension: string;
  totalExpenditure: number;
  totalIncome: number;
  value: number;
};

export type WorkforceValue = {
  dimension: string;
  schoolWorkforceFTE: number;
  numberOfPupils: bigint;
  value: number;
  schoolWorkforceHeadcount: number;
};

export type ChartDimensionsProps = {
  dimensions: Dimension[];
  handleChange: React.ChangeEventHandler<HTMLSelectElement>;
  elementId: string;
  defaultValue: string;
};
