import React from "react";
import { LaNationalRankViewProps } from "src/views";
import { ChartModeChart } from "src/components";
import { SelectedEstablishmentContext, ChartModeProvider } from "src/contexts";
import { FundingSection, PlannedExpenditureSection } from "./partials";
import { useGovUk } from "src/hooks/useGovUk";

export const LaNationalRankView: React.FC<LaNationalRankViewProps> = ({
  code,
  title,
}) => {
  useGovUk();

  return (
    <>
      <h2 className="govuk-heading-m">{title}</h2>
      <div className="govuk-tabs" data-module="govuk-tabs">
        <ul className="govuk-tabs__list">
          <li className="govuk-tabs__list-item govuk-tabs__list-item--selected">
            <a className="govuk-tabs__tab" href="#funding">
              Funding
            </a>
          </li>
          <li className="govuk-tabs__list-item">
            <a className="govuk-tabs__tab" href="#planned-expenditure">
              Planned expenditure
            </a>
          </li>
        </ul>
        <SelectedEstablishmentContext.Provider value={code}>
          <ChartModeProvider initialValue={ChartModeChart}>
            <div className="govuk-tabs__panel" id="funding">
              <FundingSection />
            </div>
            <div
              className="govuk-tabs__panel govuk-tabs__panel--hidden"
              id="planned-expenditure"
            >
              <PlannedExpenditureSection />
            </div>
          </ChartModeProvider>
        </SelectedEstablishmentContext.Provider>
      </div>
    </>
  );
};
