import React, { useLayoutEffect } from "react";
// eslint-disable-next-line @typescript-eslint/ban-ts-comment
//@ts-expect-error
import { initAll } from "govuk-frontend";
import {
  BalanceSection,
  IncomeSection,
  Spending,
  WorkforceSection,
} from "src/views/historic-data/partials";
import { HistoricDataViewProps } from "src/views/historic-data/types";
import { SchoolEstablishment } from "src/constants.tsx";

export const HistoricData: React.FC<HistoricDataViewProps> = (props) => {
  const { type, id } = props;
  useLayoutEffect(() => {
    initAll();
  }, []);

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
            <a className="govuk-tabs__tab" href="#workforce">
              Workforce
            </a>
          </li>
        )}
      </ul>
      <div className="govuk-tabs__panel" id="spending">
        <Spending />
      </div>
      <div className="govuk-tabs__panel govuk-tabs__panel--hidden" id="income">
        <IncomeSection type={type} id={id} />
      </div>
      <div className="govuk-tabs__panel govuk-tabs__panel--hidden" id="balance">
        <BalanceSection type={type} id={id} />
      </div>
      {type === SchoolEstablishment && (
        <div
          className="govuk-tabs__panel govuk-tabs__panel--hidden"
          id="workforce"
        >
          <WorkforceSection type={type} id={id} />
        </div>
      )}
    </div>
  );
};
