import React from "react";
import { useGovUk } from "src/hooks/useGovUk";
import { SelectedEstablishmentContext } from "src/contexts";
import { YearEnd } from "./partials/year-end";
import { BudgetForecastReturnsProps } from "./types";
import "./styles.scss";
import { BudgetForecastReturnsElementId } from "src/constants";

export const BudgetForecastReturns: React.FC<BudgetForecastReturnsProps> = ({
  id,
}) => {
  useGovUk(document.querySelector(`#${BudgetForecastReturnsElementId}`));

  return (
    <SelectedEstablishmentContext.Provider value={id}>
      <YearEnd id={id} />
    </SelectedEstablishmentContext.Provider>
  );
};
