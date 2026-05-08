import { ApiMapper } from "ts-oas";

/**
 * @tags Health Check
 */
export type GetHealthApi = ApiMapper<{
  path: "/api/health";
  method: "GET";
  responses: {
    /**
     * @contentType text/plain
     */
    "200": string;
  };
}>;
