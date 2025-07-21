type ChartDefinition = {
  keyField: string;
  valueField: string;
};

/**
 * @examples require('../openapi/verticalBarChart/examples.ts').examples
 */
export type VerticalBarChartDefinition = Pick<
  VerticalBarChartBuilderOptions<unknown>,
  "data"
> &
  Partial<Pick<VerticalBarChartBuilderOptions<unknown>, "height">> &
  Partial<
    Pick<ChartBuilderOptions<unknown>, "highlightKey" | "id" | "sort" | "width">
  > &
  ChartDefinition;

/**
 * @examples require('../openapi/horizontalBarChart/examples.ts').examples
 */
export type HorizontalBarChartDefinition = Pick<
  HorizontalBarChartBuilderOptions<unknown>,
  "data"
> &
  Partial<
    Pick<
      HorizontalBarChartBuilderOptions<unknown>,
      "barHeight" | "labelField" | "labelFormat" | "linkFormat" | "valueFormat"
    >
  > &
  Partial<
    Pick<ChartBuilderOptions<unknown>, "highlightKey" | "id" | "sort" | "width">
  > &
  ChartDefinition;

type ChartBuilderOptions<T> = {
  data: T[];
  highlightKey?: string | undefined;
  id: string;
  keyField: keyof T;
  sort?: "asc" | "desc" | undefined;
  valueField: keyof T;
  width: number;
};

export type VerticalBarChartBuilderOptions<T> = ChartBuilderOptions<T> & {
  height: number;
};

export type HorizontalBarChartBuilderOptions<T> = ChartBuilderOptions<T> & {
  barHeight: number;
  labelField: keyof T;
  labelFormat: string; // %1 = key, %2 = label
  linkFormat: string; // %1 = key
  valueFormat: string; // see https://d3js.org/d3-format#locale_format
};

type ChartBuilderResult = {
  id: string;
  html: string | undefined;
};
