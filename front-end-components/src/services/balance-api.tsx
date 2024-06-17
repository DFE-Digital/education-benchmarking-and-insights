import { BalanceHistory } from "src/services/types";
import { v4 as uuidv4 } from "uuid";

export class BalanceApi {
  static async history(
    type: string,
    id: string,
    dimension: string,
    includeBreakdown?: boolean
  ): Promise<BalanceHistory[]> {
    const params = new URLSearchParams({
      type: type,
      id: id,
      dimension: dimension,
    });
    if (includeBreakdown !== undefined) {
      params.append("includeBreakdown", includeBreakdown ? "true" : "false");
    }

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
}
