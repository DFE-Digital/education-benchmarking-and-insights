import { Census } from "src/services/types";
import { v4 as uuidv4 } from "uuid";

export class CensusApi {
  static async history(id: string, dimension: string): Promise<Census[]> {
    return fetch(
      "/api/census/history?" +
        new URLSearchParams({ id: id, dimension: dimension }),
      {
        redirect: "manual",
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          "X-Correlation-ID": uuidv4(),
        },
      }
    )
      .then((res) => res.json())
      .then((res) => {
        if (res.error) {
          throw res.error;
        }

        return res;
      });
  }

  static async query(
    type: string,
    id: string,
    dimension: string,
    category: string
  ): Promise<Census[]> {
    return fetch(
      "/api/census?" +
        new URLSearchParams({
          type: type,
          id: id,
          dimension: dimension,
          category: category,
        }),
      {
        redirect: "manual",
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          "X-Correlation-ID": uuidv4(),
        },
      }
    )
      .then((res) => res.json())
      .then((res) => {
        if (res.error) {
          throw res.error;
        }

        return res;
      });
  }
}
