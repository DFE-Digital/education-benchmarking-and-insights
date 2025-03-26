import { InvocationContext } from "@azure/functions";

type ChartBuilderOptions<T> = {
  context: InvocationContext;
  data: T[];
  height: number;
  highlightKey?: string | undefined;
  id: string;
  keyField: keyof T;
  sort?: "asc" | "desc" | undefined;
  valueField: keyof T;
  width: number;
};

type ChartBuilderResult = {
  id: string;
  html: string | undefined;
};
