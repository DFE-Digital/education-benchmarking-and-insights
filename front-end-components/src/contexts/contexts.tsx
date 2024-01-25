import { createContext } from "react";
import { ChartModeChart } from "src/components/chart-mode";
import { SelectedSchool } from "src/contexts/types";

export class School {
  static empty(): SelectedSchool {
    return { urn: "", name: "" };
  }
}

export const ChartModeContext = createContext(ChartModeChart);
export const ChartDimensionContext = createContext("");
export const SelectedSchoolContext = createContext(School.empty());
