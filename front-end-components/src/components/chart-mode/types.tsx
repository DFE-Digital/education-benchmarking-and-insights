import React from "react";

export type ChartModeProps = {
  displayMode: string;
  handleChange: React.ChangeEventHandler<HTMLInputElement>;
  prefix?: string;
};

export const ChartModeChart = "Chart";
export const ChartModeTable = "Table";
