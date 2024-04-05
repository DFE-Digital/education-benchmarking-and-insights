import React, {
  useCallback,
  useEffect,
  useLayoutEffect,
  useState,
} from "react";
// eslint-disable-next-line @typescript-eslint/ban-ts-comment
//@ts-expect-error
import { initAll } from "govuk-frontend";
import {
  TotalExpenditure,
  ExpenditureAccordion,
} from "src/views/compare-your-costs/partials";
import { CompareYourCostsViewProps } from "src/views/compare-your-costs";
import { EstablishmentsApi, ExpenditureData } from "src/services";
import { ChartMode, ChartModeChart } from "src/components";
import {
  School,
  SelectedSchool,
  SelectedSchoolContext,
  ChartModeContext,
} from "src/contexts";
import { SchoolEstablishment } from "src/constants.tsx";

export const CompareYourCosts: React.FC<CompareYourCostsViewProps> = (
  props
) => {
  const { type, id } = props;
  const [expenditureData, setExpenditureData] = useState<ExpenditureData[]>();
  const [displayMode, setDisplayMode] = useState<string>(ChartModeChart);
  const [selectedSchool, setSelectedSchool] = useState<SelectedSchool>(
    School.empty
  );

  useLayoutEffect(() => {
    initAll();
  }, []);

  const getExpenditure = useCallback(async () => {
    return await EstablishmentsApi.getExpenditure(type, id);
  }, [type, id]);

  useEffect(
    () => {
      getExpenditure().then((data) => {
        setExpenditureData(data);

        const currentSchool = data.find(
          (school) => type == SchoolEstablishment && school.urn == id
        );
        if (currentSchool) {
          setSelectedSchool({
            urn: currentSchool.urn,
            name: currentSchool.name,
          });
        }

        //setLoaded(true);
      });
    },
    // eslint-disable-next-line react-hooks/exhaustive-deps
    [getExpenditure]
  );

  const toggleChartMode = (e: React.ChangeEvent<HTMLInputElement>) => {
    setDisplayMode(e.target.value);
  };

  // todo: move somewhere common to all views
  useLayoutEffect(() => {
    const href = window?.location?.href;
    if (!href) {
      return;
    }

    // https://github.com/alphagov/govuk-design-system-backlog/issues/1#issuecomment-1187038700
    if (href.split("#")?.length === 2) {
      // Split the string and get the ID
      const anchorId = href.split("#")[1];

      // Check for the requested ID within the accordion
      const accordion = document.querySelector(
        `.govuk-accordion__section#${anchorId}`
      );
      if (accordion) {
        accordion.scrollIntoView();
      }
    }
  });

  return (
    <SelectedSchoolContext.Provider value={selectedSchool}>
      <div className="view-as-toggle">
        <ChartMode displayMode={displayMode} handleChange={toggleChartMode} />
      </div>
      <ChartModeContext.Provider value={displayMode}>
        <TotalExpenditure
          schools={
            expenditureData ? expenditureData : new Array<ExpenditureData>()
          }
        />
        <ExpenditureAccordion
          schools={
            expenditureData ? expenditureData : new Array<ExpenditureData>()
          }
        />
      </ChartModeContext.Provider>
    </SelectedSchoolContext.Provider>
  );
};
