import { app } from "@azure/functions";
import { verticalBarChartDom } from "./function";

app.http("verticalBarChartDom", {
  route: "verticalBarChart/dom",
  methods: ["POST"],
  authLevel: "admin",
  handler: verticalBarChartDom,
});
