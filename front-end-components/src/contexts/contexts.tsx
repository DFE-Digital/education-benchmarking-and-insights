import { createContext, ReactNode } from "react";
import { Props as LegendProps } from "recharts/types/component/DefaultLegendContent";
import { Dimension, ProgressBanding, ProgressIndicators } from "src/components";
import { CostCodeMap } from "src/views";

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

export type SuppressNegativeOrZero = {
  suppressNegativeOrZero: boolean;
  message: string;
};

export const SuppressNegativeOrZeroContext =
  createContext<SuppressNegativeOrZero>({
    suppressNegativeOrZero: false,
    message: "",
  });

export const CostCodesContext = createContext<
  CostCodesContextValue | undefined
>(undefined);

export interface CostCodesContextValue
  extends Omit<CostCodesContextValues, "categoryCostCodes"> {
  costCodeMap?: CostCodeMap;
  getCostCodes: (category: string) => string[];
}

export interface CostCodesContextValues {
  categoryCostCodes: string[];
  itemClassName?: string;
  label?: string;
  tags?: string[];
}

export const ShowHighExecutivePayContext = createContext<boolean | undefined>(
  undefined
);

export interface ProgressIndicatorsContextValue {
  available: ProgressBanding[];
  data?: ProgressIndicators;
  progressIndicators: Record<string, ProgressBanding>;
  renderChartLegend?: (props: LegendProps, keys?: string[]) => ReactNode;
  selected: ProgressBanding[];
  setSelected: React.Dispatch<React.SetStateAction<ProgressBanding[]>>;
}

export const ProgressIndicatorsContext = createContext<
  ProgressIndicatorsContextValue | undefined
>(undefined);

export const ShareButtonsLayoutContext =
  createContext<ShareButtonsLayout>(null);

export type ShareButtonsLayout = "column" | null;
