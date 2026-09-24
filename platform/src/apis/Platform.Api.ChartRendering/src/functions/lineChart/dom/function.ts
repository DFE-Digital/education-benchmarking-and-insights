import {
  HttpRequest,
  HttpResponseInit,
  InvocationContext,
} from "@azure/functions";
import { ChartBuilderResult } from "../..";
import { LineChartPayload } from "..";
import { v4 as uuidv4 } from "uuid";
import LineChartBuilder from "./builder";

export async function lineChartDom(
  request: HttpRequest,
  context: InvocationContext
): Promise<HttpResponseInit> {
  const lineChartBuilder = new LineChartBuilder();

  context.debug(`Received HTTP request for line chart using D3 in the DOM`);

  const payload = (await request.json()) as LineChartPayload;
  const definitions = Array.isArray(payload) ? payload : [payload];

  const buildChartPromises = definitions.map(
    ({
      id,
      width,
      height,
      data,
      keyField,
      valueField,
      xAxisLabel,
      showValueDots,
      showValueLabels,
      valueType,
      ...rest
    }) =>
      lineChartBuilder.buildChart({
        id: id || uuidv4(),
        width: width || 928,
        height: height || 300,
        data,
        keyField: keyField as never,
        valueField: valueField as never,
        xAxisLabel: xAxisLabel as never,
        showValueDots: showValueDots ?? true,
        showValueLabels: showValueLabels ?? true,
        valueType: valueType ?? "numeric",
        ...rest,
      })
  );

  let charts: ChartBuilderResult[];

  try {
    charts = await Promise.all(buildChartPromises);
  } catch (e) {
    context.error(e);

    return {
      jsonBody: { error: [(e as Error)?.message ?? e] },
      status: 500,
    };
  }

  if (Array.isArray(payload)) {
    return {
      jsonBody: charts,
    };
  }

  if (request.headers.get("x-accept") === "image/svg+xml") {
    const body = charts[0].html ?? "<svg />";
    return {
      body,
      headers: {
        "Content-Length": Buffer.byteLength(body, "utf-8").toString(),
        "Content-Type": "image/svg+xml; charset=utf-8",
      },
    };
  }

  return {
    jsonBody: charts[0],
  };
}
