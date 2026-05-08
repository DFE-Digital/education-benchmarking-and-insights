import { app } from "@azure/functions";
import { horizontalBarChartDom } from "./function";

app.http("horizontalBarChartDom", {
  route: "horizontalBarChart/dom",
  methods: ["POST"],
  authLevel: "admin",
  handler: horizontalBarChartDom,
});
