import { app } from "@azure/functions";
import { healthCheck } from "./function";

app.http("health", {
  methods: ["GET"],
  authLevel: "admin",
  handler: healthCheck,
});
