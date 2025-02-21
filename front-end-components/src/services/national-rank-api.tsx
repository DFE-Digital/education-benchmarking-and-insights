import { LocalAuthorityRanking } from "src/services/types";
import { v4 as uuidv4 } from "uuid";

export class NationalRankApi {
  static async get(sort?: "asc" | "desc"): Promise<LocalAuthorityRanking> {
    const response = await fetch(
      "/api/local-authorities/national-rank?" +
        new URLSearchParams({ sort: sort as string }),
      {
        redirect: "manual",
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          "X-Correlation-ID": uuidv4(),
        },
      }
    );

    const json = await response.json();
    if (json.error) {
      throw json.error;
    }

    return json;
  }
}
