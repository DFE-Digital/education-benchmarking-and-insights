import { useState } from "react";
import {
  HistoricDataHighNeedsSectionName,
  HistoricDataHighNeedsViewProps,
} from "src/views/historic-data-high-needs/types";
import { useGovUk } from "src/hooks/useGovUk";
import { ChartModeChart } from "src/components";
import { ChartModeProvider } from "src/contexts";
import {
  Section251Section,
  Send2Section,
} from "src/views/historic-data-high-needs/partials";

export const HistoricDataHighNeeds: React.FC<
  HistoricDataHighNeedsViewProps
> = ({ preLoadSections, ...props }) => {
  const [loadedSections, setLoadedSections] = useState<
    HistoricDataHighNeedsSectionName[]
  >(preLoadSections ?? ["section-251"]);
  useGovUk();

  const handleSectionLoad = (section: HistoricDataHighNeedsSectionName) => {
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
            href="#section-251"
            onClick={() => handleSectionLoad("section-251")}
          >
            Section 251
          </a>
        </li>
        <li className="govuk-tabs__list-item">
          <a
            className="govuk-tabs__tab"
            href="#send-2"
            onClick={() => handleSectionLoad("send-2")}
          >
            SEND 2
          </a>
        </li>
      </ul>
      <ChartModeProvider initialValue={ChartModeChart}>
        <div className="govuk-tabs__panel" id="section-251">
          <Section251Section
            {...props}
            load={loadedSections.includes("section-251")}
          />
        </div>
        <div
          className="govuk-tabs__panel govuk-tabs__panel--hidden"
          id="send-2"
        >
          <Send2Section {...props} load={loadedSections.includes("send-2")} />
        </div>
      </ChartModeProvider>
    </div>
  );
};
