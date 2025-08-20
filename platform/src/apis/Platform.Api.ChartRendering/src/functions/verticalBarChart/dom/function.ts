import {
  HttpRequest,
  HttpResponseInit,
  InvocationContext,
} from "@azure/functions";
import { ChartBuilderResult } from "../..";
import { VerticalBarChartPayload } from "..";
import { v4 as uuidv4 } from "uuid";
import VerticalBarChartBuilder from "./builder";

export async function verticalBarChartDom(
  request: HttpRequest,
  context: InvocationContext,
): Promise<HttpResponseInit> {
  const verticalBarChartBuilder = new VerticalBarChartBuilder();

  context.debug(`Received HTTP request for vertical bar chart`);

  const payload = (await request.json()) as VerticalBarChartPayload;

  let charts: ChartBuilderResult[] = [];
  const definitions = Array.isArray(payload) ? payload : [payload];
  const buildChartPromises = definitions.map(
    ({ data, height, id, keyField, valueField, width, ...rest }) =>
      verticalBarChartBuilder.buildChart({
        data,
        height: height || 500,
        id: id || uuidv4(),
        keyField: keyField as never,
        valueField: valueField as never,
        width: width || 928,
        ...rest,
      }),
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
