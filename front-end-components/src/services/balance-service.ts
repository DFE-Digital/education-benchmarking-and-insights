import { BalanceApi } from "./balance-api";
import { SchoolBalanceHistory } from "./types";

export class BalanceService {
  static async getHistory(
    type: string,
    id: string,
    dimension: string
  ): Promise<SchoolBalanceHistory[]> {
    const historyRows = await BalanceApi.history(type, id, dimension);
    const history = new Array<SchoolBalanceHistory>();

    let year = historyRows.startYear + 1;
    while (year <= historyRows.endYear) {
      const rows = historyRows.rows.filter((r) => r.year === year);
      if (rows.length === 0) {
        rows.push({
          inYearBalance: undefined,
          revenueReserve: undefined,
          year,
        });
      }

      history.push(
        ...rows.map((r) => {
          return { ...r, term: `${year - 1} to ${year}` };
        })
      );

      year++;
    }

    return history;
  }
}
