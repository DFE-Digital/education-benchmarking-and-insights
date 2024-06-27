import React from "react";
import { useGovUk } from "src/hooks/useGovUk";
import { SelectedEstablishmentContext } from "src/contexts";
import { YearEnd } from "./partials/year-end";
import { BudgetForecastReturnsProps } from "./types";

export const BudgetForecastReturns: React.FC<BudgetForecastReturnsProps> = ({
  id,
}) => {
  useGovUk();

  return (
    <SelectedEstablishmentContext.Provider value={id}>
      <YearEnd id={id} />
    </SelectedEstablishmentContext.Provider>
  );
};
