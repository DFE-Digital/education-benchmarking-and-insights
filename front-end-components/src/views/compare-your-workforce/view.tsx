import React, {
  useCallback,
  useEffect,
  useLayoutEffect,
  useState,
} from "react";
// eslint-disable-next-line @typescript-eslint/ban-ts-comment
//@ts-expect-error
import { initAll } from "govuk-frontend";
import { CompareYourWorkforceViewProps } from "src/views";
import { EstablishmentsApi, Workforce } from "src/services";
import { ChartMode, ChartModeChart } from "src/components";
import {
  SelectedSchool,
  SelectedSchoolContext,
  ChartModeContext,
} from "src/contexts";
import {
  AuxiliaryStaff,
  Headcount,
  NonClassroomSupport,
  SchoolWorkforce,
  SeniorLeadership,
  TeachingAssistants,
  TotalTeachers,
  TotalTeachersQualified,
} from "src/views/compare-your-workforce/partials";
import { SchoolEstablishment } from "src/constants.tsx";

export const CompareYourWorkforce: React.FC<CompareYourWorkforceViewProps> = (
  props
) => {
  const { type, id, academyYear, maintainedYear } = props;
  const [workforceData, setWorkforceData] = useState<Workforce[]>();
  const [displayMode, setDisplayMode] = useState<string>(ChartModeChart);
  const [selectedSchool, setSelectedSchool] = useState<SelectedSchool>({
    urn: "",
    name: "",
  });

  useLayoutEffect(() => {
    initAll();
  }, []);

  const getWorkforceData = useCallback(async () => {
    return await EstablishmentsApi.getWorkforceBenchmarkData(type, id);
  }, [type, id]);

  useEffect(
    () => {
      getWorkforceData().then((data) => {
        setWorkforceData(data);

        const currentSchool = data.find(
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
    [getWorkforceData]
  );

  // Function to toggle between chart and table display modes
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
        <SchoolWorkforce
          schools={workforceData ? workforceData : new Array<Workforce>()}
        />
        <TotalTeachers
          schools={workforceData ? workforceData : new Array<Workforce>()}
        />
        <TotalTeachersQualified
          schools={workforceData ? workforceData : new Array<Workforce>()}
        />
        <SeniorLeadership
          schools={workforceData ? workforceData : new Array<Workforce>()}
        />
        <TeachingAssistants
          schools={workforceData ? workforceData : new Array<Workforce>()}
        />
        <NonClassroomSupport
          schools={workforceData ? workforceData : new Array<Workforce>()}
        />
        <AuxiliaryStaff
          schools={workforceData ? workforceData : new Array<Workforce>()}
        />
        <Headcount
          schools={workforceData ? workforceData : new Array<Workforce>()}
        />
      </ChartModeContext.Provider>
    </SelectedSchoolContext.Provider>
  );
};
