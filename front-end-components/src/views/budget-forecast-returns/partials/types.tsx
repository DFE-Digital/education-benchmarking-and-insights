import { BudgetForecastReturn } from "src/services";

export type BudgetForecastData = {
  periodEndDate: string;
  forecast?: number;
  actual?: number;
  variance?: number;
};

export type BfrChartProps = {
  data: BudgetForecastData[];
  onImageCopied: React.Dispatch<React.SetStateAction<string | undefined>>;
  onImageLoading: React.Dispatch<React.SetStateAction<boolean | undefined>>;
};

export type BfrTableProps = {
  data?: BudgetForecastReturn[] | null;
};
