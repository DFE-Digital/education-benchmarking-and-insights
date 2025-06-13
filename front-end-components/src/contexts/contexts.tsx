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

export const CostCodeMapContext = createContext<
  CostCodeMapContextValue | undefined
>(undefined);

export interface CostCodeMapContextValue {
  costCodeMap?: Record<string, string>;
  getCostCodes: (category: string) => string[];
  tags?: string[];
}

export interface CostCodeMapContextValues {
  categoryCostCodes: string[];
  tags?: string[];
}

export const ShowHighExecutivePayContext = createContext<boolean | undefined>(
  undefined
);
