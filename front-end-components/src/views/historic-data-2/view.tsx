import { SpendingSection } from "src/views/historic-data-2/partials";
import { HistoricData2ViewProps } from "src/views/historic-data-2/types";
import { useGovUk } from "src/hooks/useGovUk";
import { ChartModeChart } from "src/components";
import { ChartModeProvider } from "src/contexts";

export const HistoricData2: React.FC<HistoricData2ViewProps> = (props) => {
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
      </ul>
      <ChartModeProvider initialValue={ChartModeChart}>
        <div className="govuk-tabs__panel" id="spending">
          <SpendingSection type={type} id={id} />
        </div>
      </ChartModeProvider>
    </div>
  );
};
