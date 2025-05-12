import { app } from "@azure/functions";
import { healthCheck } from "./function";

app.http("health", {
  methods: ["GET"],
  authLevel: "anonymous",
  handler: healthCheck,
});
