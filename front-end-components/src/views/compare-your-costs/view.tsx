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
import {
  EstablishmentsApi,
  EstablishmentApiResult,
  Expenditure,
} from "src/services";
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
  const { type, id, academyYear, maintainedYear } = props;
  const [expenditureData, setExpenditureData] =
    useState<EstablishmentApiResult<Expenditure>>();
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

        const currentSchool = data.results.find(
          (school) => type == SchoolEstablishment && school.urn == id
        );
        if (currentSchool) {
          setSelectedSchool({
            urn: currentSchool.urn,
            name: currentSchool.name,
          });
        }
      });
    },
    // eslint-disable-next-line react-hooks/exhaustive-deps
    [getExpenditure]
  );

  const toggleChartMode = (e: React.ChangeEvent<HTMLInputElement>) => {
    setDisplayMode(e.target.value);
  };

  return (
    <SelectedSchoolContext.Provider value={selectedSchool}>
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-two-thirds">
          <p className="govuk-body">
            The data below is from the latest year available. For maintained
            schools this is {maintainedYear}, academies for {academyYear}
          </p>
        </div>
        <div className="govuk-grid-column-one-third">
          <ChartMode displayMode={displayMode} handleChange={toggleChartMode} />
        </div>
      </div>
      <ChartModeContext.Provider value={displayMode}>
        <TotalExpenditure
          schools={
            expenditureData ? expenditureData.results : new Array<Expenditure>(
          }
        />
        <ExpenditureAccordion
          schools={
            expenditureData ? expenditureData.results : new Array<Expenditure>()
          }
        />
      </ChartModeContext.Provider>
    </SelectedSchoolContext.Provider>
  );
};
