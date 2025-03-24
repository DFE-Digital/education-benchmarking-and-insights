import appInsights from "applicationinsights";
import { app } from "@azure/functions";

appInsights
  .setup()
  .setAutoCollectRequests(true)
  .setSendLiveMetrics(true)
  .start();

app.setup({
  enableHttpStream: true,
});
