import React, { useLayoutEffect } from "react";
// eslint-disable-next-line @typescript-eslint/ban-ts-comment
//@ts-expect-error
import { initAll } from "govuk-frontend";
import {
  BalanceSection,
  IncomeSection,
  Spending,
  WorkforceSection,
} from "src/views/school-history/partials";
import { SchoolHistoryViewProps } from "src/views";

export const SchoolHistory: React.FC<SchoolHistoryViewProps> = (props) => {
  const { urn } = props;
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
        <li className="govuk-tabs__list-item">
          <a className="govuk-tabs__tab" href="#workforce">
            Workforce
          </a>
        </li>
      </ul>
      <div className="govuk-tabs__panel" id="spending">
        <Spending />
      </div>
      <div className="govuk-tabs__panel govuk-tabs__panel--hidden" id="income">
        <IncomeSection urn={urn} />
      </div>
      <div className="govuk-tabs__panel govuk-tabs__panel--hidden" id="balance">
        <BalanceSection urn={urn} />
      </div>
      <div
        className="govuk-tabs__panel govuk-tabs__panel--hidden"
        id="workforce"
      >
        <WorkforceSection urn={urn} />
      </div>
    </div>
  );
};
