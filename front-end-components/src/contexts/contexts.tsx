import { createContext } from "react";
import { ChartModeChart } from "src/components/chart-mode";
import { SelectedSchool } from "src/contexts/types";
import { Dimension } from "src/components";

export class School {
  static empty(): SelectedSchool {
    return { urn: "", name: "" };
  }
}

export const ChartModeContext = createContext(ChartModeChart);
export const ChartDimensionContext = createContext<Dimension>({
  label: "",
  value: "",
  heading: "",
});
export const SelectedSchoolContext = createContext(School.empty());

interface HasIncompleteData {
  hasIncompleteData?: boolean;
  hasNoData?: boolean;
}
export const HasIncompleteDataContext = createContext<HasIncompleteData>({});

export const PhaseContext = createContext<string | undefined>(undefined);
