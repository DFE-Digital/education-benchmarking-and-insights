import {
  LocalAuthoritySection251,
  LocalAuthoritySection251Benchmark,
  LocalAuthoritySection251History,
} from "src/services/types";
import { v4 as uuidv4 } from "uuid";

export class HighNeedsApi {
  static async comparison(
    code: string,
    set?: string[],
    signals?: AbortSignal[]
  ): Promise<LocalAuthoritySection251Benchmark<LocalAuthoritySection251>[]> {
    const params = new URLSearchParams({
      code,
    });

    (set || []).forEach((s) => {
      params.append("set", s);
    });

    const response = await fetch(
      "/api/local-authorities/high-needs/comparison?" + params,
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

  static async history(
    code: string,
    signals?: AbortSignal[]
  ): Promise<LocalAuthoritySection251History<LocalAuthoritySection251>[]> {
    const params = new URLSearchParams({
      code,
    });

    const response = await fetch(
      "/api/local-authorities/high-needs/history?" + params,
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
}
