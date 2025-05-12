import { ApiMapper } from "ts-oas";
import { VerticalBarChartPayload } from "../../functions/verticalBarChart";
import { ChartBuilderResult } from "../../functions";

/**
 * Generates a single or multiple vertical bar chart(s) based on whether payload in a single object or an array
 * @summary Builds a vertical bar chart
 * @tags Charts
 * @body.description Bar chart payload
 * @body.contentType application/json
 */
export type GetVerticalBarChartApi = ApiMapper<{
  path: "/api/verticalBarChart";
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
