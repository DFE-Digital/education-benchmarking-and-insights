import { app } from "@azure/functions";
import { healthCheck } from "./healthCheck";

app.http("health", {
  methods: ["GET"],
  authLevel: "anonymous",
  handler: healthCheck,
});
