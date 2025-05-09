import { HttpResponseInit } from "@azure/functions";

export async function healthCheck(): Promise<HttpResponseInit> {
  return { body: "Healthy" };
}
