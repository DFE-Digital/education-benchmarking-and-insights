import { app } from "@azure/functions";
import { verticalBarChart } from "./function";

app.http("verticalBarChart", {
  methods: ["POST"],
  authLevel: "anonymous",
  handler: verticalBarChart,
});
