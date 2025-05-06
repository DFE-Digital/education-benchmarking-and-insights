import {
  ExpenditureHistoryRow,
  ExpenditureHistoryRows,
  SchoolExpenditure,
  SchoolHistoryComparison,
  TrustExpenditure,
} from "src/services/types";
import { v4 as uuidv4 } from "uuid";

export class ExpenditureApi {
  static async history(
    type: string,
    id: string,
    dimension: string,
    signals?: AbortSignal[]
  ): Promise<ExpenditureHistoryRows> {
    const params = new URLSearchParams({
      type: type,
      id: id,
      dimension: dimension,
    });

    const response = await fetch("/api/expenditure/history?" + params, {
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

  static async historyComparison(
    type: string,
    id: string,
    dimension: string,
    overallPhase?: string,
    financeType?: string,
    signals?: AbortSignal[]
  ): Promise<SchoolHistoryComparison<ExpenditureHistoryRow>> {
    const params = new URLSearchParams({
      type: type,
      id: id,
      dimension: dimension,
      phase: overallPhase || "",
      financeType: financeType || "",
    });

    const response = await fetch(
      "/api/expenditure/history/comparison?" + params,
      {
        redirect: "manual",
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          "X-Correlation-ID": uuidv4(),
        },
        signal: signals?.length ? AbortSignal.any(signals) : undefined,
      }
    );

    const json = await response.json();
    if (json.error) {
      throw json.error;
    }

    return json;
  }

  static async query<T extends SchoolExpenditure>(
    type: string,
    id: string,
    dimension: string,
    category: string,
    phase?: string,
    customDataId?: string,
    signals?: AbortSignal[]
  ): Promise<T[]> {
    const params = new URLSearchParams({
      type: type,
      id: id,
      dimension: dimension,
      category: category,
    });

    if (phase) {
      params.append("phase", phase);
    }

    if (customDataId) {
      params.append("customDataId", customDataId);
    }

    return fetch("/api/expenditure?" + params, {
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

  static async trust<T extends TrustExpenditure>(
    id: string,
    dimension: string,
    category: string,
    excludeCentralServices?: boolean,
    signals?: AbortSignal[]
  ): Promise<T[]> {
    const params = new URLSearchParams({
      type: "trust",
      id: id,
      dimension: dimension,
      category: category,
    });

    if (excludeCentralServices !== undefined) {
      params.append(
        "excludeCentralServices",
        excludeCentralServices ? "true" : "false"
      );
    }

    return fetch("/api/expenditure/user-defined?" + params, {
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
