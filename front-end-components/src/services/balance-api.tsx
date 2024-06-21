import {
  BudgetForecastReturn,
  SchoolBalanceHistory,
  TrustBalance,
} from "src/services/types";
import { v4 as uuidv4 } from "uuid";

export class BalanceApi {
  static async history(
    type: string,
    id: string,
    dimension: string
  ): Promise<SchoolBalanceHistory[]> {
    const params = new URLSearchParams({
      type: type,
      id: id,
      dimension: dimension,
    });

    return fetch("/api/balance/history?" + params, {
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

  static async trust(
    id: string,
    dimension: string,
    excludeCentralServices?: boolean
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
    id: string
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
