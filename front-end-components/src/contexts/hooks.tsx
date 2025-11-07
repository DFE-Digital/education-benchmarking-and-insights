import { useContext } from "react";
import {
  ChartModeContext,
  ChartModeContextValue,
  CentralServicesBreakdownContext,
  CentralServicesBreakdownContextValue,
  CostCodeMapContext,
  CostCodeMapContextValues,
  ProgressIndicatorsContextValue,
  ProgressIndicatorsContext,
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

export const useCentralServicesBreakdownContext = (
  throwIfUndefined?: boolean
): CentralServicesBreakdownContextValue => {
  const centralServicesBreakdownContext = useContext(
    CentralServicesBreakdownContext
  );
  if (centralServicesBreakdownContext === undefined) {
    if (throwIfUndefined) {
      throw new Error(
        "centralServicesBreakdownContext must be inside an <CentralServicesBreakdownProvider>"
      );
    }

    return { breakdown: "", setBreakdown: () => {} };
  }

  return centralServicesBreakdownContext;
};

export const useCostCodeMapContext = (
  category: string,
  throwIfUndefined?: boolean
): CostCodeMapContextValues => {
  const costCodeMapContext = useContext(CostCodeMapContext);
  if (costCodeMapContext === undefined) {
    if (throwIfUndefined) {
      throw new Error(
        "costCodeMapContext must be inside a <CostCodeMapProvider>"
      );
    }

    return {
      categoryCostCodes: [],
    };
  }

  const { getCostCodes, tags } = costCodeMapContext;
  const categoryCostCodes = getCostCodes(category).filter((code) => !!code);
  return { categoryCostCodes, tags };
};

export const useProgressIndicatorsContext = (
  throwIfUndefined?: boolean
): ProgressIndicatorsContextValue => {
  const progressIndicatorsContext = useContext(ProgressIndicatorsContext);
  if (progressIndicatorsContext === undefined) {
    if (throwIfUndefined) {
      throw new Error(
        "progressIndicatorsContext must be inside a <ProgressIndicatorsProvider>"
      );
    }

    return { available: [], selected: [], setSelected: () => {} };
  }

  return progressIndicatorsContext;
};
