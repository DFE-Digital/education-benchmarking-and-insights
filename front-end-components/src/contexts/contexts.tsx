import { createContext } from "react";
import { ChartModeChart } from "src/components/chart-mode";
import { Dimension } from "src/components";

export const ChartModeContext = createContext(ChartModeChart);
export const ChartDimensionContext = createContext<Dimension>({
  label: "",
  value: "",
  heading: "",
});
export const SelectedSchoolContext = createContext("");

interface HasIncompleteData {
  hasIncompleteData?: boolean;
  hasNoData?: boolean;
}
export const HasIncompleteDataContext = createContext<HasIncompleteData>({});

export const PhaseContext = createContext<string | undefined>(undefined);
