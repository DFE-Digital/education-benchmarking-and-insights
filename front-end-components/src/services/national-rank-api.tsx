import { LocalAuthorityRanking } from "src/services/types";
import { v4 as uuidv4 } from "uuid";

export class NationalRankApi {
  static async get(
    ranking: "SpendAsPercentageOfBudget",
    sort?: "asc" | "desc",
    signals?: AbortSignal[]
  ): Promise<LocalAuthorityRanking> {
    const params: { ranking: string; sort?: string } = { ranking };
    if (sort) {
      params.sort = sort;
    }
    const response = await fetch(
      "/api/local-authorities/national-rank?" + new URLSearchParams(params),
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
