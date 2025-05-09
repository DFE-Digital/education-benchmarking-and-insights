import { ApiMapper } from "ts-oas";

export type GetHealthApi = ApiMapper<{
  path: "/health";
  method: "GET";
  responses: {
    /**
     * @contentType text/plain
     */
    "200": string;
  };
}>;
