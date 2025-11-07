import { SchoolExpenditureCommon } from "../types";

export type TotalExpenditureProps = {
  schools: TotalExpenditureData[];
};

export type TotalExpenditureData = SchoolExpenditureCommon & {
  urn: string;
  schoolType: string;
  totalExpenditure: number;
};
