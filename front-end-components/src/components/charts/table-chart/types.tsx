export type TableChartProps<TData extends SchoolChartData> = {
  tableHeadings: string[];
  data?: TData[];
};

export type SchoolChartData = {
  urn: string;
  name: string;
  value: number;
  schoolType: string;
  localAuthority: string;
  numberOfPupils: bigint;
};
