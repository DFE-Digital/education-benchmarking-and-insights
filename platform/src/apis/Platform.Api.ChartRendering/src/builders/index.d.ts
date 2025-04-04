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

export type ChartBuilderResult = {
  id: string;
  html: string | undefined;
};

export type ChartJsBuilderResult = {
  id: string;
  buffer: Buffer<ArrayBufferLike>;
};
