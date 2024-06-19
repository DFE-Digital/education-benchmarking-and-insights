export type CentralServicesBreakdownProps = {
  breakdown: string | undefined;
  handleChange: (value: string) => void;
  prefix?: string;
};

export const BreakdownInclude = "Include";
export const BreakdownExclude = "Exclude";
