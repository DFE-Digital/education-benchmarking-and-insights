export type ChartModeProps = {
  chartMode: string | undefined;
  handleChange: (value: string) => void;
  prefix?: string;
  stacked?: boolean;
};

export const ChartModeChart = "Chart";
export const ChartModeTable = "Table";
