import { app } from "@azure/functions";
import { hBarChart } from "./function";

app.http("hBarChart", {
  methods: ["GET"],
  authLevel: "admin",
  handler: hBarChart,
});
