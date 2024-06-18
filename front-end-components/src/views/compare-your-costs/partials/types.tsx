export type TotalExpenditureProps = {
  schools: TotalExpenditureData[];
};

export type TotalExpenditureData = {
  urn: string;
  schoolType: string;
  totalExpenditure: number;
  totalPupils: bigint;
  schoolName: string;
  laName: string;
};
