import React from "react";
import { CompareYourTrustViewProps } from "src/views/compare-your-trust";
import { useGovUk } from "src/hooks/useGovUk";
import { SpendingSection } from "./partials/spending-section";
import { BalanceSection } from "./partials";
import { ChartModeChart } from "src/components";
import { BreakdownInclude } from "src/components/central-services-breakdown";
import {
  ChartModeProvider,
  BreakdownProvider,
  SelectedEstablishmentContext,
} from "src/contexts";

export const CompareYourTrust: React.FC<CompareYourTrustViewProps> = ({
  id,
}) => {
  useGovUk();

  return (
    <div className="govuk-tabs" data-module="govuk-tabs">
      <ul className="govuk-tabs__list">
        <li className="govuk-tabs__list-item govuk-tabs__list-item--selected">
          <a className="govuk-tabs__tab" href="#spending">
            Spending
          </a>
        </li>
        <li className="govuk-tabs__list-item govuk-tabs__list-item--selected">
          <a className="govuk-tabs__tab" href="#balance">
            Balance
          </a>
        </li>
      </ul>
      <SelectedEstablishmentContext.Provider value={id}>
        <ChartModeProvider initialValue={ChartModeChart}>
          <BreakdownProvider initialValue={BreakdownInclude}>
            <div className="govuk-tabs__panel" id="spending">
              <SpendingSection id={id} />
            </div>
          </BreakdownProvider>
          <BreakdownProvider initialValue={BreakdownInclude}>
            <div className="govuk-tabs__panel" id="balance">
              <BalanceSection id={id} />
            </div>
          </BreakdownProvider>
        </ChartModeProvider>
      </SelectedEstablishmentContext.Provider>
    </div>
  );
};
