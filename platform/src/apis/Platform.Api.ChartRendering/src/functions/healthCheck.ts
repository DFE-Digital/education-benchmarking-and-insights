import { app, HttpResponseInit } from "@azure/functions";

export async function healthCheck(): Promise<HttpResponseInit> {
  return { body: "Healthy" };
}

app.http("health", {
  methods: ["GET"],
  authLevel: "anonymous",
  handler: healthCheck,
});
