import { PropsWithChildren, useState } from "react";
import { ChartModeContext, IncludeBreakdownContext } from "./contexts";

type ChartModeProviderProps = PropsWithChildren<{
  initialValue: string;
}>;

export const ChartModeProvider = ({
  children,
  initialValue,
}: ChartModeProviderProps) => {
  const [chartMode, setChartMode] = useState<string>(initialValue);
  return (
    <ChartModeContext.Provider value={{ chartMode, setChartMode }}>
      {children}
    </ChartModeContext.Provider>
  );
};

type BreakdownProviderProps = PropsWithChildren<{
  initialValue: string;
}>;

export const BreakdownProvider = ({
  children,
  initialValue,
}: BreakdownProviderProps) => {
  const [breakdown, setBreakdown] = useState<string>(initialValue);
  return (
    <IncludeBreakdownContext.Provider value={{ breakdown, setBreakdown }}>
      {children}
    </IncludeBreakdownContext.Provider>
  );
};
