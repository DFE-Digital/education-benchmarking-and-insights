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
import { CompareYourSchoolViewProps } from "src/views/compare-your-costs";
import { SchoolApi, SchoolApiResult, Expenditure } from "src/services";
import { ChartMode, ChartModeChart } from "src/components";
import {
  School,
  SelectedSchool,
  SelectedSchoolContext,
  ChartModeContext,
} from "src/contexts";

export const CompareYourSchool: React.FC<CompareYourSchoolViewProps> = (
  props
) => {
  const { urn, academyYear, maintainedYear } = props;
  const [expenditureData, setExpenditureData] =
    useState<SchoolApiResult<Expenditure>>();
  const [displayMode, setDisplayMode] = useState<string>(ChartModeChart);
  const [selectedSchool, setSelectedSchool] = useState<SelectedSchool>(
    School.empty
  );

  useLayoutEffect(() => {
    initAll();
  }, []);

  const getExpenditure = useCallback(async () => {
    return await SchoolApi.getSchoolExpenditure(urn);
  }, [urn]);

  useEffect(() => {
    getExpenditure().then((data) => {
      setExpenditureData(data);

      const currentSchool = data.results.find((school) => school.urn == urn);
      if (currentSchool) {
        setSelectedSchool({ urn: currentSchool.urn, name: currentSchool.name });
      }
    });
  }, [getExpenditure]);

  const toggleChartMode = (e: React.ChangeEvent<HTMLInputElement>) => {
    setDisplayMode(e.target.value);
  };

  return (
    <SelectedSchoolContext.Provider value={selectedSchool}>
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-two-thirds">
          <p className="govuk-body">
            The data below is from the latest year available, For maintained
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
            expenditureData ? expenditureData.results : new Array<Expenditure>()
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
