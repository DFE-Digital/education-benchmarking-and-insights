import React, { useCallback, useEffect, useLayoutEffect, useState } from 'react';
import SchoolApi, { WorkforceBenchmarkResult } from '../services/school-api';
import {ChartMode, oppositeMode} from "../chart-mode";
import {ChartModeContext, SelectedSchoolContext} from '../contexts';
import {CompareYourSchoolViewProps, SelectedSchool} from "../types";


// @ts-ignore
import { initAll } from 'govuk-frontend';
import ToggleChartMode from "../components/toggle-chart-mode.tsx";

type CompareYourWorkforceViewProps = {
  urn: string;
  academyYear: string;
  maintainedYear: string;
};

const CompareYourWorkforce: React.FC<CompareYourWorkforceViewProps> = ({ urn, academyYear, maintainedYear }) => {
  const [workforceData, setWorkforceData] = useState<WorkforceBenchmarkResult>();
  const [displayMode, setDisplayMode] = useState<ChartMode>(ChartMode.CHART);
  const [selectedSchool, setSelectedSchool] = useState<SelectedSchool>({urn: "", name:""});

  useLayoutEffect(() => {
    initAll();
  }, []);

  const getWorkforceData = useCallback(async () => {
    try {
      const data = await SchoolApi.getWorkforceBenchmarkData(urn);
      setWorkforceData(data);
    } catch (error) {
      console.error('Error fetching workforce benchmark data', error);
    }
  }, [urn]);

  useEffect(() => {
    getWorkforceData();
  }, [getWorkforceData]);

  // Function to toggle between chart and table display modes
  const toggleChartMode = () => {
    setDisplayMode(oppositeMode(displayMode));
  };

  return (
<SelectedSchoolContext.Provider value={selectedSchool}>
            <div className="govuk-grid-row">
                <div className="govuk-grid-column-two-thirds">
                    <p className="govuk-body"></p>
                    </div>
        <div className="govuk-grid-column-one-third">
        <ToggleChartMode displayMode={displayMode} handleChange={toggleChartMode}/>
                </div>
            </div>
            <ChartModeContext.Provider value={displayMode}>
                <TotalExpenditure schools={workforceData ? workforceData.results : new Array<WorkforceBenchmarkResult>()}/>
                <ExpenditureAccordion
                    schools={workforceData ? workforceData.results : new Array<WorkforceBenchmarkResult>()}/>
            </ChartModeContext.Provider>
        </SelectedSchoolContext.Provider>

  );
};

export default CompareYourWorkforce;
