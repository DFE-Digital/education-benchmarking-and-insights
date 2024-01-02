import React, { useCallback, useEffect, useLayoutEffect, useState } from 'react';
import WorkforceChart from '../components/workforce/workforce-chart'; // Replace with your actual chart component
import WorkforceTable from '../components/workforce/workforce-table'; // Replace with your actual table component
import SchoolApi, { WorkforceBenchmarkResult } from '../services/school-api';
import { ChartMode, ChartModeContext, oppositeMode } from '../chart-more';

// @ts-ignore
import { initAll } from 'govuk-frontend';

type CompareYourWorkforceViewProps = {
  urn: string;
  academyYear: string;
  maintainedYear: string;
};

const CompareYourWorkforce: React.FC<CompareYourWorkforceViewProps> = ({ urn, academyYear, maintainedYear }) => {
  const [workforceData, setWorkforceData] = useState<WorkforceBenchmarkResult>();
  const [displayMode, setDisplayMode] = useState<ChartMode>(ChartMode.CHART);

  useLayoutEffect(() => {
    initAll();
  }, []);


  const getWorkforceData = useCallback(async () => {
    const data = await SchoolApi.getWorkforceBenchmarkData(urn); // API call without try...catch
    setWorkforceData(data); // Set the data using the same variable
  }, [urn]);
  
  useEffect(() => {
    getWorkforceData().then(setWorkforceData); // Use the same variables
  }, [getWorkforceData])
  

  const getWorkforceData = useCallback(async () => {
      const data = await SchoolApi.getWorkforceBenchmarkData(urn); 
      setWorkforceData(data);
  }, [urn]);
  
  useEffect(() => {
    getWorkforceData();
  }, [getWorkforceData]);
  

  const toggleChartMode = () => {
    setDisplayMode(oppositeMode(displayMode));
  };

  return (
    <>
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-two-thirds">
          <p className="govuk-body">
            The data below is from the latest year available, For maintained schools this is {maintainedYear},
            academies for {academyYear}
          </p>
        </div>
        <div className="govuk-grid-column-one-third">
          <div className="govuk-form-group">
            <fieldset className="govuk-fieldset">
              <legend className="govuk-fieldset__legend govuk-fieldset__legend--s">
                <h2 className="govuk-fieldset__heading">View as</h2>
              </legend>
              <div className="govuk-radios govuk-radios--small govuk-radios--inline" data-module="govuk-radios">
                <div className="govuk-radios__item">
                  <input
                    className="govuk-radios__input"
                    id="mode-chart"
                    name="changedChartMode"
                    type="radio"
                    value={ChartMode.CHART}
                    defaultChecked={displayMode == ChartMode.CHART}
                    onChange={toggleChartMode}
                  />
                  <label className="govuk-label govuk-radios__label" htmlFor="mode-chart">
                    Chart
                  </label>
                </div>
                <div className="govuk-radios__item">
                  <input
                    className="govuk-radios__input"
                    id="mode-table"
                    name="changedChartMode"
                    type="radio"
                    value={ChartMode.TABLE}
                    defaultChecked={displayMode == ChartMode.TABLE}
                    onChange={toggleChartMode}
                  />
                  <label className="govuk-label govuk-radios__label" htmlFor="mode-table">
                    Table
                  </label>
                </div>
              </div>
            </fieldset>
          </div>
          <button className="govuk-button govuk-button--secondary" data-module="govuk-button" onClick={toggleChartMode}>
            {oppositeMode(displayMode)}
          </button>
        </div>
      </div>
      <ChartModeContext.Provider value={displayMode}>
        <WorkforceChart urn={urn}
        schools={workforceData}






        {displayMode === ChartMode.CHART ? (
          <>
            <WorkforceChart data={workforceData?.schoolWorkforceFTE || []} metric="Pupils per staff role" />
            {/* Add more chart components here */}
          </>
        ) : (
          // Render benchmark tables
          <>
            <WorkforceTable data={workforceData?.schoolWorkforceFTE || []} metric="Pupils per staff role" />
            {/* Add more table components here */}
          </>
        )}
      </ChartModeContext.Provider>
    </>
  );
};

export default CompareYourWorkforce;
