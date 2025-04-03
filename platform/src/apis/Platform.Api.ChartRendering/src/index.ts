import appInsights from "applicationinsights";
import { app } from "@azure/functions";

appInsights.setup().start();
app.setup({
  enableHttpStream: true,
});
