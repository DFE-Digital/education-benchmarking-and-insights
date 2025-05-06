import { IncomeHistoryRows } from "src/services/types";
import { v4 as uuidv4 } from "uuid";

export class IncomeApi {
  static async history(
    type: string,
    id: string,
    dimension: string,
    signals?: AbortSignal[]
  ): Promise<IncomeHistoryRows> {
    const params = new URLSearchParams({
      type: type,
      id: id,
      dimension: dimension,
    });

    const response = await fetch("/api/income/history?" + params, {
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
}
