import React from "react";
import { LaNationalRankChart } from "./partials";
import {
  LaNationalRankViewProps,
  plannedExpenditureTitle,
  plannedExpenditureSummary,
  plannedExpenditureValueLabel,
  fundingTitle,
  fundingSummary,
  fundingValueLabel,
} from "src/views";
import { ChartModeChart } from "src/components";
import { SelectedEstablishmentContext, ChartModeProvider } from "src/contexts";
import { useGovUk } from "src/hooks/useGovUk";

export const LaNationalRankView: React.FC<LaNationalRankViewProps> = ({
  code,
  title,
  year,
}) => {
  useGovUk();

  return (
    <>
      <h2 className="govuk-heading-m govuk-!-margin-top-4 govuk-!-margin-bottom-5">
        {title}
      </h2>
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
              <LaNationalRankChart
                title={fundingTitle}
                summary={fundingSummary(year)}
                prefix={fundingTitle}
                valueLabel={fundingValueLabel}
                rankingApiParam="SpendAsPercentageOfFunding"
              />
            </div>
            <div
              className="govuk-tabs__panel govuk-tabs__panel--hidden"
              id="planned-expenditure"
            >
              <LaNationalRankChart
                title={plannedExpenditureTitle}
                summary={plannedExpenditureSummary(year)}
                prefix={plannedExpenditureTitle}
                valueLabel={plannedExpenditureValueLabel}
                rankingApiParam="SpendAsPercentageOfBudget"
              />
            </div>
          </ChartModeProvider>
        </SelectedEstablishmentContext.Provider>
      </div>
    </>
  );
};
