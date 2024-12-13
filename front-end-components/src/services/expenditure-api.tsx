import {
  SchoolExpenditure,
  SchoolExpenditureHistory,
  SchoolHistoryComparison,
  TrustExpenditure,
} from "src/services/types";
import { v4 as uuidv4 } from "uuid";

export class ExpenditureApi {
  static async history(
    type: string,
    id: string,
    dimension: string,
    excludeCentralServices?: boolean
  ): Promise<SchoolExpenditureHistory[]> {
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

    return fetch("/api/expenditure/history?" + params, {
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

  static async historyComparison(
    type: string,
    id: string,
    dimension: string,
    overallPhase?: string,
    financeType?: string,
    excludeCentralServices?: boolean
  ): Promise<SchoolHistoryComparison<SchoolExpenditureHistory>> {
    const params = new URLSearchParams({
      type: type,
      id: id,
      dimension: dimension,
      phase: overallPhase || "",
      financeType: financeType || "",
    });
    if (excludeCentralServices !== undefined) {
      params.append(
        "excludeCentralServices",
        excludeCentralServices ? "true" : "false"
      );
    }

    const response = await fetch(
      "/api/expenditure/history/comparison?" + params,
      {
        redirect: "manual",
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          "X-Correlation-ID": uuidv4(),
        },
        signal: AbortSignal.timeout(30_000),
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
    customDataId?: string
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
    excludeCentralServices?: boolean
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
