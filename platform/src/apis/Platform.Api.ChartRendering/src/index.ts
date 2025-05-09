import appInsights from "applicationinsights";
import { app } from "@azure/functions";
import { healthCheck } from "./functions/healthCheck/healthCheck";
import { verticalBarChart } from "./functions/verticalBarChart/verticalBarChart";

appInsights
  .setup()
  .setAutoCollectRequests(true)
  .setSendLiveMetrics(true)
  .start();

app.setup({
  enableHttpStream: true,
});

app.http("health", {
  methods: ["GET"],
  authLevel: "anonymous",
  handler: healthCheck,
});

app.http("verticalBarChart", {
  methods: ["POST"],
  authLevel: "anonymous",
  handler: verticalBarChart,
});
