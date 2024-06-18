import React from "react";
import { CompareYourTrustViewProps } from "src/views/compare-your-trust";
import { useGovUk } from "src/hooks/useGovUk";
import { SpendingSection } from "./partials/spending-section";

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
      </ul>
      <div className="govuk-tabs__panel" id="spending">
        <SpendingSection id={id} />
      </div>
    </div>
  );
};
