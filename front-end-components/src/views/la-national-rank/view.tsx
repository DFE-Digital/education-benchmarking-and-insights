import React from "react";
import { LaNationalRankViewProps } from "src/views";
import { ChartModeChart } from "src/components";
import { SelectedEstablishmentContext, ChartModeProvider } from "src/contexts";
import { LaNationalRankChart } from "./partials/chart";

export const LaNationalRankView: React.FC<LaNationalRankViewProps> = ({
  code,
  title,
}) => {
  return (
    <SelectedEstablishmentContext.Provider value={code}>
      <ChartModeProvider initialValue={ChartModeChart}>
        <LaNationalRankChart title={title} />
      </ChartModeProvider>
    </SelectedEstablishmentContext.Provider>
  );
};
