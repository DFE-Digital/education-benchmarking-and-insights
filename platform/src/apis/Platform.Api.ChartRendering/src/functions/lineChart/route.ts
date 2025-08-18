import { app } from "@azure/functions";
import { lineChart } from "./function";

app.http("lineChart", {
  methods: ["GET"],
  authLevel: "admin",
  handler: lineChart,
});
