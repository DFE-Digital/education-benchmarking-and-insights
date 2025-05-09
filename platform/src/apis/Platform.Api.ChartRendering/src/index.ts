import appInsights from "applicationinsights";
import { app } from "@azure/functions";

appInsights
  .setup()
  .setAutoCollectRequests(true)
  .setSendLiveMetrics(false)
  .start();

app.setup({
  enableHttpStream: true,
});
