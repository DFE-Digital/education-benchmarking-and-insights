import { app } from "@azure/functions";
import { verticalBarChart } from "./function";
import { verticalBarChartDom } from "./dom/function";

app.http("verticalBarChart", {
  methods: ["POST"],
  authLevel: "admin",
  handler: verticalBarChart,
});

app.http("verticalBarChartDom", {
  route: "verticalBarChart/dom",
  methods: ["POST"],
  authLevel: "admin",
  handler: verticalBarChartDom,
});
