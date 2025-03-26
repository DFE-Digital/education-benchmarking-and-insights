import {
  app,
  HttpRequest,
  HttpResponseInit,
  InvocationContext,
} from "@azure/functions";
import { v4 as uuidv4 } from "uuid";
import VerticalBarChartBuilder from "../builders/verticalBarChartBuilder.js";
import { ChartDefinition, VerticalBarChartPayload } from ".";
import { ChartBuilderResult } from "../builders";

const builder = new VerticalBarChartBuilder();

export async function verticalBarChart(
  request: HttpRequest,
  context: InvocationContext,
): Promise<HttpResponseInit> {
  context.log(`Http function processed request for url "${request.url}"`);

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
    charts = definitions.map(
      ({ data, height, id, keyField, valueField, width, ...rest }) =>
        builder.buildChart({
          context,
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

    return {
      body: e,
      status: 500,
    };
  }

  if (Array.isArray(payload)) {
    return {
      jsonBody: charts,
    };
  }

  // for single chart requests with HTML requested, just return the chart element
  if (request.headers.get("x-accept") === "image/svg+xml") {
    const body = charts[0].html;
    return {
      body,
      headers: {
        "Content-Length": body.length.toString(),
        "Content-Type": "image/svg+xml",
      },
    };
  }

  return {
    jsonBody: charts[0],
  };
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
