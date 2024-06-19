import {
  BalanceSection,
  IncomeSection,
  SpendingSection,
  CensusSection,
} from "src/views/historic-data/partials";
import { HistoricDataViewProps } from "src/views/historic-data/types";
import { SchoolEstablishment } from "src/constants.tsx";
import { useGovUk } from "src/hooks/useGovUk";
import { ChartModeChart } from "src/components";
import { ChartModeProvider } from "src/contexts";

export const HistoricData: React.FC<HistoricDataViewProps> = (props) => {
  const { type, id } = props;
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
        {type === SchoolEstablishment && (
          <li className="govuk-tabs__list-item">
            <a className="govuk-tabs__tab" href="#census">
              Pupil and workforce
            </a>
          </li>
        )}
      </ul>
      <ChartModeProvider initialValue={ChartModeChart}>
        <div className="govuk-tabs__panel" id="spending">
          <SpendingSection type={type} id={id} />
        </div>
        <div
          className="govuk-tabs__panel govuk-tabs__panel--hidden"
          id="income"
        >
          <IncomeSection type={type} id={id} />
        </div>
        <div
          className="govuk-tabs__panel govuk-tabs__panel--hidden"
          id="balance"
        >
          <BalanceSection type={type} id={id} />
        </div>
        {type === SchoolEstablishment && (
          <div
            className="govuk-tabs__panel govuk-tabs__panel--hidden"
            id="census"
          >
            <CensusSection id={id} />
          </div>
        )}
      </ChartModeProvider>
    </div>
  );
};
