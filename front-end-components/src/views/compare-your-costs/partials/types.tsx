import { ExpenditureData } from "src/services";

export type TotalExpenditureProps = {
  schools: TotalExpenditureData[];
};

export type TotalExpenditureData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  totalIncome: number;
  totalExpenditure: number;
  numberOfPupils: bigint;
  totalPupils: bigint;
  schoolName: string;
  laName: string;
};

export type ExpenditureAccordionProps = {
  schools: ExpenditureData[];
};
