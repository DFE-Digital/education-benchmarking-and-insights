import {
  HttpRequest,
  HttpResponseInit,
  InvocationContext,
} from "@azure/functions";
import appInsights from "applicationinsights";
import { ChartBuilderResult } from "..";
import { HorizontalBarChartPayload } from ".";
import { validatePayload } from "./validator";
import piscina from "../shared/pool";

const client = new appInsights.TelemetryClient();

export async function horizontalBarChart(
  request: HttpRequest,
  context: InvocationContext,
): Promise<HttpResponseInit> {
  const startTime = Date.now();
  context.debug(`Received HTTP request for horizontal bar chart`);

  let payload: HorizontalBarChartPayload | undefined;
  try {
    payload = (await request.json()) as HorizontalBarChartPayload;
  } catch (e) {
    return {
      jsonBody: {
        error: "Bad request",
        errors: [(e as Error)?.message ?? e],
      },
      status: 400,
    };
  }

  const validationErrors = validatePayload(payload);
  if (validationErrors.length > 0) {
    return {
      jsonBody: { error: "Bad request", errors: validationErrors },
      status: 400,
    };
  }

  let charts: ChartBuilderResult[] = [];
  const definitions = Array.isArray(payload) ? payload : [payload];

  try {
    charts = await piscina.run({ definitions }, { name: "HorizontalBarChart" });
  } catch (e) {
    context.error(e);

    try {
      const duration = Date.now() - startTime;
      client.trackDependency({
        name: "horizontalBarChartWorker",
        duration: duration,
        success: false,
        time: new Date(startTime),
      });
      // eslint-disable-next-line @typescript-eslint/no-unused-vars
    } catch (_e) {
      // do not pollute logs with dependency tracking issues within this exception block
    }

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

  try {
    const duration = Date.now() - startTime;
    client.trackDependency({
      name: "horizontalBarChartWorker",
      duration: duration,
      success: true,
      time: new Date(startTime),
    });
  } catch (e) {
    context.warn(e);
  }

  return result;
}
