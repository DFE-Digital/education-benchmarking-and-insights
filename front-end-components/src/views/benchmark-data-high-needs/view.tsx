import { BenchmarkDataHighNeedsViewProps } from "src/views/benchmark-data-high-needs/types";
import { useGovUk } from "src/hooks/useGovUk";
import { ChartModeChart } from "src/components";
import { ChartModeProvider, SelectedEstablishmentContext } from "src/contexts";
import { BenchmarkHighNeedsAccordion } from "./partials/benchmark-high-needs-accordion";

export const BenchmarkDataHighNeeds: React.FC<
  BenchmarkDataHighNeedsViewProps
> = ({ code, fetchTimeout }) => {
  useGovUk();

  return (
    <SelectedEstablishmentContext.Provider value={code}>
      <ChartModeProvider initialValue={ChartModeChart}>
        <BenchmarkHighNeedsAccordion fetchTimeout={fetchTimeout} />
      </ChartModeProvider>
    </SelectedEstablishmentContext.Provider>
  );
};
