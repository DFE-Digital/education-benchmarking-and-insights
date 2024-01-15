import {createContext} from "react";
import {ChartModes} from "src/components/chart-mode";
import {SelectedSchool} from "src/contexts/types";

export class School{
    static empty(): SelectedSchool {
        return {urn: "", name: ""}
    }
}

export const ChartModeContext = createContext(ChartModes.CHART);
export const ChartDimensionContext = createContext("");
export const SelectedSchoolContext = createContext(School.empty());
