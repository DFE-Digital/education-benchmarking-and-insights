/* eslint-disable no-console */
import { ATTR_SERVICE_NAME } from "@opentelemetry/semantic-conventions";
import { resourceFromAttributes } from "@opentelemetry/resources";
import { NodeSDK } from "@opentelemetry/sdk-node";
import {
  NodeTracerProvider,
  BatchSpanProcessor,
  ConsoleSpanExporter,
  SpanExporter,
} from "@opentelemetry/sdk-trace-node";
import { getNodeAutoInstrumentations } from "@opentelemetry/auto-instrumentations-node";
import { AzureMonitorTraceExporter } from "@azure/monitor-opentelemetry-exporter";

let sdk: NodeSDK | undefined;

try {
  const connectionString = process.env.APPLICATIONINSIGHTS_CONNECTION_STRING;
  const roleName = process.env.ROLE_NAME;

  const traceExporter: SpanExporter = connectionString
    ? new AzureMonitorTraceExporter({
        connectionString,
      })
    : new ConsoleSpanExporter();

  const resource = resourceFromAttributes({
    [ATTR_SERVICE_NAME]: roleName ?? "ebis-shutter",
  });

  const tracerProvider = new NodeTracerProvider({
    resource,
    spanProcessors: [
      new BatchSpanProcessor(traceExporter, {
        exportTimeoutMillis: 5000,
        maxQueueSize: 1000,
      }),
    ],
  });

  tracerProvider.register();

  sdk = new NodeSDK({
    resource,
    traceExporter,
    instrumentations: [getNodeAutoInstrumentations()],
  });

  sdk.start();
  console.debug("Tracing configured successfully");
} catch (error) {
  console.warn("Error configuring tracing", error);
}

process.on("SIGTERM", () => {
  if (sdk) {
    sdk
      .shutdown()
      .then(() => console.debug("Tracing terminated successfully"))
      .catch(error => console.warn("Error terminating tracing", error));
  }
});
