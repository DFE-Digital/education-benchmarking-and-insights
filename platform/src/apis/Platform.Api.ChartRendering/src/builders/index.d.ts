import { JSDOM } from "jsdom";

export type ChartBuilderOptions<T> = {
  data: T[];
  height: number;
  highlightKey?: string | undefined;
  id: string;
  jsDom: JSDOM;
  keyField: keyof T;
  sort?: "asc" | "desc" | undefined;
  valueField: keyof T;
  width: number;
};

export type ChartBuilderResult = {
  id: string;
  html: string | undefined;
};
