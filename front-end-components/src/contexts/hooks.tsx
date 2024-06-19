import { useContext } from "react";
import {
  ChartModeContext,
  ChartModeContextValue,
  IncludeBreakdownContext,
  BreakdownContextValue,
} from "./contexts";

export const useChartModeContext = (
  throwIfUndefined?: boolean
): ChartModeContextValue => {
  const chartModeContext = useContext(ChartModeContext);
  if (chartModeContext === undefined) {
    if (throwIfUndefined) {
      throw new Error("chartModeContext must be inside a <ChartModeProvider>");
    }

    return { chartMode: "", setChartMode: () => {} };
  }

  return chartModeContext;
};

export const useBreakdownContext = (
  throwIfUndefined?: boolean
): BreakdownContextValue => {
  const includeBreakdownContext = useContext(IncludeBreakdownContext);
  if (includeBreakdownContext === undefined) {
    if (throwIfUndefined) {
      throw new Error(
        "includeBreakdownContext must be inside an <IncludeBreakdownProvider>"
      );
    }

    return { breakdown: "", setBreakdown: () => {} };
  }

  return includeBreakdownContext;
};
