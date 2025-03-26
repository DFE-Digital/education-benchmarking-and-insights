import { ChartBuilderOptions } from "../builders";

type VerticalBarChartPayload = ChartDefinition | ChartDefinition[];

type ChartDefinition = Pick<ChartBuilderOptions<unknown>, "data"> &
  Partial<
    Pick<
      ChartBuilderOptions<unknown>,
      "height" | "highlightKey" | "id" | "sort" | "width"
    >
  > & {
    keyField: string;
    valueField: string;
  };
