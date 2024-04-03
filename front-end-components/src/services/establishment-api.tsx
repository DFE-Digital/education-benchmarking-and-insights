import { v4 as uuidv4 } from "uuid";
import { Expenditure, WorkforceBenchmark } from "src/services";

export class EstablishmentsApi {
  static async getExpenditure(
    type: string,
    id: string
  ): Promise<Expenditure[]> {
    return fetch(
      "/api/establishments/expenditure?" +
        new URLSearchParams({ type: type, id: id }),
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

  static async getWorkforceBenchmarkData(
    type: string,
    id: string
  ): Promise<WorkforceBenchmark[]> {
    return fetch(
      "/api/establishments/workforce?" +
        new URLSearchParams({ type: type, id: id }),
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
