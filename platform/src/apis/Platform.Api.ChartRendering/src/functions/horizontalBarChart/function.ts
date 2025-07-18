import {
  HttpRequest,
  HttpResponseInit,
  InvocationContext,
} from "@azure/functions";
import { Piscina } from "piscina";
import appInsights from "applicationinsights";
import { HorizontalBarChartDefinition, ChartBuilderResult } from "..";
import { HorizontalBarChartPayload } from ".";
import { validatePayload } from "./validator";

const client = new appInsights.TelemetryClient();

const piscina = new Piscina<
  { definitions: HorizontalBarChartDefinition[] },
  ChartBuilderResult[]
>({
  filename: "./dist/src/functions/horizontalBarChart/worker.js",
});

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
    charts = await piscina.run({
      definitions,
    });
  } catch (e) {
    context.error(e);

    try {
      client.trackDependency({
        name: "horizontalBarChartWorker",
        duration: Date.now() - startTime,
        success: false,
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
    console.debug(body);
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
    client.trackDependency({
      name: "horizontalBarChartWorker",
      duration: Date.now() - startTime,
      success: true,
    });
  } catch (e) {
    context.warn(e);
  }

  return result;
}
