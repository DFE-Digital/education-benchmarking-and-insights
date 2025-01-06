import {
  Census,
  CensusHistory,
  SchoolHistoryComparison,
} from "src/services/types";
import { v4 as uuidv4 } from "uuid";

export class CensusApi {
  static async history(
    id: string,
    dimension: string
  ): Promise<CensusHistory[]> {
    return fetch(
      "/api/census/history?" +
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

  static async historyComparison(
    id: string,
    dimension: string,
    overallPhase?: string,
    financeType?: string,
    signals?: AbortSignal[]
  ): Promise<SchoolHistoryComparison<CensusHistory>> {
    const params = new URLSearchParams({
      id: id,
      dimension: dimension,
      phase: overallPhase || "",
      financeType: financeType || "",
    });

    const response = await fetch("/api/census/history/comparison?" + params, {
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

  static async query(
    type: string,
    id: string,
    dimension: string,
    category: string,
    phase?: string,
    customDataId?: string
  ): Promise<Census[]> {
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

    return fetch("/api/census?" + params, {
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
