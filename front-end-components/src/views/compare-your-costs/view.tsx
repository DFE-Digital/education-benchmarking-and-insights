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
  const { type, id } = props;
  const [expenditureData, setExpenditureData] = useState<ExpenditureData[]>();
  const [displayMode, setDisplayMode] = useState<string>(ChartModeChart);
  const [selectedSchool, setSelectedSchool] = useState<SelectedSchool>(
    School.empty
  );

  useGovUk();

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
      });
    },
    // eslint-disable-next-line react-hooks/exhaustive-deps
    [getExpenditure]
  );

  const toggleChartMode = (e: React.ChangeEvent<HTMLInputElement>) => {
    setDisplayMode(e.target.value);
  };

  const hasIncompleteData =
    expenditureData?.some((x) => x.hasIncompleteData) ?? false;

  return (
    <SelectedSchoolContext.Provider value={selectedSchool}>
      <div className="view-as-toggle">
        <ChartMode displayMode={displayMode} handleChange={toggleChartMode} />
      </div>
      <HasIncompleteDataContext.Provider value={hasIncompleteData}>
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
