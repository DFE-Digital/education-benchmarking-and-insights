import { app } from "@azure/functions";
import { horizontalBarChart } from "./function";
import { horizontalBarChartDom } from "./dom/function";

app.http("horizontalBarChart", {
  methods: ["POST"],
  authLevel: "admin",
  handler: horizontalBarChart,
});

app.http("horizontalBarChartDom", {
  route: "horizontalBarChart/dom",
  methods: ["POST"],
  authLevel: "admin",
  handler: horizontalBarChartDom,
});
