import React from "react";
import { ChartSeriesValueUnit } from "src/components";

export type Dimension = {
  label: string;
  value: string;
  heading: string;
  unit?: ChartSeriesValueUnit;
};

export type ChartDimensionsProps = {
  dimensions: Dimension[];
  handleChange: React.ChangeEventHandler<HTMLSelectElement>;
  elementId: string;
  value: string;
  label?: string;
};
