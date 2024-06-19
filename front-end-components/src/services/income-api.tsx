import { Income } from "src/services/types";
import { v4 as uuidv4 } from "uuid";

export class IncomeApi {
  static async history(
    type: string,
    id: string,
    dimension: string,
    excludeCentralServices?: boolean
  ): Promise<Income[]> {
    const params = new URLSearchParams({
      type: type,
      id: id,
      dimension: dimension,
    });
    if (excludeCentralServices !== undefined) {
      params.append(
        "excludeCentralServices",
        excludeCentralServices ? "true" : "false"
      );
    }

    return fetch("/api/income/history?" + params, {
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
