import {
  LocalAuthoritySend2Benchmark,
  LocalAuthoritySend2History,
} from "src/services/types";
import { v4 as uuidv4 } from "uuid";

export class EducationHealthCarePlanApi {
  static async comparison(
    code: string,
    signals?: AbortSignal[]
  ): Promise<LocalAuthoritySend2Benchmark[]> {
    const params = new URLSearchParams({
      code,
    });

    const response = await fetch(
      "/api/local-authorities/education-health-care-plans/comparison?" + params,
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
  ): Promise<LocalAuthoritySend2History[]> {
    const params = new URLSearchParams({
      code,
    });

    const response = await fetch(
      "/api/local-authorities/education-health-care-plans/history?" + params,
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
