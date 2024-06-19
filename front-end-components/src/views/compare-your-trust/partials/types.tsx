export type TotalExpenditureProps = {
  trusts: TotalExpenditureData[];
};

export type TotalExpenditureData = {
  companyNumber: string;
  trustName: string;
  schoolTotalExpenditure?: number;
  centralTotalExpenditure?: number;
  totalExpenditure?: number;
};

export type SpendingSectionProps = {
  id: string;
};

export type BalanceSectionProps = {
  id: string;
};

export type BalanceData = {
  companyNumber: string;
  trustName: string;
  schoolInYearBalance?: number;
  centralInYearBalance?: number;
  inYearBalance?: number;
};
