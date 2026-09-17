import { app } from "@azure/functions";
import { lineChart } from "./function";

app.http("lineChart", {
  methods: ["POST"],
  authLevel: "admin",
  handler: lineChart,
});
