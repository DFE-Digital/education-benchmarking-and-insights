import { BalanceSection, IncomeSection } from "../historic-data/partials";
import {
  CensusSection,
  SpendingSection,
} from "src/views/historic-data-2/partials";
import {
  HistoricData2SectionName,
  HistoricData2ViewProps,
} from "src/views/historic-data-2/types";
import { SchoolEstablishment } from "src/constants";
import { useGovUk } from "src/hooks/useGovUk";
import { ChartModeChart } from "src/components";
import { ChartModeProvider } from "src/contexts";
import { useState } from "react";

export const HistoricData2: React.FC<HistoricData2ViewProps> = ({
  preLoadSections,
  ...props
}) => {
  const [loadedSections, setLoadedSections] = useState<
    HistoricData2SectionName[]
  >(preLoadSections ?? ["spending"]);
  useGovUk();

  const handleSectionLoad = (section: HistoricData2SectionName) => {
    if (loadedSections.includes(section)) {
      return;
    }

    setLoadedSections([...loadedSections, section]);
  };

  return (
    <div className="govuk-tabs" data-module="govuk-tabs">
      <ul className="govuk-tabs__list">
        <li className="govuk-tabs__list-item govuk-tabs__list-item--selected">
          <a
            className="govuk-tabs__tab"
            href="#spending"
            onClick={() => handleSectionLoad("spending")}
          >
            Spending
          </a>
        </li>
        <li className="govuk-tabs__list-item">
          <a
            className="govuk-tabs__tab"
            href="#income"
            onClick={() => handleSectionLoad("income")}
          >
            Income
          </a>
        </li>
        <li className="govuk-tabs__list-item">
          <a
            className="govuk-tabs__tab"
            href="#balance"
            onClick={() => handleSectionLoad("balance")}
          >
            Balance
          </a>
        </li>
        {props.type === SchoolEstablishment && (
          <li className="govuk-tabs__list-item">
            <a
              className="govuk-tabs__tab"
              href="#census"
              onClick={() => handleSectionLoad("census")}
            >
              Pupil and workforce
            </a>
          </li>
        )}
      </ul>
      <ChartModeProvider initialValue={ChartModeChart}>
        <div className="govuk-tabs__panel" id="spending">
          <SpendingSection
            {...props}
            load={loadedSections.includes("spending")}
          />
        </div>
        <div
          className="govuk-tabs__panel govuk-tabs__panel--hidden"
          id="income"
        >
          <IncomeSection {...props} load={loadedSections.includes("income")} />
        </div>
        <div
          className="govuk-tabs__panel govuk-tabs__panel--hidden"
          id="balance"
        >
          <BalanceSection
            {...props}
            load={loadedSections.includes("balance")}
          />
        </div>
        {props.type === SchoolEstablishment && (
          <div
            className="govuk-tabs__panel govuk-tabs__panel--hidden"
            id="census"
          >
            <CensusSection
              {...props}
              load={loadedSections.includes("census")}
            />
          </div>
        )}
      </ChartModeProvider>
    </div>
  );
};
