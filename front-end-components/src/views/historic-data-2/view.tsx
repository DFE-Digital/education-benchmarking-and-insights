import { BalanceSection, IncomeSection } from "../historic-data/partials";
import {
  CensusSection,
  SpendingSection,
} from "src/views/historic-data-2/partials";
import { HistoricData2Props } from "src/views/historic-data-2/types";
import { SchoolEstablishment } from "src/constants";
import { useGovUk } from "src/hooks/useGovUk";
import { ChartModeChart } from "src/components";
import { ChartModeProvider } from "src/contexts";

export const HistoricData2: React.FC<HistoricData2Props> = (props) => {
  useGovUk();

  return (
    <div className="govuk-tabs" data-module="govuk-tabs">
      <ul className="govuk-tabs__list">
        <li className="govuk-tabs__list-item govuk-tabs__list-item--selected">
          <a className="govuk-tabs__tab" href="#spending">
            Spending
          </a>
        </li>
        <li className="govuk-tabs__list-item">
          <a className="govuk-tabs__tab" href="#income">
            Income
          </a>
        </li>
        <li className="govuk-tabs__list-item">
          <a className="govuk-tabs__tab" href="#balance">
            Balance
          </a>
        </li>
        {props.type === SchoolEstablishment && (
          <li className="govuk-tabs__list-item">
            <a className="govuk-tabs__tab" href="#census">
              Pupil and workforce
            </a>
          </li>
        )}
      </ul>
      <ChartModeProvider initialValue={ChartModeChart}>
        <div className="govuk-tabs__panel" id="spending">
          <SpendingSection {...props} />
        </div>
        <div
          className="govuk-tabs__panel govuk-tabs__panel--hidden"
          id="income"
        >
          <IncomeSection {...props} />
        </div>
        <div
          className="govuk-tabs__panel govuk-tabs__panel--hidden"
          id="balance"
        >
          <BalanceSection {...props} />
        </div>
        {props.type === SchoolEstablishment && (
          <div
            className="govuk-tabs__panel govuk-tabs__panel--hidden"
            id="census"
          >
            <CensusSection {...props} />
          </div>
        )}
      </ChartModeProvider>
    </div>
  );
};
