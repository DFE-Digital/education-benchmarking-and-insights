import { Workforce } from "src/services/types";
import { v4 as uuidv4 } from "uuid";

export class WorkforceApi {
  static async history(id: string, dimension: string): Promise<Workforce[]> {
    return fetch(
      "/api/workforce/history?" +
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
  ): Promise<Workforce[]> {
    return fetch(
      "/api/workforce?" +
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
