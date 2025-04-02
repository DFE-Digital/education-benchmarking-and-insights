import appInsights from "applicationinsights";
import { app } from "@azure/functions";

appInsights.start();
app.setup({
  enableHttpStream: true,
});
