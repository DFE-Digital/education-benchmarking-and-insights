import React, { useCallback, useEffect, useState } from "react";
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
  HasIncompleteDataContext,
} from "src/contexts";
import { SchoolEstablishment } from "src/constants.tsx";
import { useGovUk } from "src/hooks/useGovUk";

export const CompareYourCosts: React.FC<CompareYourCostsViewProps> = (
  props
) => {
  const { type, id, phases } = props;
  const [expenditureData, setExpenditureData] = useState<ExpenditureData[]>();
  const [displayMode, setDisplayMode] = useState<string>(ChartModeChart);
  const [selectedSchool, setSelectedSchool] = useState<SelectedSchool>(
    School.empty
  );
  const [phase, setPhase] = useState<string | undefined>(
    phases ? phases[0] : undefined
  );

  useGovUk();

  const getExpenditure = useCallback(async () => {
    return await EstablishmentsApi.getExpenditure(type, id, phase);
  }, [type, id, phase]);

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
      });
    },
    // eslint-disable-next-line react-hooks/exhaustive-deps
    [getExpenditure]
  );

  const toggleChartMode = (e: React.ChangeEvent<HTMLInputElement>) => {
    setDisplayMode(e.target.value);
  };

  const handlePhaseChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setPhase(e.target.value);
  };

  const hasIncompleteData =
    expenditureData?.some((x) => x.hasIncompleteData) ?? false;

  const hasNoData = expenditureData?.length === 0;

  return (
    <SelectedSchoolContext.Provider value={selectedSchool}>
      <div className="chart-options">
        <div>
          {phases && (
            <div className="govuk-form-group">
              <label className="govuk-label govuk-label--s" htmlFor="phase">
                Phase
              </label>
              <select
                className="govuk-select"
                name="phase"
                id="phase"
                onChange={handlePhaseChange}
              >
                {phases.map((phase) => {
                  return (
                    <option key={phase} value={phase}>
                      {phase}
                    </option>
                  );
                })}
              </select>
            </div>
          )}
        </div>
        <div>
          <ChartMode displayMode={displayMode} handleChange={toggleChartMode} />
        </div>
      </div>
      <HasIncompleteDataContext.Provider
        value={{ hasIncompleteData, hasNoData }}
      >
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
      </HasIncompleteDataContext.Provider>
    </SelectedSchoolContext.Provider>
  );
};
