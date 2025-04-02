import {
  app,
  HttpRequest,
  HttpResponseInit,
  InvocationContext,
} from "@azure/functions";
import { ChartDefinition, VerticalBarChartPayload } from ".";
import { ChartBuilderResult } from "../builders";
import { Piscina } from "piscina";
import appInsights from "applicationinsights";
const client = new appInsights.TelemetryClient();

const piscina = new Piscina<
  { definitions: ChartDefinition[] },
  ChartBuilderResult[]
>({
  filename: "./dist/src/workers/verticalBarChartWorker.js",
});

export async function verticalBarChart(
  request: HttpRequest,
  context: InvocationContext,
): Promise<HttpResponseInit> {
  const startTime = Date.now();
  context.debug(`Received HTTP request for vertical bar chart`);

  const payload = (await request.json()) as VerticalBarChartPayload;
  if (!isValid(payload)) {
    return {
      body: "Bad request",
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
        name: "Piscina",
        duration: Date.now() - startTime,
        success: false,
      });
    } catch (e) {
      context.warn(e);
    }

    return {
      body: e,
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
    const body = charts[0].html;
    return {
      body,
      headers: {
        "Content-Length": body.length.toString(),
        "Content-Type": "image/svg+xml",
      },
    };
  } else {
    result = {
      jsonBody: charts[0],
    };
  }

  try {
    const measurements: {
      [propertyName: string]: number;
    } = {};
    Object.keys(piscina.runTime).forEach((k) => {
      const value = piscina.runTime[k] as number;
      if (!isNaN(value)) {
        measurements[k] = value;
      }
    });

    client.trackDependency({
      name: "Piscina",
      duration: Date.now() - startTime,
      success: true,
      measurements,
    });
  } catch (e) {
    context.warn(e);
  }

  return result;
}

// todo: more validation, such as ensuring if array that id has been defined for each entry
function isValid(payload: VerticalBarChartPayload) {
  if (Array.isArray(payload)) {
    return !!(payload as ChartDefinition[])[0]?.data?.length;
  }

  return !!(payload as ChartDefinition)?.data?.length;
}

app.http("verticalBarChart", {
  methods: ["POST"],
  authLevel: "anonymous",
  handler: verticalBarChart,
});
