import React, { useCallback, useEffect, useLayoutEffect, useState } from 'react';
import SchoolApi, { WorkforceBenchmarkResult } from '../services/school-api';
import { ChartMode, ChartModeContext, oppositeMode } from '../chart-more';
import WorkforceChart from '../components/workforce/workforce-chart'; // Replace with your actual chart component
import WorkforceTable from '../components/workforce/workforce-table'; // Replace with your actual table component

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
  const toggleDisplayMode = () => {
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
                    defaultChecked={displayMode === ChartMode.CHART}
                    onChange={() => setDisplayMode(ChartMode.CHART)}
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
                    defaultChecked={displayMode === ChartMode.TABLE}
                    onChange={() => setDisplayMode(ChartMode.TABLE)}
                  />
                  <label className="govuk-label govuk-radios__label" htmlFor="mode-table">
                    Table
                  </label>
                </div>
              </div>
            </fieldset>
          </div>
          <button className="govuk-button govuk-button--secondary" data-module="govuk-button" onClick={toggleDisplayMode}>
            Switch Display
          </button>
        </div>
      </div>
      <ChartModeContext.Provider value={displayMode}>
        {displayMode === ChartMode.CHART ? (
          // Render benchmark charts
          <WorkforceChart urn={urn} schools={workforceData?.results || []} />
        ) : (
          // Render benchmark tables
          <WorkforceTable urn={urn} schools={workforceData?.results || []} />
        )}
      </ChartModeContext.Provider>
    </>
  );
};

export default CompareYourWorkforce;
