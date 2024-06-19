import { createContext } from "react";
import { Dimension } from "src/components";

interface ChartModeContextValue {
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

interface HasIncompleteData {
  hasIncompleteData?: boolean;
  hasNoData?: boolean;
}
export const HasIncompleteDataContext = createContext<HasIncompleteData>({});

export const PhaseContext = createContext<string | undefined>(undefined);

interface IncludeBreakdownContextValue {
  breakdown: string;
  setBreakdown: React.Dispatch<React.SetStateAction<string>>;
}

export const IncludeBreakdownContext = createContext<
  IncludeBreakdownContextValue | undefined
>(undefined);
