import {
  HttpRequest,
  HttpResponseInit,
  InvocationContext,
} from "@azure/functions";
import { ChartBuilderResult } from "../..";
import { HorizontalBarChartPayload } from "..";
import { v4 as uuidv4 } from "uuid";
import HorizontalBarChartBuilder from "./builder";

export async function horizontalBarChartDom(
  request: HttpRequest,
  context: InvocationContext
): Promise<HttpResponseInit> {
  const horizontalBarChartBuilder = new HorizontalBarChartBuilder();

  context.debug(
    `Received HTTP request for horizontal bar chart using D3 in the DOM`
  );

  const payload = (await request.json()) as HorizontalBarChartPayload;

  let charts: ChartBuilderResult[] = [];
  const definitions = Array.isArray(payload) ? payload : [payload];
  const buildChartPromises = definitions.map(
    ({
      barHeight,
      data,
      id,
      keyField,
      labelField,
      labelFormat,
      linkFormat,
      valueField,
      valueType,
      width,
      xAxisLabel,
      ...rest
    }) =>
      horizontalBarChartBuilder.buildChart({
        barHeight: barHeight || 25,
        data,
        id: id || uuidv4(),
        keyField: keyField as never,
        labelField: labelField as never,
        labelFormat: labelFormat as never,
        linkFormat: linkFormat as never,
        valueField: valueField as never,
        valueType: valueType as never,
        width: width || 928,
        xAxisLabel: xAxisLabel as never,
        ...rest,
      })
  );

  try {
    charts = await Promise.all(buildChartPromises);
  } catch (e) {
    context.error(e);

    return {
      jsonBody: { error: [(e as Error)?.message ?? e] },
      status: 500,
    };
  }

  let result: HttpResponseInit;
  if (Array.isArray(payload)) {
    result = {
      jsonBody: charts,
    };
  } else if (request.headers.get("x-accept") === "image/svg+xml") {
    // for single chart requests with HTML requested, just return the chart element
    const body = charts[0].html ?? "<svg />";
    return {
      body,
      headers: {
        "Content-Length": Buffer.byteLength(body, "utf-8").toString(),
        "Content-Type": "image/svg+xml; charset=utf-8",
      },
    };
  } else {
    result = {
      jsonBody: charts[0],
    };
  }

  return result;
}
