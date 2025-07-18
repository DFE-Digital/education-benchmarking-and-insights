import { app } from "@azure/functions";
import { horizontalBarChart } from "./function";

app.http("horizontalBarChart", {
  methods: ["POST"],
  authLevel: "admin",
  handler: horizontalBarChart,
});
