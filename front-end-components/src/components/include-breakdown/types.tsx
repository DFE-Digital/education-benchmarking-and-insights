import React from "react";

export type IncludeBreakdownProps = {
  breakdown: string | undefined;
  handleChange: React.ChangeEventHandler<HTMLInputElement>;
  prefix?: string;
};

export const BreakdownInclude = "Include";
export const BreakdownExclude = "Exclude";
