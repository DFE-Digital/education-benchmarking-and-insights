import { createContext } from "react";
import { Dimension } from "src/components";

export interface ChartModeContextValue {
  chartMode: string;
  setChartMode: React.Dispatch<React.SetStateAction<string>>;
}

export const ChartModeContext = createContext<
  ChartModeContextValue | undefined
>(undefined);

export const ChartDimensionContext = createContext<Dimension>({
  label: "",
  value: "",
  heading: "",
});
export const SelectedEstablishmentContext = createContext("");

export const PhaseContext = createContext<string | undefined>(undefined);

export interface CentralServicesBreakdownContextValue {
  breakdown: string;
  setBreakdown: React.Dispatch<React.SetStateAction<string>>;
}

export const CentralServicesBreakdownContext = createContext<
  CentralServicesBreakdownContextValue | undefined
>(undefined);

export const CustomDataContext = createContext<string | undefined>(undefined);

type SuppressNegativeOrZero = {
  suppressNegativeOrZero: boolean;
  message: string;
};

export const SuppressNegativeOrZeroContext =
  createContext<SuppressNegativeOrZero>({
    suppressNegativeOrZero: false,
    message: "",
  });

export const CostCodeMapContext = createContext<Record<string, string> | null>(
  null
);

export interface CostCodeMapContextValues {
  costCodeMap: Record<string, string> | null;
  getCostCodes: (category: string) => string[];
  categoryCostCodes: string[];
}
