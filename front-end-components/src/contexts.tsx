import {createContext} from "react";
import {ChartMode} from "./chart-mode";
import {SelectedSchool} from "./types";

export const ChartModeContext = createContext(ChartMode.CHART);
export const ChartDimensionContext = createContext("");
export const SelectedSchoolContext = createContext<SelectedSchool>({ urn: "", name: "" });