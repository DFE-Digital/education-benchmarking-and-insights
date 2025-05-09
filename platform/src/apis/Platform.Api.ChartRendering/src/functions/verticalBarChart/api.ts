import { ApiMapper } from "ts-oas";
import { VerticalBarChartPayload } from ".";
import { ChartBuilderResult } from "..";

export type GetVerticalBarChartApi = ApiMapper<{
  path: "/verticalBarChart";
  method: "POST";
  body: VerticalBarChartPayload;
  responses: {
    /**
     * @contentType application/json
     */
    "200": ChartBuilderResult | string;
    /**
     * @contentType application/json
     */
    "400": { error: string; errors?: string[] };
    /**
     * @contentType application/json
     */
    "500": { error: string };
  };
}>;
