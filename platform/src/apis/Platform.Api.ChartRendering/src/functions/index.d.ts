export type ChartDefinition = Pick<ChartBuilderOptions<unknown>, "data"> &
  Partial<
    Pick<
      ChartBuilderOptions<unknown>,
      "height" | "highlightKey" | "id" | "sort" | "width"
    >
  > & {
    keyField: string;
    valueField: string;
  };

export type ChartBuilderOptions<T> = {
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
