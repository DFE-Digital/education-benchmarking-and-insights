import { v4 as uuidv4 } from "uuid";
import { ExpenditureData } from "src/services";

export class EstablishmentsApi {
  static async getExpenditure(
    type: string,
    id: string,
    phase?: string
  ): Promise<ExpenditureData[]> {
    const params = new URLSearchParams({
      type: type,
      id: id,
    });

    if (phase) {
      params.append("phase", phase);
    }

    return fetch("/api/establishments/expenditure?" + params, {
      redirect: "manual",
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        "X-Correlation-ID": uuidv4(),
      },
    })
      .then((res) => res.json())
      .then((res) => {
        if (res.error) {
          throw res.error;
        }

        return res;
      });
  }
}
