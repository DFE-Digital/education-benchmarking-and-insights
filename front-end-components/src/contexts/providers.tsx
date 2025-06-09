import { PropsWithChildren, useState } from "react";
import {
  ChartModeContext,
  CentralServicesBreakdownContext,
  CostCodeMapContext,
} from "./contexts";

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
    <CentralServicesBreakdownContext.Provider
      value={{ breakdown, setBreakdown }}
    >
      {children}
    </CentralServicesBreakdownContext.Provider>
  );
};

type CostCodeMapProviderProps = PropsWithChildren<{
  costCodeMap?: Record<string, string>;
  tags?: string[];
}>;

export const CostCodeMapProvider = ({
  children,
  costCodeMap,
  tags,
}: CostCodeMapProviderProps) => {
  /**
   * This function duplicates the mapping logic found in `CostCodes.cs` within Web.App.
   * The duplication exists due to inconsistencies in chart-to-subcategory naming,
   * requiring this mapping to be redefined here.
   */
  const getCostCodes = (category: string): string[] => {
    if (!costCodeMap) {
      return [];
    }
    switch (category) {
      case "Total teaching and teaching support staff costs": {
        return [
          costCodeMap["teaching staff"],
          costCodeMap["supply teaching staff"],
          costCodeMap["agency supply teaching staff"],
          costCodeMap["education support staff"],
          costCodeMap["educational consultancy"],
        ];
      }
      case "Teaching staff costs": {
        return [costCodeMap["teaching staff"]];
      }
      case "Supply teaching staff costs": {
        return [costCodeMap["supply teaching staff"]];
      }
      case "Agency supply teaching staff costs": {
        return [costCodeMap["agency supply teaching staff"]];
      }
      case "Educational support staff costs": {
        return [costCodeMap["education support staff"]];
      }
      case "Educational consultancy costs": {
        return [costCodeMap["educational consultancy"]];
      }

      case "Total non-educational support staff costs": {
        return [
          costCodeMap["administrative and clerical staff"],
          costCodeMap["auditor costs"],
          costCodeMap["other staff"],
          costCodeMap["professional services (non-curriculum)"],
        ];
      }
      case "Administrative and clerical staff costs": {
        return [costCodeMap["administrative and clerical staff"]];
      }
      case "Auditors costs": {
        return [costCodeMap["auditor costs"]];
      }
      case "Other staff costs": {
        return [costCodeMap["other staff"]];
      }
      case "Professional services (non-curriculum) costs": {
        return [costCodeMap["professional services (non-curriculum)"]];
      }

      case "Total educational supplies costs": {
        return [
          costCodeMap["examination fees"],
          costCodeMap["learning resources (not ICT equipment)"],
        ];
      }
      case "Examination fees costs": {
        return [costCodeMap["examination fees"]];
      }
      case "Learning resources (not ICT equipment) costs": {
        return [costCodeMap["learning resources (not ICT equipment)"]];
      }

      case "Educational learning resources costs": {
        return [costCodeMap["ict learning resources"]];
      }

      case "Total premises staff and service costs": {
        return [
          costCodeMap["cleaning and caretaking"],
          costCodeMap["maintenance of premises"],
          costCodeMap["other occupation costs"],
          costCodeMap["premises staff"],
        ];
      }
      case "Cleaning and caretaking costs": {
        return [costCodeMap["cleaning and caretaking"]];
      }
      case "Maintenance of premises costs": {
        return [costCodeMap["maintenance of premises"]];
      }
      case "Other occupation costs": {
        return [costCodeMap["other occupation costs"]];
      }
      case "Premises staff costs": {
        return [costCodeMap["premises staff"]];
      }

      case "Total utilities costs": {
        return [costCodeMap["energy"], costCodeMap["water and sewerage"]];
      }
      case "Energy costs": {
        return [costCodeMap["energy"]];
      }
      case "Water and sewerage costs": {
        return [costCodeMap["water and sewerage"]];
      }

      case "Administrative supplies (Non-educational)": {
        return [costCodeMap["administrative supplies (non-educational)"]];
      }

      case "Total catering costs (gross)": {
        return [
          costCodeMap["catering staff"],
          costCodeMap["catering supplies"],
        ];
      }
      case "Total catering costs (net)": {
        return [
          costCodeMap["catering staff"],
          costCodeMap["catering supplies"],
        ];
      }
      case "Catering staff costs": {
        return [costCodeMap["catering staff"]];
      }
      case "Catering supplies costs": {
        return [costCodeMap["catering supplies"]];
      }

      case "Total other costs": {
        return [
          costCodeMap["direct revenue financing"],
          costCodeMap["grounds maintenance"],
          costCodeMap["indirect employee expenses"],
          costCodeMap["interest charges for loan and bank"],
          costCodeMap["other insurance premiums"],
          costCodeMap["private Finance Initiative (PFI) charges"],
          costCodeMap["rent and rates"],
          costCodeMap["special facilities"],
          costCodeMap["staff development and training"],
          costCodeMap["staff-related insurance"],
          costCodeMap["supply teacher insurance"],
          costCodeMap[
            "community focused school staff (maintained schools only)"
          ],
          costCodeMap[
            "community focused school costs (maintained schools only)"
          ],
        ];
      }
      case "Direct revenue financing costs": {
        return [costCodeMap["direct revenue financing"]];
      }
      case "Ground maintenance costs": {
        return [costCodeMap["grounds maintenance"]];
      }
      case "Indirect employee expenses": {
        return [costCodeMap["indirect employee expenses"]];
      }
      case "Interest charges for loan and bank": {
        return [costCodeMap["interest charges for loan and bank"]];
      }
      case "Other insurance premiums costs": {
        return [costCodeMap["other insurance premiums"]];
      }
      case "PFI charges": {
        return [costCodeMap["private Finance Initiative (PFI) charges"]];
      }
      case "Rent and rates costs": {
        return [costCodeMap["rent and rates"]];
      }
      case "Special facilities costs": {
        return [costCodeMap["special facilities"]];
      }
      case "Staff development and training costs": {
        return [costCodeMap["staff development and training"]];
      }
      case "Staff-related insurance costs": {
        return [costCodeMap["staff-related insurance"]];
      }
      case "Supply teacher insurance costs": {
        return [costCodeMap["supply teacher insurance"]];
      }
      case "Community focused school staff (maintained schools only)": {
        return [
          costCodeMap[
            "community focused school staff (maintained schools only)"
          ],
        ];
      }
      case "Community focused school costs (maintained schools only)": {
        return [
          costCodeMap[
            "community focused school costs (maintained schools only)"
          ],
        ];
      }
      default: {
        return [];
      }
    }
  };

  return (
    <CostCodeMapContext.Provider value={{ costCodeMap, getCostCodes, tags }}>
      {children}
    </CostCodeMapContext.Provider>
  );
};
