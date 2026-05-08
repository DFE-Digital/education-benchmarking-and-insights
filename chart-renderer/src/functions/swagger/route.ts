import { app } from "@azure/functions";
import { openApi, swagger } from "./function";

app.http("openapi", {
  route: "openapi.json",
  methods: ["GET"],
  authLevel: "admin",
  handler: openApi,
});

app.http("swagger", {
  route: "swagger/{*swaggerAsset}",
  methods: ["GET"],
  authLevel: "admin",
  handler: swagger,
});
