import { LocalAuthorityNationalRanking } from "src/services";

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
  rankingApiParam: LocalAuthorityNationalRanking;
};
