import { ApiMapper } from "ts-oas";
import { LineChartPayload } from "../../functions/lineChart";
import { ChartBuilderResult } from "../../functions";

/**
 * Generates a single or multiple line chart(s) based on whether payload is a single object or an array
 * @summary Builds a line chart
 * @tags Charts
 * @body.description Line chart payload
 * @body.contentType application/json
 */
export type GetLineChartApi = ApiMapper<{
  path: "/api/lineChart";
  method: "POST";
  body: LineChartPayload;
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

/**
 * Generates a single or multiple line chart(s) based on whether payload is a single object or an array
 * @summary Builds a line chart using D3 in the DOM
 * @tags DOM
 * @body.description Line chart payload
 * @body.contentType application/json
 */
export type GetLineChartDomApi = ApiMapper<{
  path: "/api/lineChart/dom";
  method: "POST";
  body: LineChartPayload;
  responses: {
    /**
     * @contentType application/json
     */
    "200": ChartBuilderResult | string;
    /**
     * @contentType application/json
     */
    "500": { error: string };
  };
}>;
