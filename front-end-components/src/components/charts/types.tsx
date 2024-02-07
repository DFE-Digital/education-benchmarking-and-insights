import React from "react";

export type HorizontalBarChartWrapperProps = {
  chartName: string;
  children?: React.ReactNode[] | React.ReactNode;
  data: HorizontalBarChartWrapperData;
  sort?: ChartSortMode;
};

export type HorizontalBarChartWrapperData = {
  tableHeadings: string[];
  dataPoints: ChartDataPoint[];
};

export type ChartDataPoint = {
  school: string;
  urn: string;
  value: number;
  additionalData?: (string | bigint)[];
};

export type ChartSortMode = {
  dataPoint: Exclude<keyof ChartDataPoint, "additionalData">;
  direction: "asc" | "desc";
};
