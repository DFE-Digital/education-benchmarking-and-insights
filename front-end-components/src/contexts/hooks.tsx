import { useContext } from "react";
import { ChartModeContext, IncludeBreakdownContext } from "./contexts";

export const useChartModeContext = () => {
  const chartModeContext = useContext(ChartModeContext);
  if (chartModeContext === undefined) {
    throw new Error("chartModeContext must be inside a <ChartModeProvider>");
  }

  return chartModeContext;
};

export const useIncludeBreakdownContext = () => {
  const includeBreakdownContext = useContext(IncludeBreakdownContext);
  if (includeBreakdownContext === undefined) {
    throw new Error(
      "includeBreakdownContext must be inside an <IncludeBreakdownProvider>"
    );
  }

  return includeBreakdownContext;
};
