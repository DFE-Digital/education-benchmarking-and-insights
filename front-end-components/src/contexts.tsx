import {createContext} from "react";
import {ChartMode} from "./chart-mode";

export const ChartModeContext = createContext(ChartMode.CHART);
export const ChartDimensionContext = createContext("");