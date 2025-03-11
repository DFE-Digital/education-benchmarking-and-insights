import { BenchmarkDataHighNeedsViewProps } from "src/views/benchmark-data-high-needs/types";
import { useGovUk } from "src/hooks/useGovUk";
import { ChartModeChart } from "src/components";
import { ChartModeProvider, SelectedEstablishmentContext } from "src/contexts";
import { BenchmarkHighNeeds } from "./partials/benchmark-high-needs";

export const BenchmarkDataHighNeeds: React.FC<
  BenchmarkDataHighNeedsViewProps
> = ({ code, fetchTimeout }) => {
  useGovUk();

  return (
    <SelectedEstablishmentContext.Provider value={code}>
      <ChartModeProvider initialValue={ChartModeChart}>
        <BenchmarkHighNeeds fetchTimeout={fetchTimeout} />
      </ChartModeProvider>
    </SelectedEstablishmentContext.Provider>
  );
};
