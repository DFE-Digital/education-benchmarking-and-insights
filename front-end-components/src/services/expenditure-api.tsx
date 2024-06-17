import {
  SchoolExpenditure,
  SchoolExpenditureHistory,
  TrustExpenditure,
} from "src/services/types";
import { v4 as uuidv4 } from "uuid";

export class ExpenditureApi {
  static async history(
    type: string,
    id: string,
    dimension: string,
    includeBreakdown?: boolean
  ): Promise<SchoolExpenditureHistory[]> {
    const params = new URLSearchParams({
      type: type,
      id: id,
      dimension: dimension,
    });
    if (includeBreakdown !== undefined) {
      params.append("includeBreakdown", includeBreakdown ? "true" : "false");
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

  static async query(
    type: string,
    id: string,
    dimension: string,
    category: string,
    phase?: string
  ): Promise<SchoolExpenditure[]> {
    const params = new URLSearchParams({
      type: type,
      id: id,
      dimension: dimension,
      category: category,
    });

    if (phase) {
      params.append("phase", phase);
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

  static async trust(
    type: string,
    id: string,
    dimension: string,
    category: string,
    includeBreakdown?: boolean
  ): Promise<TrustExpenditure[]> {
    const params = new URLSearchParams({
      type: type,
      id: id,
      dimension: dimension,
      category: category,
    });

    if (includeBreakdown !== undefined) {
      params.append("includeBreakdown", includeBreakdown ? "true" : "false");
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
