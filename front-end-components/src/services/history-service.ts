import { BalanceApi } from "./balance-api";
import { IncomeApi } from "./income-api";
import {
  BalanceHistoryItem,
  HistoryBase,
  HistoryRow,
  HistoryRows,
  IncomeHistoryRow,
} from "./types";

export class HistoryService {
  static async getBalanceHistory(
    type: string,
    id: string,
    dimension: string
  ): Promise<BalanceHistoryItem[]> {
    const historyRows = await BalanceApi.history(type, id, dimension);
    return HistoryService.populateHistoricYears(historyRows);
  }

  static async getIncomeHistory(
    type: string,
    id: string,
    dimension: string
  ): Promise<IncomeHistoryRow[]> {
    const historyRows = await IncomeApi.history(type, id, dimension);
    return HistoryService.populateHistoricYears(historyRows);
  }

  private static populateHistoricYears<
    TRow extends HistoryRow,
    TRows extends HistoryRows<TRow>,
    TData extends HistoryBase,
  >(rows: TRows): TData[] {
    const history = new Array<TData>();

    let year = rows.startYear + 1;
    while (year <= rows.endYear) {
      const yearRows = rows.rows.filter((r) => r.year === year);
      if (yearRows.length === 0) {
        yearRows.push({
          year,
        } as TRow);
      }

      history.push(
        ...yearRows.map((r) => {
          return { ...r, term: `${year - 1} to ${year}` } as unknown as TData;
        })
      );

      year++;
    }

    return history;
  }
}
