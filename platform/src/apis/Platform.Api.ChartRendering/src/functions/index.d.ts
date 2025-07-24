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
      | "barHeight"
      | "labelField"
      | "labelFormat"
      | "linkFormat"
      | "valueFormat"
      | "xAxisLabel"
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
  /**
   * `sprintf` format to use for labels on y-axis of the chart, where `%1` is the key and `%2` is the label
   * @example %2$s
   * @ref https://www.npmjs.com/package/sprintf-js#format-specification
   */
  labelFormat: string;
  /**
   * `sprintf` format to use for rendering y-axis labels as links on the chart, where `%1` is the key
   * @example /school/%1$s
   * @ref https://www.npmjs.com/package/sprintf-js#format-specification
   */
  linkFormat: string;
  /**
   * `d3-format` format to use for rendering values on the chart
   * @example $,~s
   * @ref https://d3js.org/d3-format#locale_format
   */
  valueFormat: string;
  xAxisLabel: string;
};

type ChartBuilderResult = {
  id: string;
  html: string | undefined;
};
