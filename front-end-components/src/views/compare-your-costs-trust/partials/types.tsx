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
