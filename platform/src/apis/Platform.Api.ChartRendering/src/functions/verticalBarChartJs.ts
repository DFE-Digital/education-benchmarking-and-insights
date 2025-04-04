import {
  app,
  HttpRequest,
  HttpResponseInit,
  InvocationContext,
} from "@azure/functions";
import { ChartDefinition, VerticalBarChartPayload } from ".";
import { ChartJsBuilderResult } from "../builders";
import appInsights from "applicationinsights";
import { v4 as uuidv4 } from "uuid";
import VerticalBarChartJsBuilder from "../builders/verticalBarChartJsBuilder.js";
const client = new appInsights.TelemetryClient();
const builder = new VerticalBarChartJsBuilder();

export async function verticalBarChartJs(
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

  let charts: ChartJsBuilderResult[] = [];
  const definitions = Array.isArray(payload) ? payload : [payload];

  try {
    charts = definitions.map(
      ({ data, height, id, keyField, valueField, width, ...rest }) =>
        builder.buildChart({
          data,
          height: height || 500,
          id: id || uuidv4(),
          keyField: keyField as never,
          valueField: valueField as never,
          width: width || 928,
          ...rest,
        }),
    );
  } catch (e) {
    context.error(e);

    try {
      client.trackDependency({
        name: "verticalBarChartBuilder",
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
      jsonBody: charts.map((c) => ({
        id: c.id,
        html: Buffer.from(c.buffer).toString(),
      })),
    };
  } else if (request.headers.get("x-accept") === "image/svg+xml") {
    // for single chart requests with HTML requested, just return the chart element
    const body = charts[0].buffer;
    return {
      body,
      headers: {
        "Content-Length": body.length.toString(),
        "Content-Type": "image/svg+xml",
      },
    };
  } else {
    result = {
      jsonBody: {
        id: charts[0].id,
        html: Buffer.from(charts[0].buffer).toString(),
      },
    };
  }

  try {
    client.trackDependency({
      name: "verticalBarChartBuilder",
      duration: Date.now() - startTime,
      success: true,
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

app.http("verticalBarChartJs", {
  methods: ["POST"],
  authLevel: "anonymous",
  handler: verticalBarChartJs,
});
