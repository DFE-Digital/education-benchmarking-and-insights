import { BudgetForecastReturn } from "./types";
import { v4 as uuidv4 } from "uuid";

export class BudgetForecastApi {
  static async budgetForecastReturns(
    id: string,
    signals?: AbortSignal[]
  ): Promise<BudgetForecastReturn[]> {
    const params = new URLSearchParams({
      companyNumber: id,
    });

    return fetch("/api/budget-forecast?" + params, {
      redirect: "manual",
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        "X-Correlation-ID": uuidv4(),
      },
      signal: signals?.length ? AbortSignal.any(signals) : undefined,
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
