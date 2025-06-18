import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";

export type LocalAuthorityRankData = {
  laCode: string;
  laName: string;
  value: number;
};

export type LaNationalRankChartProps = {
  title: string;
  summary: string;
  prefix: string;
  valueLabel: string;
  chartData: HorizontalBarChartWrapperData<LocalAuthorityRankData>;
  notInRanking: boolean;
};
