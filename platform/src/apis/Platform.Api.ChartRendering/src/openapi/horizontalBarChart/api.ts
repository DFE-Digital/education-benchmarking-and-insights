import { ApiMapper } from "ts-oas";
import { HorizontalBarChartPayload } from "../../functions/horizontalBarChart";
import { ChartBuilderResult } from "../../functions";

/**
 * Generates a single or multiple horizontal bar chart(s) based on whether payload in a single object or an array
 * @summary Builds a horizontal bar chart
 * @tags Charts
 * @body.description Bar chart payload
 * @body.contentType application/json
 */
export type GetHorizontalBarChartApi = ApiMapper<{
  path: "/api/horizontalBarChart";
  method: "POST";
  body: HorizontalBarChartPayload;
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
 * Generates a single or multiple horizontal bar chart(s) based on whether payload in a single object or an array
 * @summary Builds a horizontal bar chart using D3 in the DOM
 * @tags DOM
 * @body.description Bar chart payload
 * @body.contentType application/json
 */
export type GetHorizontalBarChartDomApi = ApiMapper<{
  path: "/api/horizontalBarChart/dom";
  method: "POST";
  body: HorizontalBarChartPayload;
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
