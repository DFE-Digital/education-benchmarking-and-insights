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
      | "missingDataLabel"
      | "missingDataLabelWidth"
      | "paddingInner"
      | "paddingOuter"
      | "valueType"
      | "xAxisLabel"
    >
  > &
  Partial<
    Pick<ChartBuilderOptions<unknown>, "highlightKey" | "id" | "sort" | "width">
  > &
  ChartDefinition;

export type DatumKey = string | undefined;
export type Group = string;

type ChartBuilderOptions<T> = {
  data: T[];
  groupedKeys?: Partial<Record<Group, DatumKey[]>>;
  highlightKey?: DatumKey;
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
  missingDataLabel?: string;
  missingDataLabelWidth?: number;
  /**
   * Sets the inner padding to the specified value which must be in the range [0, 1].
   * The inner padding determines the ratio of the range that is reserved for blank space between bands.
   * @default 0.2
   */
  paddingInner?: number;
  /**
   * Sets the outer padding to the specified value which must be in the range [0, 1].
   * The outer padding determines the ratio of the range that is reserved for blank space before the first band and after the last band.
   * @default 0.1
   */
  paddingOuter?: number;
  /**
   * Describes how values on the chart should be interpreted and formatted.
   * This determines both the visual representation (e.g. percentage or currency)
   * and any necessary normalisation (e.g. dividing percent values by 100).
   * Must be one of the predefined `ValueType` options.
   * @example "Percent" | "Currency"
   */
  valueType: ValueType;
  xAxisLabel: string;
};

type ChartBuilderResult = {
  id: string;
  html: string | undefined;
};

export type ValueType = "percent" | "currency";
