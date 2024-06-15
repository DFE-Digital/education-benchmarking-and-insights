export type TotalExpenditureProps = {
  schools: TotalExpenditureData[];
};

export type TotalExpenditureData = {
  urn: string;
  schoolType: string;
  totalIncome: number;
  totalExpenditure: number;
  totalPupils: bigint;
  schoolName: string;
  laName: string;
};
