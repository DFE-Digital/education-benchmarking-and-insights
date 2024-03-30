import { Balance, Workforce } from "src/services/types";
import { v4 as uuidv4 } from "uuid";

export class HistoryApi {
  static async getBalance(
    type: string,
    id: string,
    dimension: string
  ): Promise<Balance[]> {
    return fetch(
      "/api/establishments/balance/history?" +
        new URLSearchParams({ type: type, id: id, dimension: dimension }),
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

  static async getWorkforce(
    type: string,
    id: string,
    dimension: string
  ): Promise<Workforce[]> {
    return fetch(
      "/api/establishments/workforce/history?" +
        new URLSearchParams({ type: type, id: id, dimension: dimension }),
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
