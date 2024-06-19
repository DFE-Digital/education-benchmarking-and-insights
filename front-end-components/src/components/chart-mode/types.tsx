export type ChartModeProps = {
  chartMode: string | undefined;
  handleChange: (value: string) => void;
  prefix?: string;
};

export const ChartModeChart = "Chart";
export const ChartModeTable = "Table";
