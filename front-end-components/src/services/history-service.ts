import { BalanceApi } from "./balance-api";
import { CensusApi } from "./census-api";
import { ExpenditureApi } from "./expenditure-api";
import { IncomeApi } from "./income-api";
import {
  BalanceHistoryItem,
  CensusHistoryItem,
  ExpenditureHistoryItem,
  HistoryBase,
  HistoryRow,
  HistoryRows,
  IncomeHistoryItem,
  SchoolHistoryComparison,
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
  ): Promise<IncomeHistoryItem[]> {
    const historyRows = await IncomeApi.history(type, id, dimension);
    return HistoryService.populateHistoricYears(historyRows);
  }

  static async getCensusHistory(
    id: string,
    dimension: string
  ): Promise<CensusHistoryItem[]> {
    const historyRows = await CensusApi.history(id, dimension);
    return HistoryService.populateHistoricYears(historyRows);
  }

  static async getCensusHistoryComparison(
    id: string,
    dimension: string,
    overallPhase?: string,
    financeType?: string,
    signals?: AbortSignal[]
  ): Promise<SchoolHistoryComparison<CensusHistoryItem>> {
    const {
      startYear,
      endYear,
      school,
      comparatorSetAverage,
      nationalAverage,
    } = await CensusApi.historyComparison(
      id,
      dimension,
      overallPhase,
      financeType,
      signals
    );

    if (!startYear || !endYear) {
      return {};
    }

    return {
      school: HistoryService.populateHistoricYearsRows(
        school || [],
        startYear,
        endYear
      ),
      comparatorSetAverage: HistoryService.populateHistoricYearsRows(
        comparatorSetAverage || [],
        startYear,
        endYear
      ),
      nationalAverage: HistoryService.populateHistoricYearsRows(
        nationalAverage || [],
        startYear,
        endYear
      ),
    };
  }

  static async getExpenditureHistory(
    type: string,
    id: string,
    dimension: string
  ): Promise<ExpenditureHistoryItem[]> {
    const historyRows = await ExpenditureApi.history(type, id, dimension);
    return HistoryService.populateHistoricYears(historyRows);
  }

  static async getExpenditureHistoryComparison(
    type: string,
    id: string,
    dimension: string,
    overallPhase?: string,
    financeType?: string,
    signals?: AbortSignal[]
  ): Promise<SchoolHistoryComparison<ExpenditureHistoryItem>> {
    const {
      startYear,
      endYear,
      school,
      comparatorSetAverage,
      nationalAverage,
    } = await ExpenditureApi.historyComparison(
      type,
      id,
      dimension,
      overallPhase,
      financeType,
      signals
    );

    if (!startYear || !endYear) {
      return {};
    }

    return {
      school: HistoryService.populateHistoricYearsRows(
        school || [],
        startYear,
        endYear
      ),
      comparatorSetAverage: HistoryService.populateHistoricYearsRows(
        comparatorSetAverage || [],
        startYear,
        endYear
      ),
      nationalAverage: HistoryService.populateHistoricYearsRows(
        nationalAverage || [],
        startYear,
        endYear
      ),
    };
  }

  private static populateHistoricYears<
    TRow extends HistoryRow,
    TRows extends HistoryRows<TRow>,
    TData extends HistoryBase,
  >({ rows, startYear, endYear }: TRows): TData[] {
    return this.populateHistoricYearsRows(rows, startYear, endYear);
  }

  private static populateHistoricYearsRows<
    TRow extends HistoryRow,
    TRows extends Array<TRow>,
    TData extends HistoryBase,
  >(rows: TRows, startYear: number, endYear: number): TData[] {
    const history = new Array<TData>();

    let year = startYear;
    while (year <= endYear) {
      const yearRows = rows.filter((r) => r.year === year);
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
