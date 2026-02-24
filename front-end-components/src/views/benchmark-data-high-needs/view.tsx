import { BenchmarkDataHighNeedsViewProps } from "src/views/benchmark-data-high-needs/types";
import { useGovUk } from "src/hooks/useGovUk";
import { ChartModeChart } from "src/components";
import {
  ChartModeProvider,
  SelectedEstablishmentContext,
  SourceInfoContext,
} from "src/contexts";
import { BenchmarkHighNeeds } from "./partials/benchmark-high-needs";

export const BenchmarkDataHighNeeds: React.FC<
  BenchmarkDataHighNeedsViewProps
> = ({ code, rootEl, yearsLabel, glossaryUrl, ...props }) => {
  useGovUk(rootEl);

  return (
    <SelectedEstablishmentContext.Provider value={code}>
      <ChartModeProvider initialValue={ChartModeChart}>
        <SourceInfoContext.Provider value={{ yearsLabel, glossaryUrl }}>
          <BenchmarkHighNeeds {...props} />
        </SourceInfoContext.Provider>
      </ChartModeProvider>
    </SelectedEstablishmentContext.Provider>
  );
};
