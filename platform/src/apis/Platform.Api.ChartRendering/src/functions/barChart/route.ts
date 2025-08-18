import { app } from "@azure/functions";
import { barChart } from "./function";

app.http("barChart", {
  methods: ["GET"],
  authLevel: "admin",
  handler: barChart,
});
