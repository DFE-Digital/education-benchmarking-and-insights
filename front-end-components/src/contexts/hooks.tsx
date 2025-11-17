import { useContext } from "react";
import {
  ChartModeContext,
  ChartModeContextValue,
  CentralServicesBreakdownContext,
  CentralServicesBreakdownContextValue,
  CostCodesContext,
  CostCodesContextValues,
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

export const useCostCodesContext = (
  category: string,
  throwIfUndefined?: boolean
): CostCodesContextValues => {
  const costCodesContext = useContext(CostCodesContext);
  if (costCodesContext === undefined) {
    if (throwIfUndefined) {
      throw new Error("costCodesContext must be inside a <CostCodeProvider>");
    }

    return {
      categoryCostCodes: [],
    };
  }

  const { getCostCodes, itemClassName, label, tags } = costCodesContext;
  const categoryCostCodes = getCostCodes(category).filter((code) => !!code);
  return { categoryCostCodes, itemClassName, label, tags };
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

    return {
      available: [],
      progressIndicators: {},
      selected: [],
      setSelected: () => {},
    };
  }

  return progressIndicatorsContext;
};
