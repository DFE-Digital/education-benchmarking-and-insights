import { app } from "@azure/functions";
import { lineChartDom } from "./function";

app.http("lineChartDom", {
  route: "lineChart/dom",
  methods: ["POST"],
  authLevel: "admin",
  handler: lineChartDom,
});
