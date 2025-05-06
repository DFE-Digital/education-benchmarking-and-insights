import {
  BalanceHistoryRows,
  BudgetForecastReturn,
  TrustBalance,
} from "src/services/types";
import { v4 as uuidv4 } from "uuid";

export class BalanceApi {
  static async history(
    type: string,
    id: string,
    dimension: string,
    signals?: AbortSignal[]
  ): Promise<BalanceHistoryRows> {
    const params = new URLSearchParams({
      type: type,
      id: id,
      dimension: dimension,
    });

    const response = await fetch("/api/balance/history?" + params, {
      redirect: "manual",
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        "X-Correlation-ID": uuidv4(),
      },
      signal: signals?.length ? AbortSignal.any(signals) : undefined,
    });

    const json = await response.json();
    if (json.error) {
      throw json.error;
    }

    return json;
  }

  static async trust(
    id: string,
    dimension: string,
    excludeCentralServices?: boolean,
    signals?: AbortSignal[]
  ): Promise<TrustBalance[]> {
    const params = new URLSearchParams({
      type: "trust",
      id: id,
      dimension: dimension,
    });

    if (excludeCentralServices !== undefined) {
      params.append(
        "excludeCentralServices",
        excludeCentralServices ? "true" : "false"
      );
    }

    return fetch("/api/balance/user-defined?" + params, {
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
