import { BudgetForecastReturn } from "src/services";

export type BudgetForecastData = {
  periodEndDate: string;
  forecast?: number;
  actual?: number;
  variance?: number;
};

export type BfrChartProps = {
  data: BudgetForecastData[];
  onImageLoading: React.Dispatch<React.SetStateAction<boolean | undefined>>;
};

export type BfrTableProps = {
  data?: BudgetForecastReturn[] | null;
};
